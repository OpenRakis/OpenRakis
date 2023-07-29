using System;

using Avalonia.Controls;
using Avalonia.Controls.Templates;

using DuneEdit2.ViewModels;

namespace DuneEdit2;

public class ViewLocator : IDataTemplate
{
    public Control Build(object? data)
    {
        var name = data?.GetType().FullName!.Replace("ViewModel", "View");
        if (string.IsNullOrWhiteSpace(name))
        {
            return new TextBlock { Text = $"Not Found: {name}" };
        }
        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }
        else
        {
            return new TextBlock { Text = $"Not Found: {name}" };
        }
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}