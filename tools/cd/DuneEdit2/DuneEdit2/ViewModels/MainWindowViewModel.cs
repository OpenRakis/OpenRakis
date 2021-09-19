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

        private ObservableCollection<GameStageViewModel> _gameStages = new();

        public MainWindowViewModel()
        {
            OpenSaveGame = ReactiveCommand.CreateFromTask<Unit, Unit>(OpenSaveGameMethodAsync);
            SaveGameFile = ReactiveCommand.Create<Unit, Unit>(SaveGameMethod);
            UpdateSietch = ReactiveCommand.Create<Unit, Unit>(UpdateSietchMethod);
            UpdateTroop = ReactiveCommand.Create<Unit, Unit>(UpdateTroopMethod);
            GameStages = PopulateGameStages();
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
            set
            {
                this.RaiseAndSetIfChanged(ref _currentTroop, value);
                if(value != null)
                {
                    this._currentSietchWithTroop = this.Sietches.FirstOrDefault(x => x.HousedTroopID == value.TroopID);
                    this.RaisePropertyChanged(nameof(CurrentSietchWithTroop));
                }
            }
        }

        private ObservableCollection<SietchViewModel> _sietches = new();

        public ObservableCollection<SietchViewModel> Sietches
        {
            get => _sietches;
            private set => this.RaiseAndSetIfChanged(ref _sietches, value);
        }

        private ObservableCollection<SietchViewModel> _sietchesOfTroop = new();

        public ObservableCollection<SietchViewModel> SietchesWithTroops
        {
            get => _sietchesOfTroop;
            private set => this.RaiseAndSetIfChanged(ref _sietchesOfTroop, value);
        }

        private SietchViewModel? _currentSietchWithTroop;
        public SietchViewModel? CurrentSietchWithTroop
        {
            get => _currentSietchWithTroop;
            private set
            {
                this.RaiseAndSetIfChanged(ref _currentSietchWithTroop, value);
                this._currentTroop = this.Troops.FirstOrDefault(x => x.TroopID == value?.HousedTroopID);
                this.RaisePropertyChanged(nameof(CurrentTroop));
            }
        }

        private ObservableCollection<TroopViewModel> _troops = new();

        public ObservableCollection<TroopViewModel> Troops
        {
            get => _troops;
            private set => this.RaiseAndSetIfChanged(ref _troops, value);
        }

        public ReactiveCommand<Unit, Unit> OpenSaveGame { get; private set; }

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

        private GameStageViewModel? _currentGameStage = new(0, GameStageFinder.FindStage(0));

        public GameStageViewModel? CurrentGameStage
        {
            get => _currentGameStage;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentGameStage, value);
                this.RaisePropertyChanged(nameof(CurrentGameStage));
            }
        }

        public ObservableCollection<GameStageViewModel> GameStages
        {
            get => _gameStages;
            set => this.RaiseAndSetIfChanged(ref _gameStages, value);
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
            var dialog = new OpenFileDialog
            {
                AllowMultiple = false
            };
            var result = await dialog.ShowAsync(_mainWindow);
            if (result.Length > 0)
            {
                _savegameFile = new SaveGameFile(result[0]);
                SpiceVal = _savegameFile.Generals.Spice;
                CharismaVal = _savegameFile.Generals.CharismaGUI;
                ContactDistanceVal = _savegameFile.Generals.ContactDistance;
                CurrentGameStage = GameStages.FirstOrDefault(x => x.GameStage == _savegameFile.Generals.GameStage);
                PopulateTroops(_savegameFile.GetTroops(), _savegameFile.GetSietches());
                PopulateSietches(_savegameFile.GetSietches());
                IsSaveGameLoaded = true;
            }
            return Unit.Default;
        }

        private static ObservableCollection<GameStageViewModel> PopulateGameStages()
        {
            var collection = new ObservableCollection<GameStageViewModel>();
            for (byte i = byte.MinValue; i < byte.MaxValue; i++)
            {
                collection.Add(new GameStageViewModel(i, GameStageFinder.FindStage(i)));
            }
            return collection;
        }

        private void PopulateSietches(List<Sietch> sietches)
        {
            Sietches.Clear();
            foreach (var sietch in sietches)
            {
                Sietches.Add(new SietchViewModel(sietch));
            }
            if (sietches.Any())
            {
                Sietches = new ObservableCollection<SietchViewModel>(Sietches.OrderBy(x => x.RegionName));
                SietchesWithTroops = new ObservableCollection<SietchViewModel>(Sietches.OrderBy(x => x.RegionName).Where(x => Troops.Any(y => y.TroopID == x.HousedTroopID)));
                CurrentSietchWithTroop = SietchesWithTroops.FirstOrDefault();
                CurrentSietch = Sietches.First();
            }
        }

        private void PopulateTroops(List<Troop> troops, List<Sietch> sietches)
        {
            Troops.Clear();
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
            _savegameFile.UpdateCharisma(CharismaVal);
            _savegameFile.UpdateSpice(SpiceVal);
            _savegameFile.UpdateContactDistance(ContactDistanceVal);
            if (CurrentGameStage != null)
            {
                _savegameFile.UpdateGameStage(CurrentGameStage.GameStage);
            }
            _savegameFile.SaveCompressed();
            return Unit.Default;
        }
    }
}