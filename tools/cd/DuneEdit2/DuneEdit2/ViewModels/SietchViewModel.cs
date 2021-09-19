namespace DuneEdit2.ViewModels
{
    using DuneEdit2.Models;

    using ReactiveUI;

    public class SietchViewModel : ViewModelBase
    {
        private readonly Sietch _sietch;

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

        public Sietch Sietch => _sietch;

        public byte Atomics
        {
            get => _sietch.Atomics;

            set
            {
                _sietch.Atomics = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Atomics));
            }
        }

        public bool BattleWon
        {
            get => _sietch.BattleWon;

            set
            {
                _sietch.BattleWon = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(BattleWon));
            }
        }

        public byte Bulbs
        {
            get => _sietch.Bulbs;

            set
            {
                _sietch.Bulbs = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Bulbs));
            }
        }

        public string? Coordinates => _sietch.Coordinates;

        public bool FremenFound
        {
            get => _sietch.FremenFound;

            set
            {
                _sietch.FremenFound = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(FremenFound));
            }
        }

        public byte Harvesters
        {
            get => _sietch.Harvesters;

            set
            {
                _sietch.Harvesters = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Harvesters));
            }
        }

        public bool HasVegetation
        {
            get => _sietch.HasVegetation;

            set
            {
                _sietch.HasVegetation = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(HasVegetation));
            }
        }

        public bool HasWindtrap
        {
            get => _sietch.HasWindtrap;

            set
            {
                _sietch.HasWindtrap = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(HasWindtrap));
            }
        }

        public byte HousedTroopID => _sietch.HousedTroopID;

        public string ID => _sietch.ID;

        public bool InBattle
        {
            get => _sietch.InBattle;

            set
            {
                _sietch.InBattle = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(InBattle));
            }
        }

        public byte Krys
        {
            get => _sietch.Krys;

            set
            {
                _sietch.Krys = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Krys));
            }
        }

        public byte LaserGuns
        {
            get => _sietch.LaserGuns;

            set
            {
                _sietch.LaserGuns = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(LaserGuns));
            }
        }

        public bool NotDiscovered
        {
            get => _sietch.NotDiscovered;

            set
            {
                _sietch.NotDiscovered = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(NotDiscovered));
            }
        }

        public byte Ornis
        {
            get => _sietch.Ornis;

            set
            {
                _sietch.Bulbs = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Ornis));
            }
        }

        public bool Prospected
        {
            get => _sietch.Prospected;

            set
            {
                _sietch.Prospected = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Prospected));
            }
        }

        public byte Region => _sietch.Region;

        public string RegionName => _sietch.RegionName;

        public bool SeeInventory
        {
            get => _sietch.SeeInventory;

            set
            {
                _sietch.SeeInventory = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(SeeInventory));
            }
        }

        public byte SpiceDensity
        {
            get => _sietch.SpiceDensity;

            set
            {
                _sietch.SpiceDensity = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(SpiceDensity));
            }
        }

        public byte SpicefieldID => _sietch.SpicefieldID;

        public int StartOffset => _sietch.StartOffset;

        public byte Status => _sietch.Status;

        public byte SubRegion => _sietch.SubRegion;

        public byte Water
        {
            get => _sietch.Water;

            set
            {
                _sietch.Water = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Water));
            }
        }

        public byte WeirdingMod
        {
            get => _sietch.WeirdingMod;

            set
            {
                _sietch.WeirdingMod = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(WeirdingMod));
            }
        }

        public SietchViewModel(Sietch sietch) => _sietch = sietch;
    }
}