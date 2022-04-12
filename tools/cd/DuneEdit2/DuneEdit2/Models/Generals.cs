namespace DuneEdit2.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using DuneEdit2.Enums;
    using DuneEdit2.Parsers;

    public record Generals
    {
        private readonly byte[] _dateAndTime = new byte[] { 0, 0 };

        private byte _charisma;

        private readonly int _contactDistance;

        private readonly byte _gameStage;

        private readonly byte[] _spice = new byte[] { 0 };
        private readonly byte _numberOfRalliedTroops;

        public Generals()
        {
        }

        public Generals(List<byte> uncompressedData, ISaveGameOffsets offsets)
        {
            _spice = new byte[2]
            {
                uncompressedData[offsets.Spice],
                uncompressedData[offsets.Spice + 1]
            };
            _numberOfRalliedTroops = uncompressedData[offsets.NumberOfRalliedTroops];
            _gameStage = uncompressedData[offsets.GameStage];
            _contactDistance = uncompressedData[offsets.ContactDistance];
            _dateAndTime = new byte[2]
            {
                uncompressedData[offsets.DateTime],
                uncompressedData[offsets.DateTime + 1]
            };
            _charisma = uncompressedData[offsets.Charisma];
        }

        public static string DateGUI => "??";

        public byte CharismaByte
        {
            get => _charisma;
        }

        public byte CharismaGUI
        {
            get => Convert.ToByte(_charisma <= 1 ? 0 : _charisma / 2.0);

            set
            {
                checked
                {
                    var newValue = value * 2;
                    if (newValue > 0)
                    {
                        _charisma = (byte)newValue;
                    }
                    else
                    {
                        _charisma = 0;
                    }
                }
            }
        }

        public int ContactDistance => int.Parse(_contactDistance.ToString("X"), NumberStyles.HexNumber);

        public string DateAsHex => $"{_dateAndTime[0]:X2}{_dateAndTime[1]:X2}";

        public string GameStageAsHex => $"{_gameStage:X2}";

        public byte NumberOfRalliedTroops => _numberOfRalliedTroops;

        public byte GameStage => _gameStage;

        public string SpiceAsHex => $"{_spice[0]:X2}{_spice[1]:X2}";

        public int Spice
        {
            get
            {
                string s = _spice[1].ToString("X") + _spice[0].ToString("X");
                int num = int.Parse(s, NumberStyles.HexNumber);
                return checked(num * 10);
            }
        }

        public string GetGameStageDesc() => GameStageFinder.FindStage(_gameStage);
    }
}