namespace DuneEdit2.ViewModels
{
    using DuneEdit2.Models;
    using DuneEdit2.Parsers;

    using ReactiveUI;

    public class TroopViewModel : ReactiveObject
    {
        private Sietch? _sietch;

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

        private Troop _troop;

        public Troop Troop => _troop;

        public byte ArmySkill
        {
            get => _troop.ArmySkill;

            set
            {
                _troop.ArmySkill = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(ArmySkill));
            }
        }

        public bool Atomics
        {
            get => _troop.Atomics;
            set
            {
                _troop.Atomics = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Atomics));
            }
        }

        public bool Bulbs
        {
            get => _troop.Bulbs;
            set
            {
                _troop.Bulbs = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Bulbs));
            }
        }

        public string? Coordinates
        {
            get => _troop.Coordinates;

            set
            {
                _troop.Coordinates = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Coordinates));
            }
        }

        public byte Dissatisfaction
        {
            get => _troop.Dissatisfaction;

            set
            {
                _troop.Dissatisfaction = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Dissatisfaction));
                this.RaisePropertyChanged(nameof(DissatisfactionDesc));
            }
        }

        public string DissatisfactionDesc => SpeechFinder.GetSpeechDesc(_troop.Dissatisfaction);

        public byte EcologySkill
        {
            get => _troop.EcologySkill;

            set
            {
                _troop.EcologySkill = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(EcologySkill));
            }
        }

        public bool Harvesters
        {
            get => _troop.Harvesters;
            set
            {
                _troop.Harvesters = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Harvesters));
            }
        }

        public byte Job
        {
            get => _troop.Job;

            set
            {
                _troop.Job = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Job));
                this.RaisePropertyChanged(nameof(JobDesc));
            }
        }

        public string JobDesc => JobFinder.GetJobDesc(_troop.Job);

        public bool KrysKnives
        {
            get => _troop.KrysKnives;
            set
            {
                _troop.KrysKnives = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(KrysKnives));
            }
        }

        public bool LaserGuns
        {
            get => _troop.LaserGuns;
            set
            {
                _troop.LaserGuns = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(LaserGuns));
            }
        }

        public byte Motivation
        {
            get => _troop.Motivation;

            set
            {
                _troop.Motivation = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Motivation));
            }
        }

        public byte NextTroopInSietch
        {
            get => _troop.NextTroopInSietch;

            set
            {
                _troop.NextTroopInSietch = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(NextTroopInSietch));
            }
        }

        public bool Ornithopters
        {
            get => _troop.Ornithopters;
            set
            {
                _troop.Ornithopters = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Ornithopters));
            }
        }

        public int Population
        {
            get => _troop.Population;

            set
            {
                _troop.Population = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Population));
            }
        }

        public byte Speech
        {
            get => _troop.Speech;

            set
            {
                _troop.Speech = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Speech));
            }
        }

        public byte SpiceSkill
        {
            get => _troop.SpiceSkill;

            set
            {
                _troop.SpiceSkill = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(SpiceSkill));
            }
        }

        public int StartOffset => _troop.StartOffset;

        public string TroopDesc => _troop.TroopDesc;

        public byte TroopID => _troop.TroopID;

        public bool Weirdings
        {
            get => _troop.Weirdings;
            set
            {
                _troop.Weirdings = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Weirdings));
            }
        }

        public string CurrentSietch => GetSietchName();

        private string GetSietchName()
        {
            var name = "no sietch ID";
            if (_sietch != null && string.IsNullOrWhiteSpace(_sietch.RegionName) == false)
            {
                return _sietch.RegionName;
            }
            return name;
        }

        public string Description => $"{_troop.TroopID} ({GetSietchName()})";

        public TroopViewModel(Troop troop, Sietch? sietch)
        {
            _troop = troop;
            _sietch = sietch;
        }
    }
}