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

        private readonly List<byte> _originalSaveGameData = new();

        private List<byte> _compressedData = new();

        private List<Control> _control = new();

        private readonly Generals _generals = new();

        private List<Trap> _traps = new();

        private List<byte> _uncompressedData = new();

        private readonly List<Sietch> _sietches = new();

        private readonly List<Troop> _troops = new();

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
                _originalSaveGameData = new List<byte>();
                using FileStream fileStream = new(fileName, FileMode.Open);
                while (fileStream.Position < fileStream.Length)
                {
                    _originalSaveGameData.Add(checked((byte)fileStream.ReadByte()));
                }
                DetectTraps();
                UncompressData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
                throw;
            }
            _generals = new Generals(_uncompressedData);
            _sietches = PopulateSietches(_uncompressedData);
            _troops = PopulateTroops(_uncompressedData);
        }

        private static List<Sietch> PopulateSietches(List<byte> data)
        {
            var sietches = new List<Sietch>();
            int cursor = 0;
            checked
            {
                int position;
                int endPos;
                do
                {
                    int itemPos = SaveGameIndex.GetFieldStartPos(FieldName.Sietchs) + cursor * 28;
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
                    cursor++;
                    position = cursor;
                    endPos = 69;
                }
                while (position <= endPos);
            }
            return sietches;
        }

        private static List<Troop> PopulateTroops(List<byte> data)
        {
            var troops = new List<Troop>();
            int cursor = 0;
            checked
            {
                int position;
                int endPos;
                do
                {
                    int itemPos = SaveGameIndex.GetFieldStartPos(FieldName.Troops) + cursor * 27;
                    var troop = new Troop(data[itemPos + 25])
                    {
                        StartOffset = itemPos,
                        TroopID = data[itemPos + 0],
                        NextTroopInSietch = data[itemPos + 1],
                        Job = data[itemPos + 3],
                        Status = data[itemPos + 18],
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
                    cursor++;
                    position = cursor;
                    endPos = 66;
                }
                while (position <= endPos);
            }
            return troops;
        }

        public List<Sietch> GetSietches() => new(_sietches);

        public List<Troop> GetTroops() => new(_troops);

        internal void UpdateTroop(Troop troop)
        {
            int startOffset = troop.StartOffset;
            _uncompressedData[startOffset + 3] = troop.Job;
            _uncompressedData[startOffset + 18] = troop.Status;
            _uncompressedData[startOffset + 21] = troop.Motivation;
            _uncompressedData[startOffset + 22] = troop.SpiceSkill;
            _uncompressedData[startOffset + 23] = troop.ArmySkill;
            _uncompressedData[startOffset + 24] = troop.EcologySkill;
            _uncompressedData[startOffset + 25] = (byte)troop.Equipment;
            _uncompressedData[startOffset + 26] = (byte)Math.Round(troop.Population / 10.0);
        }

        internal void UpdateSietch(Sietch sietch)
        {
            int startOffset = sietch.StartOffset;
            _uncompressedData[startOffset + 10] = (byte)sietch.Status;
            _uncompressedData[startOffset + 18] = sietch.SpiceDensity;
            _uncompressedData[startOffset + 20] = sietch.Harvesters;
            _uncompressedData[startOffset + 21] = sietch.Ornis;
            _uncompressedData[startOffset + 22] = sietch.Krys;
            _uncompressedData[startOffset + 23] = sietch.LaserGuns;
            _uncompressedData[startOffset + 24] = sietch.WeirdingMod;
            _uncompressedData[startOffset + 25] = sietch.Atomics;
            _uncompressedData[startOffset + 26] = sietch.Bulbs;
            _uncompressedData[startOffset + 27] = sietch.Water;
        }

        public Generals Generals => _generals;

        public List<byte> UncompressedData => _uncompressedData;

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
                throw;
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
                                if (GetTrapByRealOffset(cursorPosition))
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
                                    checked
                                    {
                                        int uncompressedDataEndMinusOne = _uncompressedData.Count - 1;
                                        overallCurrentPos = cursorPosition;
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

        internal void UpdateGameStage(byte gameStageValue)
        {
            _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.GameStage)] = gameStageValue;
        }

        internal void UpdateContactDistance(int contactDistanceValue)
        {
            _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.ContactDistance)] = (byte)checked(contactDistanceValue);
        }

        internal void UpdateSpice(int spiceValue)
        {
            string spiceString = checked((int)Math.Round(spiceValue / 10.0)).ToString("X");
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
                throw;
            }
            return result;
        }

        private void SetRealOffset(int index, int value)
        {
            checked
            {
                int length = _traps.Count - 1;
                int count = 0;
                while (true)
                {
                    if (count <= length)
                    {
                        if (_traps[count].Offset == index)
                        {
                            _traps[count].RealOffset = value;
                            break;
                        }
                        count++;
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
                int overallLength = _originalSaveGameData.Count - 3;
                _uncompressedData = new List<byte>();
                _control = new List<Control>();
                try
                {
                    int length = overallLength;
                    int offset = 0;
                    while (true)
                    {
                        int innerLength = length;
                        if (offset > innerLength)
                        {
                            break;
                        }
                        byte firstByte = _originalSaveGameData[offset];
                        byte secondByte = _originalSaveGameData[offset + 1];
                        byte thirdByte = _originalSaveGameData[offset + 2];
                        byte[] byteArray = new byte[3] { firstByte, secondByte, thirdByte };
                        if (!SequenceParser.IsControlSequence(byteArray))
                        {
                            Trap t = new();
                            bool trap = GetTrap(offset);
                            if (!SequenceParser.IsDeflateSequence(byteArray))
                            {
                                _uncompressedData.Add(firstByte);
                                if (offset == overallLength)
                                {
                                    _uncompressedData.Add(secondByte);
                                    _uncompressedData.Add(thirdByte);
                                }
                            }
                            else
                            {
                                if (trap)
                                {
                                    SetRealOffset(offset, _uncompressedData.Count);
                                }
                                int count = 1;
                                while (true)
                                {
                                    innerLength = secondByte;
                                    if (count > innerLength)
                                    {
                                        break;
                                    }
                                    _uncompressedData.Add(thirdByte);
                                    count++;
                                }
                                offset += 2;
                            }
                        }
                        else
                        {
                            Control control = new();
                            control.Offset = _uncompressedData.Count;
                            control.ControlType = new byte[3] { firstByte, secondByte, thirdByte };
                            _control.Add(control);
                            _uncompressedData.Add(247);
                            offset += 2;
                        }
                        offset++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetBaseException().Message);
                    throw;
                }
                return result;
            }
        }

        internal void ModifyByteInUncompressedData(byte value, int position) => _uncompressedData[position] = value;

        private void DetectTraps()
        {
            _traps = new List<Trap>();
            checked
            {
                int length = _originalSaveGameData.Count - 4;
                int count = 0;
                while (true)
                {
                    int num3 = count;
                    int num4 = length;
                    if (num3 <= num4)
                    {
                        byte firstByte = _originalSaveGameData[count];
                        byte repeatByte = _originalSaveGameData[count + 1];
                        byte secondByte = _originalSaveGameData[count + 2];
                        byte thirdByte = _originalSaveGameData[count + 3];
                        if (unchecked(firstByte == 247 && secondByte == thirdByte))
                        {
                            Trap trap = new()
                            {
                                Offset = count,
                                Repeat = repeatByte,
                                HexCode = secondByte
                            };
                            _traps.Add(trap);
                        }
                        count++;
                        continue;
                    }
                    break;
                }
            }
        }

        private bool GetTrap(int index)
        {
            bool result = false;
            for (int i = 0; i < _traps.Count; i++)
            {
                Trap trap = _traps[i];
                if (trap.Offset == index)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private bool GetTrapByRealOffset(int index)
        {
            bool result = false;
            for (int i = 0; i < _traps.Count; i++)
            {
                Trap trap = _traps[i];
                if (trap.RealOffset == index)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private bool IsNonDeflate(int index)
        {
            bool result = false;
            for (int i = 0; i < _control.Count; i++)
            {
                Control item = _control[i];
                if (item.Offset == index)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}