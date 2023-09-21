using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DuneEdit2.ViewModels;

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
using DuneEdit2.Enums;

public partial class MainWindowViewModel : ViewModelBase
{
    private bool _isSavegameLoaded;
    private SaveGameFile _savegameFile = new(SaveFileFormat.DUNE_37);

    public MainWindowViewModel(Window mainWindow)
    {
        MainWindow = mainWindow;
    }

    [RelayCommand]
    private void UpdateTroop()
    {
        if (CurrentTroop != null)
        {
            _savegameFile.UpdateTroop(CurrentTroop.Troop);
        }
    }

    [RelayCommand]
    private void UpdateLocation()
    {
        if (CurrentLocation != null)
        {
            _savegameFile.UpdateLocation(CurrentLocation.Location);
        }
    }

    public bool IsSaveGameLoaded
    {
        get { return Design.IsDesignMode || _isSavegameLoaded; }
        private set => this.RaiseAndSetIfChanged(ref _isSavegameLoaded, value);
    }

    private LocationViewModel? _currentSietch;

    public LocationViewModel? CurrentLocation
    {
        get => _currentSietch;
        set => this.RaiseAndSetIfChanged(ref _currentSietch, value);
    }

    private NPCViewModel? _currentNPC;

    public NPCViewModel? CurrentNPC
    {
        get => _currentNPC;
        set => this.RaiseAndSetIfChanged(ref _currentNPC, value);
    }

    private SmugglerViewModel? _currentSmuggler;
    public SmugglerViewModel? CurrentSmuggler
    {
        get => _currentSmuggler;
        set => this.RaiseAndSetIfChanged(ref _currentSmuggler, value);
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
                this._currentLocationWithTroop = Locations.FirstOrDefault(x => x.ID == value.CurrentLocation?.ID);
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

    private List<NPCViewModel> _NPCs = new();

    public List<NPCViewModel> NPCs
    {
        get => _NPCs;
        private set => this.RaiseAndSetIfChanged(ref _NPCs, value);
    }

    private List<SmugglerViewModel> _smugglers = new();

    public List<SmugglerViewModel> Smugglers
    {
        get => _smugglers;
        private set => this.RaiseAndSetIfChanged(ref _smugglers, value);
    }

    private List<TroopViewModel> _troops = new();

    public List<TroopViewModel> Troops
    {
        get => _troops;
        private set => this.RaiseAndSetIfChanged(ref _troops, value);
    }

    public string GameStageDesc => GameStageFinder.FindStage(GameStage);

    private byte _gameStage = 0;

    private bool _hasChanged = false;
    private byte _numberOfRalliedTroops;

    public bool HasChanged
    {
        get => _hasChanged;
        set => this.RaiseAndSetIfChanged(ref _hasChanged, value);
    }

    public byte NumberOfRalliedTroops
    {
        get => _numberOfRalliedTroops;
        set
        {
            if (_numberOfRalliedTroops != value)
            {
                this.RaiseAndSetIfChanged(ref _numberOfRalliedTroops, value);
                this.RaisePropertyChanged(nameof(GameStageDesc));
                HasChanged = true;
            }
        }
    }

    public byte GameStage
    {
        get => _gameStage;
        set
        {
            if(_gameStage != value)
            {
                this.RaiseAndSetIfChanged(ref _gameStage, value);
                this.RaisePropertyChanged(nameof(GameStageDesc));
                HasChanged = true;
            }
        }
    }

    private int _spiceVal = 0;

    public int SpiceVal
    {
        get => _spiceVal;
        set
        {
            if(_spiceVal != value)
            {
                this.RaiseAndSetIfChanged(ref _spiceVal, value);
                HasChanged = true;
            }
        }
    }

    private byte _charismaVal = 0;

    public byte CharismaVal
    {
        get => _charismaVal;
        set
        {
            if(_charismaVal != value)
            {
                this.RaiseAndSetIfChanged(ref _charismaVal, value);
                HasChanged = true;
            }
        }
    }

    private int _contactDistanceVal = 0;

    public int ContactDistanceVal
    {
        get => _contactDistanceVal;
        set
        {
            if(_contactDistanceVal != value)
            {
                this.RaiseAndSetIfChanged(ref _contactDistanceVal, value);
                HasChanged = true;
            }
        }
    }

    private Window MainWindow { get; set; }

    [RelayCommand]
    private async Task Open37SaveGame()
    {
        var dialog = new OpenFileDialog
        {
            AllowMultiple = false,
        };
        var result = await dialog.ShowAsync(MainWindow);
        if (result != null && result.Length > 0)
        {
            var selectedFile = result[0];
            PopulateVMs(selectedFile, SaveFileFormat.DUNE_37);
            IsSaveGameLoaded = true;
        }
    }

    [RelayCommand]
    private async Task Open38SaveGame()
    {
        var dialog = new OpenFileDialog
        {
            AllowMultiple = false,
        };
        var result = await dialog.ShowAsync(MainWindow);
        if (result != null && result.Length > 0)
        {
            var selectedFile = result[0];
            PopulateVMs(selectedFile, SaveFileFormat.DUNE_38);
            IsSaveGameLoaded = true;
        }
    }

    [RelayCommand]
    private async Task Open21SaveGame()
    {
        var dialog = new OpenFileDialog
        {
            AllowMultiple = false,
        };
        var result = await dialog.ShowAsync(MainWindow);
        if (result != null && result.Length > 0)
        {
            var selectedFile = result[0];
            PopulateVMs(selectedFile, SaveFileFormat.DUNE_21);
            IsSaveGameLoaded = true;
        }
    }

    [RelayCommand]
    private async Task Open23SaveGame()
    {
        var dialog = new OpenFileDialog
        {
            AllowMultiple = false,
        };
        var result = await dialog.ShowAsync(MainWindow);
        if (result != null && result.Length > 0)
        {
            var selectedFile = result[0];
            PopulateVMs(selectedFile, SaveFileFormat.DUNE_23);
            IsSaveGameLoaded = true;
        }
    }

    [RelayCommand]
    private async Task Open24SaveGame()
    {
        var dialog = new OpenFileDialog
        {
            AllowMultiple = false,
        };
        var result = await dialog.ShowAsync(MainWindow);
        if (result != null && result.Length > 0)
        {
            var selectedFile = result[0];
            PopulateVMs(selectedFile, SaveFileFormat.DUNE_24);
            IsSaveGameLoaded = true;
        }
    }

    private void PopulateVMs(string saveFileName, SaveFileFormat format)
    {
        _savegameFile = new SaveGameFile(saveFileName, format);
        SpiceVal = _savegameFile.Generals.Spice;
        CharismaVal = _savegameFile.Generals.CharismaGUI;
        ContactDistanceVal = _savegameFile.Generals.ContactDistance;
        GameStage = _savegameFile.Generals.GameStage;
        NumberOfRalliedTroops = _savegameFile.Generals.NumberOfRalliedTroops;
        PopulateTroops(_savegameFile.GetTroops(), _savegameFile.GetSietches());
        PopulateLocations(_savegameFile.GetSietches());
        PopulateNPCs(_savegameFile.GetNPCs());
        PopulateSmugglers(_savegameFile.GetSmugglers());
        HasChanged = false;
    }

    private void PopulateNPCs(List<NPC> npcs)
    {
        var npcsVMs = npcs.Select(npc => new NPCViewModel(npc)).ToList();
        NPCs = npcsVMs;
        if (NPCs.Any())
        {
            NPCs = new List<NPCViewModel>(NPCs.OrderBy(x => x.Name));
            CurrentNPC = NPCs.First();
        }
    }

    private void PopulateSmugglers(List<Smuggler> smugglers)
    {
        var smugglersVMs = smugglers.Select(smuggler => new SmugglerViewModel(smuggler)).ToList();
        Smugglers = smugglersVMs;
        if (Smugglers.Any())
        {
            Smugglers = new List<SmugglerViewModel>(Smugglers.OrderBy(x => x.RegionName));
            CurrentSmuggler = Smugglers.First();
        }
    }

    private void PopulateLocations(List<Models.Location> locations)
    {
        var locationsVMs = locations.Select(location => new LocationViewModel(location)).ToList();
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
        for (int i = 0; i < troops.Count; i++)
        {
            Troop? troop = troops[i];
            Models.Location? location = GetTroopHousing(troops, locations, troop);
            var troopVM = new TroopViewModel(troop, location);
            troopsVMs.Add(troopVM);
        }
        Troops = troopsVMs;
        if (Troops.Any())
        {
            CurrentTroop = Troops.First();
        }
    }

    private static Models.Location? GetTroopHousing(List<Troop> troops, List<Models.Location> locations, Troop troop)
    {
        IEnumerable<Models.Location>? populatedLocations = locations.Where(x => troops.Any(y => y.TroopID == x.HousedTroopID));
        IOrderedEnumerable<Troop>? orderedTroops = troops.OrderBy(x => x.NextTroopInLocation);
        Models.Location? location = locations.FirstOrDefault(x => x.HousedTroopID == troop.TroopID);
        if (location is null)
        {
            foreach(Models.Location? populatedLocation in populatedLocations)
            {
                Troop? firstTroopOfLocation = orderedTroops.First(x => x.TroopID == populatedLocation.HousedTroopID);
                Troop? nextTroopInSietch = orderedTroops.FirstOrDefault(x => x.TroopID == firstTroopOfLocation.NextTroopInLocation);
                while (nextTroopInSietch != null)
                {
                    if (nextTroopInSietch.TroopID == troop.TroopID)
                    {
                        location = populatedLocation;
                        break;
                    }
                    Troop? newNextNexTroopInSietch = orderedTroops.FirstOrDefault(x => x.TroopID == nextTroopInSietch.NextTroopInLocation);
                    if(newNextNexTroopInSietch == nextTroopInSietch)
                    {
                        break;
                    }
                    nextTroopInSietch = newNextNexTroopInSietch;
                }
                if (location != null)
                {
                    break;
                }
            }
        }

        return location;
    }

    [RelayCommand]
    private async Task SaveGameFile()
    {
        if (IsSaveGameLoaded == false)
        {
            return;
        }
        FileAttributes attributes = File.GetAttributes(_savegameFile.Filename);
        if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
        {
            var fileName = await new SaveFileDialog()
            {
                Directory = Path.GetDirectoryName(_savegameFile.Filename),
                InitialFileName = $"{Path.GetFileNameWithoutExtension(_savegameFile.Filename)}-modified.SAV"
            }.ShowAsync(MainWindow);
            if (string.IsNullOrWhiteSpace(fileName) == false)
            {
                _savegameFile.SaveCompressedAs(fileName);
            }
        }
        else
        {
            _savegameFile.SaveCompressed();

        }
    }

    [RelayCommand]
    private void UpdateGenerals()
    {
        _savegameFile.UpdateCharisma(CharismaVal);
        _savegameFile.UpdateSpice(SpiceVal);
        _savegameFile.UpdateContactDistance(ContactDistanceVal);
        _savegameFile.UpdateNumberOfRalliedTroops(NumberOfRalliedTroops);
        _savegameFile.UpdateGameStage(GameStage);
    }

    [RelayCommand]
    private void UpdateNPC()
    {
        if (CurrentNPC != null)
        {
            _savegameFile.UpdateNPC(CurrentNPC.NPC);
        }
    }

    [RelayCommand]
    private void UpdateSmuggler()
    {
        if (CurrentSmuggler != null)
        {
            _savegameFile.UpdateSmuggler(CurrentSmuggler.Smuggler);
        }
    }
}