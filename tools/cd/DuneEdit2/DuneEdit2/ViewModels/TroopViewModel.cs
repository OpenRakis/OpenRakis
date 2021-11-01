﻿namespace DuneEdit2.ViewModels
{

    using DuneEdit2.Models;
    using DuneEdit2.Parsers;

    using ReactiveUI;

    public partial class TroopViewModel : ViewModelBase
    {
        private readonly Location? _sietch;

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

        private readonly Troop _troop;

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
                this.RaisePropertyChanged(nameof(Equipement));
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
                this.RaisePropertyChanged(nameof(Equipement));
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

        public byte Job
        {
            get => _troop.Job;
            set
            {
                _troop.Job = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Job));
                this.RaisePropertyChanged(nameof(JobDesc));
                this.RaisePropertyChanged(nameof(Description));
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
                this.RaisePropertyChanged(nameof(Origin));
            }
        }

        public string JobDesc => $"{Job} - {JobFinder.GetJobDesc(Job)}";

        public string DissatisfactionDesc => $"{Dissatisfaction} - {DissatisfactionFinder.GetStatusDesc(Dissatisfaction)}";

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
                this.RaisePropertyChanged(nameof(Equipement));
            }
        }

        public bool KrysKnives
        {
            get => _troop.KrysKnives;
            set
            {
                _troop.KrysKnives = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(KrysKnives));
                this.RaisePropertyChanged(nameof(Equipement));
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
                this.RaisePropertyChanged(nameof(Equipement));
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

        public byte NextTroopInLocation
        {
            get => _troop.NextTroopInSietch;

            set
            {
                _troop.NextTroopInSietch = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(NextTroopInLocation));
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
                this.RaisePropertyChanged(nameof(Equipement));
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
                this.RaisePropertyChanged(nameof(SpeechDesc));
            }
        }

        public string SpeechDesc => SpeechFinder.GetSpeechDesc(Speech);

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
                this.RaisePropertyChanged(nameof(Equipement));
            }
        }

        private string GetSietchName()
        {
            var name = "no sietch ID";
            if (_sietch != null && string.IsNullOrWhiteSpace(_sietch.RegionName) == false)
            {
                return _sietch.RegionName;
            }
            return name;
        }

        public string Origin => TroopOriginFinder.GetOrigin(_troop.Dissatisfaction);

        public string Description => $"{_troop.TroopID} ({GetSietchName()}) ({GetFaction()})";

        public TroopViewModel(Troop troop, Location? sietch)
        {
            _troop = troop;
            _sietch = sietch;
        }

        private string GetFaction()
        {
            if (IsFremen())
            {
                return "Fremen";
            }
            else
            {
                return "Harkonnen";
            }
        }

        private bool IsFremen()
        {
            if(_troop.Job > 0xA0)
            {
                return true;
            }
            var computedValue = _troop.Job & 0xF;
            if(computedValue < 0xC)
            {
                return true;
            }
            return false;
        }

        public int Equipement
        {
            get => _troop.Equipment;
            set
            {
                _troop.Equipment = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(Equipement));
                this.RaisePropertyChanged(nameof(Weirdings));
                this.RaisePropertyChanged(nameof(Atomics));
                this.RaisePropertyChanged(nameof(LaserGuns));
                this.RaisePropertyChanged(nameof(KrysKnives));
                this.RaisePropertyChanged(nameof(Ornithopters));
                this.RaisePropertyChanged(nameof(Harvesters));
                this.RaisePropertyChanged(nameof(Bulbs));
            }
        }
    }
}