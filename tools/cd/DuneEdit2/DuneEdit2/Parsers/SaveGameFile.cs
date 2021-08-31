namespace DuneEdit2.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using DuneEdit2.Enums;
    using DuneEdit2.Models;

    public class SaveGameFile
    {
        private readonly string _fileName = "";

        private readonly List<byte> _original = new();

        private List<byte> _compressedData = new();

        private List<Control> _control = new();

        private Generals _generals = new Generals();

        private List<Trap> _traps = new();

        private List<byte> _uncompressedData = new();

        private List<Sietch> _sietches = new();

        private List<Troop> _troops = new();

        public SaveGameFile()
        {
        }

        public SaveGameFile(List<byte> data, bool isCompressed = true)
        {
            if (isCompressed)
            {
                _compressedData = data;
            }
            else
            {
                _uncompressedData = data;
            }
        }

        public SaveGameFile(string fileName)
        {
            _fileName = fileName;
            try
            {
                _original = new List<byte>();
                using FileStream fileStream = new(fileName, FileMode.Open)
                {
                    Position = 0L
                };
                while (fileStream.Position < fileStream.Length)
                {
                    _original.Add(checked((byte)fileStream.ReadByte()));
                }
                DetectTraps();
                UncompressData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
            }
            _generals = new Generals(_uncompressedData);
            _sietches = PopulateSietches(_uncompressedData);
            _troops = PopulateTroops(_uncompressedData);
        }

        private List<Sietch> PopulateSietches(List<byte> data)
        {
            var sietches = new List<Sietch>();
            int num = 0;
            checked
            {
                int position;
                int endPos;
                do
                {
                    int itemPos = SaveGameIndex.GetFieldStartPos(FieldName.Sietchs) + num * 28;
                    var sietch = new Sietch(itemPos, data[itemPos + 0], data[itemPos + 1], data[itemPos + 9], data[itemPos + 10], data[itemPos + 16], data[itemPos + 18], data[itemPos + 20], data[itemPos + 21], data[itemPos + 22], data[itemPos + 23], data[itemPos + 24], data[itemPos + 25], data[itemPos + 26], data[itemPos + 27]);
                    int coordsCursor = 0;
                    int coordsPos;
                    do
                    {
                        sietch.Coordinates += Convert.ToString(data[itemPos + 2 + coordsCursor]);
                        coordsCursor++;
                        coordsPos = coordsCursor;
                        endPos = 3;
                    }
                    while (coordsPos <= endPos);
                    sietches.Add(sietch);
                    num++;
                    position = num;
                    endPos = 69;
                }
                while (position <= endPos);
            }
            return sietches;
        }

        private List<Troop> PopulateTroops(List<byte> data)
        {
            var troops = new List<Troop>();
            int num = 0;
            checked
            {
                int position;
                int endPos;
                do
                {
                    int itemPos = SaveGameIndex.GetFieldStartPos(FieldName.Troops) + num * 27;
                    var troop = new Troop(data[itemPos + 25])
                    {
                        StartOffset = itemPos,
                        TroopID = data[itemPos + 0],
                        NextTroopInSietch = data[itemPos + 1],
                        Job = data[itemPos + 3],
                        Dissatisfaction = data[itemPos + 18],
                        Speech = data[itemPos + 19],
                        Motivation = data[itemPos + 21],
                        SpiceSkill = data[itemPos + 22],
                        ArmySkill = data[itemPos + 23],
                        EcologySkill = data[itemPos + 24],
                        Equipment = data[itemPos + 25],
                        Population = unchecked(data[checked(itemPos + 26)]) * 10
                    };
                    int coordsCursor = 0;
                    int coordsPos;
                    do
                    {
                        troop.Coordinates += Convert.ToString(data[itemPos + 6 + coordsCursor]);
                        coordsCursor++;
                        coordsPos = coordsCursor;
                        endPos = 3;
                    }
                    while (coordsPos <= endPos);
                    troops.Add(troop);
                    num++;
                    position = num;
                    endPos = 66;
                }
                while (position <= endPos);
            }
            return troops;
        }

        public List<Sietch> GetSietches()
        {
            return _sietches;
        }

        public List<Troop> GetTroops()
        {
            return _troops;
        }

        internal void UpdateTroop(Troop troop)
        {
            throw new NotImplementedException();
        }

        internal void UpdateSietch(Sietch sietch)
        {
            throw new NotImplementedException();
        }

        public Generals Generals => _generals;

        public List<byte> Uncompressed => _uncompressedData;

        public static bool SaveUnCompressedAs(string fileName, List<byte> uncompressed)
        {
            bool result = true;
            FileStream? fileStream = null;
            try
            {
                File.Delete(fileName);
                fileStream = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write);
                for (int i = 0; i < uncompressed.Count; i++)
                {
                    byte item = uncompressed[i];
                    fileStream.WriteByte(item);
                }
                fileStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
                fileStream?.Close();
                result = false;
            }
            return result;
        }

        public bool CompressData()
        {
            bool result2 = true;
            _compressedData = new List<byte>();
            checked
            {
                try
                {
                    int overallCurrentPos = 0;
                    int uncompressedDataCountMinusThree = _uncompressedData.Count - 3;
                    int cursorPosition = 0;
                    byte byteValueAtCursorPlusOne = 0;
                    byte byteValueAtCursorPlusTwo = 0;
                    while (true)
                    {
                        int currentTrapPos = cursorPosition;
                        int uncompressedDataEnd = uncompressedDataCountMinusThree;
                        if (currentTrapPos > uncompressedDataEnd)
                        {
                            break;
                        }
                        byte byteValueAtCursor = _uncompressedData[cursorPosition];
                        byteValueAtCursorPlusOne = _uncompressedData[cursorPosition + 1];
                        byteValueAtCursorPlusTwo = _uncompressedData[cursorPosition + 2];
                        unchecked
                        {
                            if (IsNonDeflate(cursorPosition) && byteValueAtCursor == 247)
                            {
                                _compressedData.Add(247);
                                _compressedData.Add(1);
                                _compressedData.Add(247);
                            }
                            else
                            {
                                int start = 0;
                                int end = 255;
                                Trap t = new();
                                bool isOverTrap = false;
                                if (GetTrapByRealOffset(cursorPosition, t))
                                {
                                    end = t.Repeat;
                                    isOverTrap = true;
                                }
                                if (byteValueAtCursor == byteValueAtCursorPlusOne && byteValueAtCursor != byteValueAtCursorPlusTwo)
                                {
                                    _compressedData.Add(byteValueAtCursor);
                                }
                                else if (byteValueAtCursor == byteValueAtCursorPlusOne && byteValueAtCursor == byteValueAtCursorPlusTwo)
                                {
                                    int num8 = cursorPosition;
                                    checked
                                    {
                                        int uncompressedDataEndMinusOne = _uncompressedData.Count - 1;
                                        overallCurrentPos = num8;
                                        while (true)
                                        {
                                            int currentPos = overallCurrentPos;
                                            uncompressedDataEnd = uncompressedDataEndMinusOne;
                                            if (currentPos > uncompressedDataEnd)
                                            {
                                                break;
                                            }
                                            if (byteValueAtCursor == _uncompressedData[overallCurrentPos])
                                            {
                                                start++;
                                                if (start == end)
                                                {
                                                    cursorPosition += start - 1;
                                                    _compressedData.Add(247);
                                                    _compressedData.Add((byte)start);
                                                    _compressedData.Add(byteValueAtCursor);
                                                    overallCurrentPos = _uncompressedData.Count;
                                                    if (isOverTrap)
                                                    {
                                                        _compressedData.Add(t.HexCode);
                                                        cursorPosition++;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                cursorPosition += start - 1;
                                                _compressedData.Add(247);
                                                _compressedData.Add((byte)start);
                                                _compressedData.Add(byteValueAtCursor);
                                                overallCurrentPos = _uncompressedData.Count;
                                            }
                                            overallCurrentPos++;
                                        }
                                    }
                                }
                                else
                                {
                                    _compressedData.Add(byteValueAtCursor);
                                }
                            }
                        }
                        cursorPosition++;
                    }
                    if (cursorPosition == _uncompressedData.Count - 2)
                    {
                        _compressedData.Add(byteValueAtCursorPlusOne);
                        _compressedData.Add(byteValueAtCursorPlusTwo);
                    }
                    if (cursorPosition == _uncompressedData.Count - 1)
                    {
                        _compressedData.Add(byteValueAtCursorPlusTwo);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetBaseException().Message);
                    result2 = false;
                }
                return result2;
            }
        }

        internal void UpdateContactDistance(int contactDistanceValue)
        {
            _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.ContactDistance)] = (byte)checked(contactDistanceValue);
        }

        internal void UpdateSpice(int spiceValue)
        {
            string spiceString = checked((int)Math.Round((double)spiceValue / 10.0)).ToString("X");
            byte[] spice = SequenceParser.SplitTwo(spiceString);
            _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.Spice)] = spice[0];
            if (spice.Length > 1)
            {
                _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.Spice) + 1] = spice[1];
            }
        }

        internal void UpdateCharisma(byte charismaValue) => _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.Charisma)] = (byte)(unchecked(charismaValue) * 2);

        public bool SaveCompressed() => SaveCompressedAs(_fileName);

        public bool SaveCompressedAs(string fileName)
        {
            bool result = true;
            CompressData();
            FileStream? fileStream = null;
            try
            {
                File.Delete(fileName + ".bak");
                File.Copy(fileName, fileName + ".bak");
                File.Delete(fileName);
                fileStream = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write);
                foreach (byte item in _compressedData)
                {
                    fileStream.WriteByte(item);
                }
                fileStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
                fileStream?.Close();
                result = false;
            }
            return result;
        }

        private void SetRealOffset(int i, int v)
        {
            checked
            {
                int num = _traps.Count - 1;
                int num2 = 0;
                while (true)
                {
                    int num3 = num2;
                    int num4 = num;
                    if (num3 <= num4)
                    {
                        if (_traps[num2].Offset == i)
                        {
                            _traps[num2].SetRealOffset(v);
                            break;
                        }
                        num2++;
                        continue;
                    }
                    break;
                }
            }
        }

        public bool UncompressData()
        {
            bool result = true;
            checked
            {
                int num = _original.Count - 3;
                _uncompressedData = new List<byte>();
                _control = new List<Control>();
                try
                {
                    int num2 = num;
                    int num3 = 0;
                    while (true)
                    {
                        int num4 = num3;
                        int num5 = num2;
                        if (num4 > num5)
                        {
                            break;
                        }
                        byte b = _original[num3];
                        byte b2 = _original[num3 + 1];
                        byte b3 = _original[num3 + 2];
                        byte[] ba = new byte[3] { b, b2, b3 };
                        if (!SequenceParser.IsControlSequence(ba))
                        {
                            Trap t = new();
                            bool trap = GetTrap(num3, t);
                            if (!SequenceParser.IsDeflateSequence(ba))
                            {
                                _uncompressedData.Add(b);
                                if (num3 == num)
                                {
                                    _uncompressedData.Add(b2);
                                    _uncompressedData.Add(b3);
                                }
                            }
                            else
                            {
                                if (trap)
                                {
                                    SetRealOffset(num3, _uncompressedData.Count);
                                }
                                int num6 = b2;
                                int num7 = 1;
                                while (true)
                                {
                                    int num8 = num7;
                                    num5 = num6;
                                    if (num8 > num5)
                                    {
                                        break;
                                    }
                                    _uncompressedData.Add(b3);
                                    num7++;
                                }
                                num3 += 2;
                            }
                        }
                        else
                        {
                            Control control = new();
                            control.Offset = _uncompressedData.Count;
                            control.ControlType = new byte[3] { b, b2, b3 };
                            _control.Add(control);
                            _uncompressedData.Add(247);
                            num3 += 2;
                        }
                        num3++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetBaseException().Message);
                    result = false;
                }
                return result;
            }
        }

        internal void ModifyByteAtAddressInUncompressedData(byte value, int position) => _uncompressedData[position] = value;

        private void DetectTraps()
        {
            _traps = new List<Trap>();
            checked
            {
                int num = _original.Count - 4;
                int num2 = 0;
                while (true)
                {
                    int num3 = num2;
                    int num4 = num;
                    if (num3 <= num4)
                    {
                        byte b = _original[num2];
                        byte repeat = _original[num2 + 1];
                        byte b2 = _original[num2 + 2];
                        byte b3 = _original[num2 + 3];
                        if (unchecked(b == 247 && b2 == b3))
                        {
                            Trap trap = new()
                            {
                                Offset = num2,
                                Repeat = repeat,
                                HexCode = b2
                            };
                            _traps.Add(trap);
                        }
                        num2++;
                        continue;
                    }
                    break;
                }
            }
        }

        private bool GetTrap(int index, Trap t)
        {
            bool result = false;
            for (int i = 0; i < _traps.Count; i++)
            {
                Trap trap = _traps[i];
                if (trap.Offset == index)
                {
                    t = trap;
                    result = true;
                    break;
                }
            }
            return result;
        }

        private bool GetTrapByRealOffset(int index, Trap t)
        {
            bool result = false;
            for (int i = 0; i < _traps.Count; i++)
            {
                Trap trap = _traps[i];
                if (trap.realOffset == index)
                {
                    t = trap;
                    result = true;
                    break;
                }
            }
            return result;
        }

        private bool IsNonDeflate(int i)
        {
            bool result = false;
            for (int j = 0; j < _control.Count; j++)
            {
                Control item = _control[j];
                if (item.Offset == i)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}