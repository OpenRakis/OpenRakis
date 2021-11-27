namespace DuneEdit2.ViewModels
{
    using DuneEdit2.Models;
    using DuneEdit2.Parsers;

    using ReactiveUI;

    internal class SmugglerViewModel : ViewModelBase
    {
        private Smuggler _smuggler;

        private bool _hasChanged = false;

        public bool HasChanged
        {
            get => _hasChanged;
            set
            {
                _hasChanged = value;
                this.RaisePropertyChanged(nameof(HasChanged));
            }
        }


        public SmugglerViewModel(Smuggler smuggler) => _smuggler = smuggler;

        public int StartOffset => _smuggler.StartOffset;

        /// <summary>
        /// 1st byte
        /// </summary>
        public byte Region
        {
            get => _smuggler.Region;
            set
            {
                _smuggler.Region = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Region));
            }
        }

        /// <summary>
        /// 2nd byte
        /// </summary>
        public byte WillingnessToHaggle
        {
            get => _smuggler.WillingnessToHaggle;
            set
            {
                _smuggler.WillingnessToHaggle = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(WillingnessToHaggle));
            }
        }

        /// <summary>
        /// 3rd byte - Field C
        /// </summary>
        public byte UnknownByte1
        {
            get => _smuggler.UnknownByte1;
            set
            {
                _smuggler.UnknownByte1 = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(UnknownByte1));
            }
        }

        /// <summary>
        /// 4th byte - Field D
        /// </summary>
        public byte UnknownByte2
        {
            get => _smuggler.UnknownByte2;
            set
            {
                _smuggler.UnknownByte2 = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(UnknownByte2));
            }
        }

        /// <summary>
        /// 5th byte
        /// </summary>
        public byte Harvesters
        {
            get => _smuggler.Harvesters;
            set
            {
                _smuggler.Harvesters = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Harvesters));
            }
        }

        /// <summary>
        /// 6th byte
        /// </summary>
        public byte Ornithopters
        {
            get => _smuggler.Ornithopters;
            set
            {
                _smuggler.Ornithopters = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Ornithopters));
            }
        }

        /// <summary>
        /// 7th byte
        /// </summary>
        public byte KrysKnives
        {
            get => _smuggler.KrysKnives;
            set
            {
                _smuggler.KrysKnives = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(KrysKnives));
            }
        }

        /// <summary>
        /// 8bth byte
        /// </summary>
        public byte LaserGuns
        {
            get => _smuggler.LaserGuns;
            set
            {
                _smuggler.LaserGuns = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(LaserGuns));
            }
        }

        /// <summary>
        /// 9th byte
        /// </summary>
        public byte WeirdingModules
        {
            get => _smuggler.WeirdingModules;
            set
            {
                _smuggler.WeirdingModules = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(WeirdingModules));
            }
        }

        /// <summary>
        /// 10th byte
        /// </summary>
        public byte HarvestersPrice
        {
            get => _smuggler.HarvestersPrice;
            set
            {
                _smuggler.HarvestersPrice = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(HarvestersPrice));
            }
        }

        /// <summary>
        /// 11th byte
        /// </summary>
        public byte OrnithoptersPrice
        {
            get => _smuggler.OrnithoptersPrice;
            set
            {
                _smuggler.OrnithoptersPrice = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(OrnithoptersPrice));
            }
        }

        /// <summary>
        /// 12th byte
        /// </summary>
        public byte KrysKnivesPrice
        {
            get => _smuggler.KrysKnivesPrice;
            set
            {
                _smuggler.KrysKnivesPrice = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(KrysKnivesPrice));
            }
        }

        /// <summary>
        /// 13th byte
        /// </summary>
        public byte LaserGunsPrice
        {
            get => _smuggler.LaserGunsPrice;
            set
            {
                _smuggler.LaserGunsPrice = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(LaserGunsPrice));
            }
        }

        /// <summary>
        /// 14th byte
        /// </summary>
        public byte WeirdingModulesPrice
        {
            get => _smuggler.WeirdingModulesPrice;
            set
            {
                _smuggler.WeirdingModulesPrice = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(WeirdingModulesPrice));
            }
        }

    }
}
