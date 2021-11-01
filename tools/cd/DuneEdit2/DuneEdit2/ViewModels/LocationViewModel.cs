namespace DuneEdit2.ViewModels
{
    using DuneEdit2.Models;
    using DuneEdit2.Parsers;

    using ReactiveUI;

    public class LocationViewModel : ViewModelBase
    {
        private readonly Location _location;

        private bool _hasChanged;

        public bool HasChanged
        {
            get => _hasChanged;
            private set
            {
                _hasChanged = value;
                this.RaisePropertyChanged(nameof(HasChanged));
            }
        }

        public Location Sietch => _location;

        public byte Atomics
        {
            get => _location.Atomics;

            set
            {
                _location.Atomics = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Atomics));
            }
        }

        public bool BattleWon
        {
            get => _location.BattleWon;

            set
            {
                _location.BattleWon = value;
                HasChanged = true;
                RaiseStatusChanged();
            }
        }

        public byte Bulbs
        {
            get => _location.Bulbs;

            set
            {
                _location.Bulbs = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Bulbs));
            }
        }

        public string? Coordinates => _location.Coordinates;

        public bool Infiltrated
        {
            get => _location.Infiltrated;

            set
            {
                _location.Infiltrated = value;
                HasChanged = true;
                RaiseStatusChanged();
            }
        }

        public byte Harvesters
        {
            get => _location.Harvesters;

            set
            {
                _location.Harvesters = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Harvesters));
            }
        }

        public bool HasVegetation
        {
            get => _location.HasVegetation;

            set
            {
                _location.HasVegetation = value;
                HasChanged = true;
                RaiseStatusChanged();
            }
        }

        public bool HasWindtrap
        {
            get => _location.HasWindtrap;

            set
            {
                _location.HasWindtrap = value;
                HasChanged = true;
                RaiseStatusChanged();
            }
        }

        public byte HousedTroopID
        {
            get => _location.HousedTroopID;
            set
            {
                _location.HousedTroopID = value;
                HasChanged = true;
            }
        }

        public string ID => _location.ID;

        public bool InBattle
        {
            get => _location.InBattle;

            set
            {
                _location.InBattle = value;
                HasChanged = true;
                RaiseStatusChanged();
            }
        }

        public byte Krys
        {
            get => _location.Krys;

            set
            {
                _location.Krys = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Krys));
            }
        }

        public byte LaserGuns
        {
            get => _location.LaserGuns;

            set
            {
                _location.LaserGuns = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(LaserGuns));
            }
        }

        public bool NotDiscovered
        {
            get => _location.NotDiscovered;

            set
            {
                _location.NotDiscovered = value;
                HasChanged = true;
                RaiseStatusChanged();
            }
        }

        public byte Ornis
        {
            get => _location.Ornis;

            set
            {
                _location.Ornis = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Ornis));
            }
        }

        public bool Prospected
        {
            get => _location.Prospected;

            set
            {
                _location.Prospected = value;
                HasChanged = true;
                RaiseStatusChanged();
            }
        }

        public byte Region => _location.Region;

        public string RegionName => _location.RegionName;

        public bool SeeInventory
        {
            get => _location.SeeInventory;

            set
            {
                _location.SeeInventory = value;
                HasChanged = true;
                RaiseStatusChanged();
            }
        }

        public byte SpiceDensity
        {
            get => _location.SpiceDensity;

            set
            {
                _location.SpiceDensity = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(SpiceDensity));
            }
        }

        public byte SpicefieldID => _location.SpicefieldID;

        public int StartOffset => _location.StartOffset;

        public int Status
        {
            get => _location.Status;
            set
            {
                _location.Status = value;
                HasChanged = true;
                RaiseStatusChanged();
            }
        }

        public byte Appearance
        {
            get => _location.Appearance;
            set
            {
                _location.Appearance = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Appearance));
            }
        }

        public byte PosXmap
        {
            get => _location.PosXmap;
            set
            {
                _location.PosXmap = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(PosXmap));
            }
        }

        public byte PosYmap
        {
            get => _location.PosYmap;
            set
            {
                _location.PosYmap = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(PosYmap));
            }
        }

        public byte PosY
        {
            get => _location.PosY;
            set
            {
                _location.PosY = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(PosY));
            }
        }

        public byte PosX
        {
            get => _location.PosX;
            set
            {
                _location.PosX = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(PosX));
            }
        }

        public byte Unknown1
        {
            get => _location.Unknown1;
            set
            {
                _location.Unknown1 = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Unknown1));
            }
        }

        public byte GameStage
        {
            get => _location.GameStage;
            set
            {
                _location.GameStage = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(GameStage));
            }
        }

        public byte Unknown3
        {
            get => _location.Unknown3;
            set
            {
                _location.Unknown3 = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Unknown3));
            }
        }

        public byte Unknown4
        {
            get => _location.Unknown4;
            set
            {
                _location.Unknown4 = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Unknown4));
            }
        }

        public byte Unknown5
        {
            get => _location.Unknown5;
            set
            {
                _location.Unknown5 = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Unknown5));
            }
        }

        public byte Unknown6
        {
            get => _location.Unknown6;
            set
            {
                _location.Unknown6 = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Unknown6));
            }
        }

        public byte Unknown2
        {
            get => _location.Unknown2;
            set
            {
                _location.Unknown2 = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Unknown2));
            }
        }

        private void RaiseStatusChanged()
        {
            this.RaisePropertyChanged(nameof(Status));
            this.RaisePropertyChanged(nameof(HasVegetation));
            this.RaisePropertyChanged(nameof(InBattle));
            this.RaisePropertyChanged(nameof(BattleWon));
            this.RaisePropertyChanged(nameof(Infiltrated));
            this.RaisePropertyChanged(nameof(SeeInventory));
            this.RaisePropertyChanged(nameof(HasWindtrap));
            this.RaisePropertyChanged(nameof(Prospected));
            this.RaisePropertyChanged(nameof(NotDiscovered));
            this.RaisePropertyChanging(nameof(StatusDesc));
        }

        public string StatusDesc => LocationStatusFinder.GetSietchStatusDescription(Status);

        public string? Faction { get; set; }

        public byte SubRegion => _location.SubRegion;

        public byte Water
        {
            get => _location.Water;

            set
            {
                _location.Water = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Water));
            }
        }

        public byte Spice
        {
            get => _location.Spice;

            set
            {
                _location.Spice = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Spice));
            }
        }

        public byte WeirdingMod
        {
            get => _location.WeirdingMod;

            set
            {
                _location.WeirdingMod = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(WeirdingMod));
            }
        }

        public LocationViewModel(Location location) => _location = location;
    }
}