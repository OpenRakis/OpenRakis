namespace DuneEdit2.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reactive;
    using System.Reactive.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Avalonia.Controls;
    using ReactiveUI;

    public class MainWindowViewModel : ViewModelBase
    {
        private bool _isSavegameLoaded;

        private Window? _mainWindow;

        private SaveGame _savegame = new();

        public MainWindowViewModel()
        {
            OpenSaveGame = ReactiveCommand.CreateFromTask<Unit, Unit>(OpenSaveGameMethodAsync);
            SaveGame = ReactiveCommand.Create<Unit, Unit>(SaveGameMethod);
        }

        public ReactiveCommand<Unit, Unit>? ExitApp { get; private set; }

        public bool IsSaveGameLoaded
        {
            get => _isSavegameLoaded;
            private set { this.RaiseAndSetIfChanged(ref _isSavegameLoaded, value); }
        }

        public ReactiveCommand<Unit, Unit> OpenSaveGame { get; private set; }

        public SaveGame Savegame
        {
            get => _savegame;
            private set { this.RaiseAndSetIfChanged(ref _savegame, value); }
        }

        public ReactiveCommand<Unit, Unit> SaveGame { get; private set; }

        private Window? MainWindow
        {
            get => _mainWindow;
            set => _mainWindow = value;
        }

        public static MainWindowViewModel Create(Window mainWindow)
        {
            var instance = new MainWindowViewModel
            {
                ExitApp = ReactiveCommand.Create(() => mainWindow.Close()),
                MainWindow = mainWindow
            };
            return instance;
        }

        private async Task<Unit> OpenSaveGameMethodAsync(Unit arg)
        {
            var dialog = new OpenFileDialog();
            dialog.AllowMultiple = false;
            var result = await dialog.ShowAsync(_mainWindow);
            if (result.Length > 0)
            {
                Savegame = new SaveGame(result[0]);
                IsSaveGameLoaded = true;
            }
            return Unit.Default;
        }

        private Unit SaveGameMethod(Unit arg)
        {
            _savegame.SaveCompressed();
            return Unit.Default;
        }
    }
}