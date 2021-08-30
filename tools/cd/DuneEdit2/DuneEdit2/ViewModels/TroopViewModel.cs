namespace DuneEdit2.ViewModels
{
    using System;

    using DuneEdit2.Models;
    using DuneEdit2.Parsers;

    using ReactiveUI;

    public class TroopViewModel : ReactiveObject
    {
        private Troop _troop;

        public byte ArmySkill
        {
            get => _troop.ArmySkill;

            set
            {
                _troop.ArmySkill = value;
                this.RaisePropertyChanged(nameof(ArmySkill));
            }
        }

        public bool Atomics
        {
            get => _troop.Atomics;
            set
            {
                _troop.Atomics = value;
                this.RaisePropertyChanged(nameof(Atomics));
            }
        }

        public bool Bulbs
        {
            get => _troop.Bulbs;
            set
            {
                _troop.Bulbs = value;
                this.RaisePropertyChanged(nameof(Bulbs));
            }
        }

        public string? Coordinates
        {
            get => _troop.Coordinates;

            set
            {
                _troop.Coordinates = value;
                this.RaisePropertyChanged(nameof(Coordinates));
            }
        }

        public byte Dissatisfaction
        {
            get => _troop.Dissatisfaction;

            set
            {
                _troop.Dissatisfaction = value;
                this.RaisePropertyChanged(nameof(Dissatisfaction));
            }
        }

        public byte EcologySkill
        {
            get => _troop.EcologySkill;

            set
            {
                _troop.EcologySkill = value;
                this.RaisePropertyChanged(nameof(EcologySkill));
            }
        }

        public bool Harvesters
        {
            get => _troop.Harvesters;
            set
            {
                _troop.Harvesters = value;
                this.RaisePropertyChanged(nameof(Harvesters));
            }
        }

        public byte Job
        {
            get => _troop.Job;

            set
            {
                _troop.Job = value;
                this.RaisePropertyChanged(nameof(Job));
            }
        }

        public bool KrysKnives
        {
            get => _troop.KrysKnives;
            set
            {
                _troop.KrysKnives = value;
                this.RaisePropertyChanged(nameof(KrysKnives));
            }
        }

        public bool LaserGuns
        {
            get => _troop.LaserGuns;
            set
            {
                _troop.LaserGuns = value;
                this.RaisePropertyChanged(nameof(LaserGuns));
            }
        }

        public byte Motivation
        {
            get => _troop.Motivation;

            set
            {
                _troop.Motivation = value;
                this.RaisePropertyChanged(nameof(Motivation));
            }
        }

        public byte NextTroopInSietch
        {
            get => _troop.NextTroopInSietch;

            set
            {
                _troop.NextTroopInSietch = value;
                this.RaisePropertyChanged(nameof(NextTroopInSietch));
            }
        }

        public bool Ornithopters
        {
            get => _troop.Ornithopters;
            set
            {
                _troop.Ornithopters = value;
                this.RaisePropertyChanged(nameof(Ornithopters));
            }
        }

        public int Population
        {
            get => _troop.Population;

            set
            {
                _troop.Population = value;
                this.RaisePropertyChanged(nameof(Population));
            }
        }

        public byte Speech
        {
            get => _troop.Speech;

            set
            {
                _troop.Speech = value;
                this.RaisePropertyChanged(nameof(Speech));
            }
        }

        public byte SpiceSkill
        {
            get => _troop.SpiceSkill;

            set
            {
                _troop.SpiceSkill = value;
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
                this.RaisePropertyChanged(nameof(Weirdings));
            }
        }

        public TroopViewModel(Troop troop) => _troop = troop;
    }
}