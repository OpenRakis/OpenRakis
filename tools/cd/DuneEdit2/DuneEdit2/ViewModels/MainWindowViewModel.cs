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

    using DuneEdit2.Models;

    using ReactiveUI;

    public class MainWindowViewModel : ViewModelBase
    {
        private bool _isSavegameLoaded;

        private Window? _mainWindow;

        private SaveGame _savegame = new();

        public MainWindowViewModel()
        {
            OpenSaveGame = ReactiveCommand.CreateFromTask<Unit, Unit>(OpenSaveGameMethodAsync);
            SaveGameFile = ReactiveCommand.Create<Unit, Unit>(SaveGameMethod);
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

        private int _spiceVM = 0;

        public int SpiceVM
        {
            get => _spiceVM;
            set { this.RaiseAndSetIfChanged(ref _spiceVM, value); }
        }

        private byte _charismaVM = 0;

        public byte CharismaVM
        {
            get => _charismaVM;
            set { this.RaiseAndSetIfChanged(ref _charismaVM, value); }
        }

        private int _contactDistanceVM = 0;

        public int ContactDistanceVM
        {
            get => _contactDistanceVM;
            set { this.RaiseAndSetIfChanged(ref _contactDistanceVM, value); }
        }

        public ReactiveCommand<Unit, Unit> SaveGameFile { get; private set; }

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
            SpiceVM = Savegame.Generals.Spice;
            CharismaVM = Savegame.Generals.CharismaGUI;
            ContactDistanceVM = Savegame.Generals.ContactDistance;

            return Unit.Default;
        }

        private Unit SaveGameMethod(Unit arg)
        {
            if (IsSaveGameLoaded == false)
            {
                return Unit.Default;
            }
            _savegame.UpdateCharisma(CharismaVM);
            _savegame.UpdateSpice(SpiceVM);
            _savegame.UpdateContactDistance(ContactDistanceVM);
            _savegame.SaveCompressed();
            return Unit.Default;
        }
    }
}