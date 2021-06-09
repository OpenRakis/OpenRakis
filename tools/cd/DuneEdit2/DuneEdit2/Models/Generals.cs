namespace DuneEdit2.Models
{
    using DuneEdit2.Parsing;
    using ReactiveUI;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class Generals : ReactiveObject
    {
        public const int CharismaStartOffset = 17480;

        public const int ContactDistanceStartOffset = 21909;

        public const int DateTimeStartOffset = 21907;

        public const int GameStageOffset = 17481;

        public const int SpiceStartOffset = 17599;

        private readonly byte[] _date = new byte[] { 0 };

        private readonly List<byte> _sg = new List<byte>();

        private byte _charisma;

        private int _contactDistance;

        private byte _gameStage;

        private byte[] _spice = new byte[] { 0 };

        public Generals()
        {
        }

        public Generals(List<byte> sg)
        {
            _spice = new byte[2] { 0, 0 };
            _sg = sg;
            _spice = new byte[2]
            {
                _sg[SpiceStartOffset],
                _sg[SpiceStartOffset + 1]
            };
            _gameStage = _sg[GameStageOffset];
            _contactDistance = _sg[ContactDistanceStartOffset];
            _date = new byte[2]
            {
                _sg[DateTimeStartOffset],
                _sg[DateTimeStartOffset + 1]
            };
            _charisma = _sg[CharismaStartOffset];
        }

        public static string DateGUI => "??";

        public byte Charisma
        {
            get
            {
                return _charisma;
            }

            set
            {
                _charisma = value;
            }
        }

        public byte CharismaGUI
        {
            get => Convert.ToByte(_charisma <= 1 ? 0 : _charisma / 2.0);

            set
            {
                checked
                {
                    var newValue = (byte)(unchecked(value) * 2);
                    this.RaiseAndSetIfChanged(ref _charisma, newValue);
                }
            }
        }

        public int ContactDistanceGUI
        {
            get
            {
                return int.Parse(_contactDistance.ToString("X"), NumberStyles.HexNumber);
            }

            set
            {
                this.RaiseAndSetIfChanged(ref _contactDistance, value);
            }
        }

        public string DateAsHex => $"{_date[0]:X2}{_date[1]:X2}";

        public string GameStageAsHex => $"{_gameStage:X2}";

        public string SpiceAsHex => $"{_spice[0]:X2}{_spice[1]:X2}";

        public int SpiceGUI
        {
            get
            {
                if (_spice.Length < 2)
                {
                    return 0;
                }
                string s = GetSpiceAsHexReversed();
                int num = int.Parse(s, NumberStyles.HexNumber);
                return checked(num * 10);
            }

            set
            {
                _spice = new byte[2] { 0, 0 };
                string s = checked((int)Math.Round((double)value / 10.0)).ToString("X");
                var newValue = SequenceParser.SplitTwo(s);
                this.RaiseAndSetIfChanged(ref _spice, newValue);
            }
        }

        public string GetGameStageExplained() => GameStageFinder.FindStage(_gameStage);

        public void Update(int spiceVal, int contactDistValue, byte charisma)
        {
            SpiceGUI = spiceVal;
            ContactDistanceGUI = contactDistValue;
            CharismaGUI = charisma;
            _sg[SpiceStartOffset] = _spice[0];
            if (_spice.Length > 1)
            {
                _sg[SpiceStartOffset + 1] = _spice[1];
            }
            _sg[ContactDistanceStartOffset] = checked((byte)int.Parse(ContactDistanceGUI.ToString(), NumberStyles.Integer));
            _sg[CharismaStartOffset] = _charisma;
        }

        private string GetSpiceAsHexReversed() => $"{_spice[1]:X}{_spice[0]:X}";
    }
}