using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using Avalonia.Controls;
using ReactiveUI;

namespace DuneEdit2.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
        }

        public ReactiveCommand<Unit, Unit>? ExitApp { get; private set; }

        public static MainWindowViewModel Create(Window mainWindow)
        {
            var instance = new MainWindowViewModel();
            instance.ExitApp = ReactiveCommand.Create((() => mainWindow.Close()));
            return instance;
        }
    }
}