namespace DuneEdit2.Models
{
    using DuneEdit2.Parsing;

    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class Generals
    {
        private const int SpiceStartOffset = 17599;

        private const int CharismaStartOffset = 17480;

        private const int ContactDistanceStartOffset = 21909;

        private byte[] _spice;

        private int _contactDistance;

        private byte _charisma;

        private readonly List<byte> _sg;

        public byte CharismaGUI
        {
            get => Convert.ToByte(_charisma <= 1 ? 0 : _charisma / 2.0);
            set
            {
                checked
                {
                    _charisma = (byte)(unchecked((int)value) * 2);
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
                string s = _spice[1].ToString("X") + _spice[0].ToString("X");
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
                _sg[17599],
                _sg[17600]
            };
            _contactDistance = _sg[21909];
            _charisma = _sg[17480];
        }

        public void Update(int spiceVal, int contactDistValue, byte charisma)
        {
            Spice = spiceVal;
            ContactDistance = contactDistValue;
            CharismaGUI = charisma;
            _sg[17599] = _spice[0];
            if (_spice.Length > 1)
            {
                _sg[17600] = _spice[1];
            }
            _sg[21909] = checked((byte)int.Parse(ContactDistance.ToString(), NumberStyles.Integer));
            _sg[17480] = _charisma;
        }
    }
}