namespace DuneEdit2.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
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
        private SaveGameFile _savegameFile = new();

        public MainWindowViewModel()
        {
            OpenSaveGame = ReactiveCommand.CreateFromTask<Unit, Unit>(OpenSaveGameMethodAsync);
            SaveGameFile = ReactiveCommand.CreateFromTask<Unit, Unit>(SaveGameMethodAsync);
            UpdateSietch = ReactiveCommand.Create<Unit, Unit>(UpdateLocationMethod);
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

        private Unit UpdateLocationMethod(Unit arg)
        {
            if (CurrentLocation != null)
            {
                _savegameFile.UpdateSietch(CurrentLocation.Sietch);
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

        private LocationViewModel? _currentSietch;

        public LocationViewModel? CurrentLocation
        {
            get => _currentSietch;
            set => this.RaiseAndSetIfChanged(ref _currentSietch, value);
        }

        private TroopViewModel? _currentTroop;

        public TroopViewModel? CurrentTroop
        {
            get => _currentTroop;
            set
            {
                _currentTroop = value;
                if(value != null)
                {
                    this._currentLocationWithTroop = this.Locations.FirstOrDefault(x => x.HousedTroopID == value.TroopID);
                    this.RaisePropertyChanged(nameof(CurrentLocationWithTroop));
                }
                this.RaisePropertyChanged(nameof(CurrentTroop));
            }
        }

        private List<LocationViewModel> _locations = new();

        public List<LocationViewModel> Locations
        {
            get => _locations;
            private set => this.RaiseAndSetIfChanged(ref _locations, value);
        }

        private List<LocationViewModel> _locationsWithTroops = new();

        public List<LocationViewModel> LocationsWithTroops
        {
            get => _locationsWithTroops;
            private set => this.RaiseAndSetIfChanged(ref _locationsWithTroops, value);
        }

        private LocationViewModel? _currentLocationWithTroop;
        public LocationViewModel? CurrentLocationWithTroop
        {
            get => _currentLocationWithTroop;
            private set
            {
                this.RaiseAndSetIfChanged(ref _currentLocationWithTroop, value);
                this._currentTroop = this.Troops.FirstOrDefault(x => x.TroopID == value?.HousedTroopID);
                this.RaisePropertyChanged(nameof(CurrentTroop));
            }
        }

        private List<TroopViewModel> _troops = new();

        public List<TroopViewModel> Troops
        {
            get => _troops;
            private set => this.RaiseAndSetIfChanged(ref _troops, value);
        }

        public ReactiveCommand<Unit, Unit> OpenSaveGame { get; private set; }

        public string GameStageDesc => GameStageFinder.FindStage(GameStage);

        private byte _gameStage = 0;

        public byte GameStage
        {
            get => _gameStage;
            set
            {
                this.RaiseAndSetIfChanged(ref _gameStage, value);
                this.RaisePropertyChanged(nameof(GameStageDesc));
            }
        }

        private int _spiceVal = 0;

        public int SpiceVal
        {
            get => _spiceVal;
            set => this.RaiseAndSetIfChanged(ref _spiceVal, value);
        }

        private byte _charismaVal = 0;

        public byte CharismaVal
        {
            get => _charismaVal;
            set => this.RaiseAndSetIfChanged(ref _charismaVal, value);
        }

        private int _contactDistanceVal = 0;

        public int ContactDistanceVal
        {
            get => _contactDistanceVal;
            set => this.RaiseAndSetIfChanged(ref _contactDistanceVal, value);
        }

        public ReactiveCommand<Unit, Unit> SaveGameFile { get; private set; }

        private Window? MainWindow { get; set; }

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
            var dialog = new OpenFileDialog
            {
                AllowMultiple = false
            };
            var result = await dialog.ShowAsync(MainWindow);
            if (result != null && result.Length > 0)
            {
                var selectedFile = result[0];
                _savegameFile = new SaveGameFile(selectedFile);
                SpiceVal = _savegameFile.Generals.Spice;
                CharismaVal = _savegameFile.Generals.CharismaGUI;
                ContactDistanceVal = _savegameFile.Generals.ContactDistance;
                GameStage = _savegameFile.Generals.GameStage;
                PopulateTroops(_savegameFile.GetTroops(), _savegameFile.GetSietches());
                PopulateSietches(_savegameFile.GetSietches());
                IsSaveGameLoaded = true;
            }
            return Unit.Default;
        }

        private void PopulateSietches(List<Models.Location> locations)
        {
            var locationsVMs = new List<LocationViewModel>();
            foreach (var location in locations)
            {
                locationsVMs.Add(new LocationViewModel(location));
            }
            Locations = locationsVMs;
            if (Locations.Any())
            {
                Locations = new List<LocationViewModel>(Locations.OrderBy(x => x.RegionName));
                LocationsWithTroops = new List<LocationViewModel>(Locations.OrderBy(x => x.RegionName).Where(x => Troops.Any(y => y.TroopID == x.HousedTroopID)));
                CurrentLocationWithTroop = LocationsWithTroops.FirstOrDefault();
                CurrentLocation = Locations.First();
            }
        }

        private void PopulateTroops(List<Troop> troops, List<Models.Location> locations)
        {
            var troopsVMs = new List<TroopViewModel>();
            foreach (var troop in troops)
            {
                var location = locations.FirstOrDefault(x => x.HousedTroopID == troop.TroopID);
                var troopVM = new TroopViewModel(troop, location);
                troopsVMs.Add(troopVM);
            }
            Troops = troopsVMs;
            if (Troops.Any())
            {
                CurrentTroop = Troops.First();
            }
        }

        private async Task<Unit> SaveGameMethodAsync(Unit arg)
        {
            if (IsSaveGameLoaded == false)
            {
                return Unit.Default;
            }
            _savegameFile.UpdateCharisma(CharismaVal);
            _savegameFile.UpdateSpice(SpiceVal);
            _savegameFile.UpdateContactDistance(ContactDistanceVal);
            _savegameFile.UpdateGameStage(GameStage);
            FileAttributes attributes = File.GetAttributes(_savegameFile.Filename);
            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                var fileName = await new SaveFileDialog()
                {
                    Directory = Path.GetDirectoryName(_savegameFile.Filename),
                    InitialFileName = $"{Path.GetFileNameWithoutExtension(_savegameFile.Filename)}-modified.SAV"
                }.ShowAsync(MainWindow);
                if(string.IsNullOrWhiteSpace(fileName) == false)
                {
                    _savegameFile.SaveCompressedAs(fileName);
                }
            }
            else
            {
                _savegameFile.SaveCompressed();

            }
            return Unit.Default;
        }
    }
}