namespace DuneTools.Behaviors;

using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using AvaloniaHex;
using AvaloniaHex.Document;

public static class HexFieldSelectionBehavior
{
    public static readonly AttachedProperty<HexEditor?> HexEditorProperty =
        AvaloniaProperty.RegisterAttached<TextBox, HexEditor?>(
            "HexEditor",
            typeof(HexFieldSelectionBehavior));

    public static readonly AttachedProperty<ulong> ByteOffsetProperty =
        AvaloniaProperty.RegisterAttached<TextBox, ulong>(
            "ByteOffset",
            typeof(HexFieldSelectionBehavior));

    public static readonly AttachedProperty<ulong> ByteLengthProperty =
        AvaloniaProperty.RegisterAttached<TextBox, ulong>(
            "ByteLength",
            typeof(HexFieldSelectionBehavior),
            1UL);

    private static readonly Dictionary<HexEditor, EditorState> States = [];
    private static readonly Dictionary<AvaloniaHex.Editing.Selection, HexEditor> SelectionToEditor = [];
    private static readonly Dictionary<AvaloniaHex.Editing.Caret, HexEditor> CaretToEditor = [];

    static HexFieldSelectionBehavior()
    {
        HexEditorProperty.Changed.AddClassHandler<TextBox>(OnHexEditorChanged);
    }

    public static void SetHexEditor(AvaloniaObject element, HexEditor? value) => element.SetValue(HexEditorProperty, value);

    public static HexEditor? GetHexEditor(AvaloniaObject element) => element.GetValue(HexEditorProperty);

    public static void SetByteOffset(AvaloniaObject element, ulong value) => element.SetValue(ByteOffsetProperty, value);

    public static ulong GetByteOffset(AvaloniaObject element) => element.GetValue(ByteOffsetProperty);

    public static void SetByteLength(AvaloniaObject element, ulong value) => element.SetValue(ByteLengthProperty, value);

    public static ulong GetByteLength(AvaloniaObject element) => element.GetValue(ByteLengthProperty);

    private static void OnHexEditorChanged(TextBox textBox, AvaloniaPropertyChangedEventArgs args)
    {
        if (args.OldValue is HexEditor previousHex)
        {
            UnregisterTextBox(previousHex, textBox);
            textBox.GotFocus -= OnTextBoxGotFocus;
            textBox.PropertyChanged -= OnTextBoxPropertyChanged;
        }

        if (args.NewValue is HexEditor nextHex)
        {
            RegisterTextBox(nextHex, textBox);
            textBox.GotFocus += OnTextBoxGotFocus;
            textBox.PropertyChanged += OnTextBoxPropertyChanged;
        }
    }

    private static void RegisterTextBox(HexEditor editor, TextBox textBox)
    {
        if (!States.TryGetValue(editor, out EditorState? state))
        {
            state = new EditorState();
            States[editor] = state;
            editor.Caret.LocationChanged += OnHexCaretLocationChanged;
            editor.Selection.RangeChanged += OnHexSelectionRangeChanged;
            SelectionToEditor[editor.Selection] = editor;
            CaretToEditor[editor.Caret] = editor;
        }

        state.AddBinding(textBox, GetByteOffset(textBox), Math.Max(1UL, GetByteLength(textBox)));
    }

    private static void UnregisterTextBox(HexEditor editor, TextBox textBox)
    {
        if (!States.TryGetValue(editor, out EditorState? state))
        {
            return;
        }

        state.RemoveBinding(textBox);

        if (state.BindingCount == 0)
        {
            editor.Caret.LocationChanged -= OnHexCaretLocationChanged;
            editor.Selection.RangeChanged -= OnHexSelectionRangeChanged;
            SelectionToEditor.Remove(editor.Selection);
            CaretToEditor.Remove(editor.Caret);
            States.Remove(editor);
        }
    }

    private static void OnTextBoxGotFocus(object? sender, EventArgs e)
    {
        if (sender is TextBox textBox)
        {
            SyncHexFromTextBox(textBox);
        }
    }

    private static void OnTextBoxPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (sender is TextBox textBox
            && textBox.IsFocused
            && (e.Property == TextBox.SelectionStartProperty || e.Property == TextBox.SelectionEndProperty))
        {
            SyncHexFromTextBox(textBox);
        }
    }

    private static void SyncHexFromTextBox(TextBox textBox)
    {
        HexEditor? editor = GetHexEditor(textBox);
        if (editor is null || !States.TryGetValue(editor, out EditorState? state) || state.IsInternalUpdate)
        {
            return;
        }

        ulong start = GetByteOffset(textBox);
        ulong length = Math.Max(1UL, GetByteLength(textBox));

        state.IsInternalUpdate = true;
        try
        {
            editor.Selection.Range = new BitRange(start, start + length);
            editor.Caret.Location = new BitLocation(start);
            editor.HexView?.BringIntoView(new BitLocation(start));
        }
        finally
        {
            state.IsInternalUpdate = false;
        }
    }

    private static void OnHexSelectionRangeChanged(object? sender, EventArgs e)
    {
        if (sender is AvaloniaHex.Editing.Selection selection)
        {
            SelectionToEditor.TryGetValue(selection, out HexEditor? editor);
            SyncTextBoxFromHex(editor);
        }
    }

    private static void OnHexCaretLocationChanged(object? sender, EventArgs e)
    {
        if (sender is AvaloniaHex.Editing.Caret caret)
        {
            CaretToEditor.TryGetValue(caret, out HexEditor? editor);
            SyncTextBoxFromHex(editor);
        }
    }

    private static void SyncTextBoxFromHex(HexEditor? editor)
    {
        if (editor is null || !States.TryGetValue(editor, out EditorState? state) || state.IsInternalUpdate)
        {
            return;
        }

        ulong selectedByte = GetSelectedByte(editor);

        TextBox? matching = state.FindTextBoxByByte(selectedByte);

        if (matching is null)
        {
            return;
        }

        state.IsInternalUpdate = true;
        try
        {
            matching.SelectAll();
            matching.BringIntoView();
        }
        finally
        {
            state.IsInternalUpdate = false;
        }
    }

    private static ulong GetSelectedByte(HexEditor editor)
    {
        BitRange range = editor.Selection.Range;
        if (range.ByteLength > 0)
        {
            return range.Start.ByteIndex;
        }

        return editor.Caret.Location.ByteIndex;
    }

    private sealed class EditorState
    {
        public bool IsInternalUpdate { get; set; }

        private readonly Dictionary<TextBox, BindingEntry> _bindingsByTextBox = [];
        private readonly List<BindingEntry> _bindingsByStart = [];

        public int BindingCount => _bindingsByTextBox.Count;

        public void AddBinding(TextBox textBox, ulong start, ulong length)
        {
            RemoveBinding(textBox);

            var entry = new BindingEntry(textBox, start, start + length);
            _bindingsByTextBox[textBox] = entry;

            int index = _bindingsByStart.BinarySearch(entry, BindingStartComparer.Instance);
            if (index < 0)
            {
                index = ~index;
            }

            _bindingsByStart.Insert(index, entry);
        }

        public void RemoveBinding(TextBox textBox)
        {
            if (!_bindingsByTextBox.Remove(textBox, out BindingEntry? existing))
            {
                return;
            }

            int index = _bindingsByStart.BinarySearch(existing, BindingStartComparer.Instance);
            if (index >= 0)
            {
                while (index > 0 && _bindingsByStart[index - 1].Start == existing.Start)
                {
                    index--;
                }

                for (int i = index; i < _bindingsByStart.Count && _bindingsByStart[i].Start == existing.Start; i++)
                {
                    if (ReferenceEquals(_bindingsByStart[i].TextBox, textBox))
                    {
                        _bindingsByStart.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        public TextBox? FindTextBoxByByte(ulong selectedByte)
        {
            if (_bindingsByStart.Count == 0)
            {
                return null;
            }

            int left = 0;
            int right = _bindingsByStart.Count - 1;

            while (left <= right)
            {
                int mid = left + ((right - left) / 2);
                BindingEntry candidate = _bindingsByStart[mid];

                if (selectedByte < candidate.Start)
                {
                    right = mid - 1;
                }
                else if (selectedByte >= candidate.End)
                {
                    left = mid + 1;
                }
                else
                {
                    return candidate.TextBox;
                }
            }

            return null;
        }
    }

    private sealed record BindingEntry(TextBox TextBox, ulong Start, ulong End);

    private sealed class BindingStartComparer : IComparer<BindingEntry>
    {
        public static BindingStartComparer Instance { get; } = new();

        public int Compare(BindingEntry? x, BindingEntry? y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }

            if (x is null)
            {
                return -1;
            }

            if (y is null)
            {
                return 1;
            }

            int startCompare = x.Start.CompareTo(y.Start);
            if (startCompare != 0)
            {
                return startCompare;
            }

            return x.End.CompareTo(y.End);
        }
    }
}
