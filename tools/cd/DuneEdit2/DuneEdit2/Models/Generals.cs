namespace DuneEdit2.Models
{
    using DuneEdit2.Parsing;

    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class Generals
    {
        public const int SpiceStartOffset = 17599;

        public const int CharismaStartOffset = 17480;

        public const int ContactDistanceStartOffset = 21909;

        public const int DateTimeStartOffset = 21907;

        private byte[] _spice;

        private int _contactDistance;

        private readonly byte[] _date;

        private byte _charisma;

        private readonly List<byte> _sg;

        public byte CharismaGUI
        {
            get => Convert.ToByte(_charisma <= 1 ? 0 : _charisma / 2.0);
            set
            {
                checked
                {
                    _charisma = (byte)(unchecked(value) * 2);
                }
            }
        }

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

        public int Spice
        {
            get
            {
                string s = GetSpiceAsHexReversed();
                int num = int.Parse(s, NumberStyles.HexNumber);
                return checked(num * 10);
            }
            set
            {
                _spice = new byte[2] { 0, 0 };
                string s = checked((int)Math.Round((double)value / 10.0)).ToString("X");
                _spice = SequenceParser.SplitTwo(s);
            }
        }

        private string GetSpiceAsHexReversed() => $"{_spice[1]:X}{_spice[0]:X}";

        public string SpiceAsHex => $"{_spice[0]:X2}{_spice[1]:X2}";

        public int Date
        {
            get
            {
                string d = DateAsHex;
                int num = int.Parse(d, NumberStyles.HexNumber);
                return num;
            }
        }

        public string DateAsHex => $"{_date[0]:X2}{_date[1]:X2}";

        public static string DateGUI => "??";

        public int ContactDistance
        {
            get
            {
                return int.Parse(_contactDistance.ToString("X"), NumberStyles.HexNumber);
            }
            set
            {
                _contactDistance = value;
            }
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
            _contactDistance = _sg[ContactDistanceStartOffset];
            _date = new byte[2]
            {
                _sg[DateTimeStartOffset],
                _sg[DateTimeStartOffset + 1]
            };
            _charisma = _sg[CharismaStartOffset];
        }

        public void Update(int spiceVal, int contactDistValue, byte charisma)
        {
            Spice = spiceVal;
            ContactDistance = contactDistValue;
            CharismaGUI = charisma;
            _sg[SpiceStartOffset] = _spice[0];
            if (_spice.Length > 1)
            {
                _sg[SpiceStartOffset + 1] = _spice[1];
            }
            _sg[ContactDistanceStartOffset] = checked((byte)int.Parse(ContactDistance.ToString(), NumberStyles.Integer));
            _sg[CharismaStartOffset] = _charisma;
        }
    }
}