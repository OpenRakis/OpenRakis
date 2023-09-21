using System;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace DuneEdit2.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    [RelayCommand]
    private void ZoomIn()
    {
        MapOfDuneZoomedIn.IsVisible = true;
        MapOfDuneZoomedOut.IsVisible = false;
    }

    [RelayCommand]
    private void ZoomOut()
    {
        MapOfDuneZoomedIn.IsVisible = false;
        MapOfDuneZoomedOut.IsVisible = true;
    }
}