namespace DuneEdit2.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reactive;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    using Avalonia.Controls;

    using DuneEdit2.Models;
    using DuneEdit2.Parsers;

    using ReactiveUI;

    public class MainWindowViewModel : ViewModelBase
    {
        private bool _isSavegameLoaded;

        private Window? _mainWindow;

        private SaveGameFile _savegameFile = new();

        public MainWindowViewModel()
        {
            OpenSaveGame = ReactiveCommand.CreateFromTask<Unit, Unit>(OpenSaveGameMethodAsync);
            SaveGameFile = ReactiveCommand.Create<Unit, Unit>(SaveGameMethod);
            UpdateSietch = ReactiveCommand.Create<Unit, Unit>(UpdateSietchMethod);
            UpdateTroop = ReactiveCommand.Create<Unit, Unit>(UpdateTroopMethod);
        }

        private Unit UpdateTroopMethod(Unit arg)
        {
            if (CurrentTroop != null)
            {
                _savegameFile.UpdateTroop(CurrentTroop.Troop);
            }
            return Unit.Default;
        }

        private Unit UpdateSietchMethod(Unit arg)
        {
            if (CurrentSietch != null)
            {
                _savegameFile.UpdateSietch(CurrentSietch.Sietch);
            }
            return Unit.Default;
        }

        public ReactiveCommand<Unit, Unit>? ExitApp { get; private set; }

        public ReactiveCommand<Unit, Unit>? UpdateSietch { get; private set; }
        public ReactiveCommand<Unit, Unit>? UpdateTroop { get; private set; }

        public bool IsSaveGameLoaded
        {
            get
            {
                if (Design.IsDesignMode)
                    return true;
                else
                    return _isSavegameLoaded;
            }
            private set => this.RaiseAndSetIfChanged(ref _isSavegameLoaded, value);
        }

        private SietchViewModel? _currentSietch;

        public SietchViewModel? CurrentSietch
        {
            get => _currentSietch;
            set => this.RaiseAndSetIfChanged(ref _currentSietch, value);
        }

        private TroopViewModel? _currentTroop;

        public TroopViewModel? CurrentTroop
        {
            get => _currentTroop;
            set => this.RaiseAndSetIfChanged(ref _currentTroop, value);
        }

        private ObservableCollection<SietchViewModel> _sietches = new();

        public ObservableCollection<SietchViewModel> Sietches
        {
            get => _sietches;
            private set => this.RaiseAndSetIfChanged(ref _sietches, value);
        }

        private ObservableCollection<TroopViewModel> _troops = new();

        public ObservableCollection<TroopViewModel> Troops
        {
            get => _troops;
            private set => this.RaiseAndSetIfChanged(ref _troops, value);
        }

        public ReactiveCommand<Unit, Unit> OpenSaveGame { get; private set; }

        private int _spiceVM = 0;

        public int SpiceVM
        {
            get => _spiceVM;
            set => this.RaiseAndSetIfChanged(ref _spiceVM, value);
        }

        private byte _charismaVM = 0;

        public byte CharismaVM
        {
            get => _charismaVM;
            set => this.RaiseAndSetIfChanged(ref _charismaVM, value);
        }

        private int _contactDistanceVM = 0;

        public int ContactDistanceVM
        {
            get => _contactDistanceVM;
            set => this.RaiseAndSetIfChanged(ref _contactDistanceVM, value);
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
                _savegameFile = new SaveGameFile(result[0]);
                SpiceVM = _savegameFile.Generals.Spice;
                CharismaVM = _savegameFile.Generals.CharismaGUI;
                ContactDistanceVM = _savegameFile.Generals.ContactDistance;
                PopulateSietches(_savegameFile.GetSietches());
                PopulateTroops(_savegameFile.GetTroops(), _savegameFile.GetSietches());
                IsSaveGameLoaded = true;
            }
            return Unit.Default;
        }

        private void PopulateSietches(List<Sietch> sietches)
        {
            foreach (var sietch in sietches)
            {
                Sietches.Add(new SietchViewModel(sietch));
            }
            if (sietches.Any())
            {
                CurrentSietch = Sietches.First();
            }
        }

        private void PopulateTroops(List<Troop> troops, List<Sietch> sietches)
        {
            foreach (var troop in troops)
            {
                var sietch = sietches.FirstOrDefault(x => x.HousedTroopID == troop.TroopID);
                var troopVM = new TroopViewModel(troop, sietch);
                Troops.Add(troopVM);
            }
            if (troops.Any())
            {
                CurrentTroop = Troops.First();
            }
        }

        private Unit SaveGameMethod(Unit arg)
        {
            if (IsSaveGameLoaded == false)
            {
                return Unit.Default;
            }
            _savegameFile.UpdateCharisma(CharismaVM);
            _savegameFile.UpdateSpice(SpiceVM);
            _savegameFile.UpdateContactDistance(ContactDistanceVM);
            _savegameFile.SaveCompressed();
            return Unit.Default;
        }
    }
}