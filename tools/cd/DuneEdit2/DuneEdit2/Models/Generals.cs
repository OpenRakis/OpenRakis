namespace DuneEdit2.Models
{
    using DuneEdit2.Enums;
    using DuneEdit2.Parsers;

    using ReactiveUI;

    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class Generals : ReactiveObject
    {
        private readonly byte[] _date = new byte[] { 0 };

        private readonly List<byte> _uncompressedData = new List<byte>();

        private byte _charisma;

        private int _contactDistance;

        private byte _gameStage;

        private byte[] _spice = new byte[] { 0 };

        public Generals()
        {
        }

        public Generals(List<byte> uncompressedData)
        {
            _spice = new byte[2] { 0, 0 };
            _uncompressedData = uncompressedData;
            _spice = new byte[2]
            {
                _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.Spice)],
                _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.Spice) + 1]
            };
            _gameStage = _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.GameStage)];
            _contactDistance = _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.ContactDistance)];
            _date = new byte[2]
            {
                _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.DateTime)],
                _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.DateTime) + 1]
            };
            _charisma = _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.Charisma)];
        }

        public static string DateGUI => "??";

        public byte Charisma
        {
            get => _charisma;

            set => _charisma = value;
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
            get => int.Parse(_contactDistance.ToString("X"), NumberStyles.HexNumber);

            set => this.RaiseAndSetIfChanged(ref _contactDistance, value);
        }

        public string DateAsHex => $"{_date[0]:X2}{_date[1]:X2}";

        public string GameStageAsHex => $"{_gameStage:X2}";

        public string SpiceAsHex => $"{_spice[0]:X2}{_spice[1]:X2}";

        public int SpiceGUI
        {
            get
            {
                string s = _spice[1].ToString("X") + _spice[0].ToString("X");
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
            _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.Spice)] = _spice[0];
            if (_spice.Length > 1)
            {
                _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.Spice) + 1] = _spice[1];
            }
            _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.ContactDistance)] = checked((byte)int.Parse(ContactDistanceGUI.ToString(), NumberStyles.Integer));
            _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.Charisma)] = _charisma;
        }

        private string GetSpiceAsHexReversed() => $"{_spice[1]:X}{_spice[0]:X}";
    }
}