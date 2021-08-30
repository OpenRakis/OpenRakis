namespace DuneEdit2.ViewModels
{
    using DuneEdit2.Models;

    using ReactiveUI;

    public class SietchViewModel : ReactiveObject
    {
        private Sietch _sietch;

        public byte Atomics
        {
            get => _sietch.Atomics;

            set
            {
                _sietch.Atomics = value;
                this.RaisePropertyChanged(nameof(Atomics));
            }
        }

        public bool BattleWon
        {
            get => _sietch.BattleWon;

            set
            {
                _sietch.BattleWon = value;
                this.RaisePropertyChanged(nameof(BattleWon));
            }
        }

        public byte Bulbs
        {
            get => _sietch.Bulbs;

            set
            {
                _sietch.Bulbs = value;
                this.RaisePropertyChanged(nameof(Bulbs));
            }
        }

        public string? Coordinates
        {
            get => _sietch.Coordinates;

            set
            {
                _sietch.Coordinates = value;
                this.RaisePropertyChanged(nameof(Coordinates));
            }
        }

        public bool FremenFound
        {
            get => _sietch.FremenFound;

            set
            {
                _sietch.FremenFound = value;
                this.RaisePropertyChanged(nameof(FremenFound));
            }
        }

        public byte Harvesters
        {
            get => _sietch.Harvesters;

            set
            {
                _sietch.Harvesters = value;
                this.RaisePropertyChanged(nameof(Harvesters));
            }
        }

        public bool HasVegetation
        {
            get => _sietch.HasVegetation;

            set
            {
                _sietch.HasVegetation = value;
                this.RaisePropertyChanged(nameof(HasVegetation));
            }
        }

        public bool HasWindtrap
        {
            get => _sietch.HasWindtrap;

            set
            {
                _sietch.HasWindtrap = value;
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
                this.RaisePropertyChanged(nameof(InBattle));
            }
        }

        public byte Krys
        {
            get => _sietch.Krys;

            set
            {
                _sietch.Krys = value;
                this.RaisePropertyChanged(nameof(Krys));
            }
        }

        public byte LaserGuns
        {
            get => _sietch.LaserGuns;

            set
            {
                _sietch.LaserGuns = value;
                this.RaisePropertyChanged(nameof(LaserGuns));
            }
        }

        public bool NotDiscovered
        {
            get => _sietch.NotDiscovered;

            set
            {
                _sietch.NotDiscovered = value;
                this.RaisePropertyChanged(nameof(NotDiscovered));
            }
        }

        public byte Orni
        {
            get => Orni;

            set
            {
                _sietch.Bulbs = value;
                this.RaisePropertyChanged(nameof(Bulbs));
            }
        }

        public bool Prospected
        {
            get => _sietch.Prospected;

            set
            {
                _sietch.Prospected = value;
                this.RaisePropertyChanged(nameof(Prospected));
            }
        }

        public byte Region
        {
            get => _sietch.Region;

            set
            {
                _sietch.Region = value;
                this.RaisePropertyChanged(nameof(Region));
            }
        }

        public string RegionName => _sietch.RegionName;

        public bool SeeInventory
        {
            get => _sietch.SeeInventory;

            set
            {
                _sietch.SeeInventory = value;
                this.RaisePropertyChanged(nameof(SeeInventory));
            }
        }

        public byte SpiceDensity
        {
            get => _sietch.SpiceDensity;

            set
            {
                _sietch.SpicefieldID = value;
                this.RaisePropertyChanged(nameof(SpicefieldID));
            }
        }

        public byte SpicefieldID
        {
            get => _sietch.SpicefieldID;

            set
            {
                _sietch.SpicefieldID = value;
                this.RaisePropertyChanged(nameof(SpicefieldID));
            }
        }

        public int StartOffset
        {
            get => _sietch.StartOffset;

            set
            {
                _sietch.StartOffset = value;
                this.RaisePropertyChanged(nameof(StartOffset));
            }
        }

        public byte Status
        {
            get => _sietch.Status;

            set
            {
                _sietch.Status = value;
                this.RaisePropertyChanged(nameof(Status));
            }
        }

        public byte SubRegion
        {
            get => _sietch.SubRegion;

            set
            {
                _sietch.SubRegion = value;
                this.RaisePropertyChanged(nameof(SubRegion));
            }
        }

        public byte Water
        {
            get => _sietch.Water;

            set
            {
                _sietch.Water = value;
                this.RaisePropertyChanged(nameof(Water));
            }
        }

        public byte WeirdingMod
        {
            get => _sietch.WeirdingMod;

            set
            {
                _sietch.WeirdingMod = value;
                this.RaisePropertyChanged(nameof(WeirdingMod));
            }
        }

        public SietchViewModel(Sietch sietch) => _sietch = sietch;
    }
}