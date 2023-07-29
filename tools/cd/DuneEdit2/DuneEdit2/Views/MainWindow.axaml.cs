using System;
using System.Reactive;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;

using ReactiveUI;

namespace DuneEdit2.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        ZoomIn = ReactiveCommand.Create<Unit, Unit>(ZoomInMethod);
        ZoomOut = ReactiveCommand.Create<Unit, Unit>(ZoomOutMethod);
        InitializeComponent();
    }

    private Unit ZoomInMethod(Unit arg)
    {
        MapOfDuneZoomedIn.IsVisible = true;
        MapOfDuneZoomedOut.IsVisible = false;
        return Unit.Default;
    }

    private Unit ZoomOutMethod(Unit arg)
    {
        MapOfDuneZoomedIn.IsVisible = false;
        MapOfDuneZoomedOut.IsVisible = true;
        return Unit.Default;
    }

    public ReactiveCommand<Unit, Unit> ZoomOut { get; private set; }
    public ReactiveCommand<Unit, Unit> ZoomIn { get; private set; }
}