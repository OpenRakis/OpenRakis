﻿namespace DuneEdit2.Parsers
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
        private readonly SaveFileFormat _format;
        private ISaveGameOffsets _offsets;
        private readonly List<Location> _locations = new();

        private readonly List<Troop> _troops = new();

        public string Filename => _fileName;

        public SaveGameFile(SaveFileFormat format)
        {
            _format = format;
            _offsets = GetSaveFileOffsets(format);
        }

        private ISaveGameOffsets GetSaveFileOffsets(SaveFileFormat format)
        {
            if (format == SaveFileFormat.DUNE_21)
            {
                return new Dune21Offsets();
            }
            if (format == SaveFileFormat.DUNE_23)
            {
                return new Dune23Offsets();
            }
            if (format == SaveFileFormat.DUNE_24)
            {
                return new Dune24Offsets();
            }
            if (format == SaveFileFormat.DUNE_37)
            {
                return new Dune37Offsets();
            }
            return new Dune37Offsets();
        }

        public SaveGameFile(List<byte> data, SaveFileFormat format, bool isCompressed = true)
        {
            _format = format;
            _offsets = GetSaveFileOffsets(format);
            if (isCompressed)
            {
                _compressedData = data;
            }
            else
            {
                _uncompressedData = data;
            }
        }

        public SaveGameFile(string fileName, SaveFileFormat format)
        {
            _format = format;
            _offsets = GetSaveFileOffsets(format);
            _fileName = fileName;
            try
            {
                _originalSaveGameData = new List<byte>();
                using FileStream fileStream = File.OpenRead(_fileName);
                while (fileStream.Position < fileStream.Length)
                {
                    _originalSaveGameData.Add((byte)fileStream.ReadByte());
                }
                DetectTraps();
                UncompressData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
                throw;
            }
            _generals = new Generals(_uncompressedData, _offsets);
            _locations = PopulateLocations(_uncompressedData);
            _troops = PopulateTroops(_uncompressedData);
        }

        private List<Location> PopulateLocations(List<byte> data)
        {
            var locations = new List<Location>();
            for(int i = 0; i < 70; i++)
            {
                int itemPos = _offsets.Locations + i * 28;
                var location = new Location()
                {
                    StartOffset = itemPos,
                    Region = data[itemPos + 0],
                    SubRegion = data[itemPos + 1],
                    PosXmap = data[itemPos + 3],
                    PosYmap = data[itemPos + 4],
                    Unknown1 = data[itemPos + 5],
                    PosX = data[itemPos + 6],
                    PosY = data[itemPos + 7],
                    Appearance = data[itemPos + 8],
                    HousedTroopID = data[itemPos + 9],
                    StatusBitField = new ClsBitfield(data[itemPos + 10]),
                    Status = data[itemPos + 10],
                    GameStage = data[itemPos + 11],
                    Unknown3 = data[itemPos + 12],
                    Unknown4 = data[itemPos + 13],
                    Unknown5 = data[itemPos + 14],
                    Unknown6 = data[itemPos + 15],
                    SpicefieldID = data[itemPos + 16],
                    Spice = data[itemPos + 17],
                    SpiceDensity = data[itemPos + 18],
                    Unknown2 = data[itemPos + 19],
                    Harvesters = data[itemPos + 20],
                    Ornis = data[itemPos + 21],
                    Krys = data[itemPos + 22],
                    LaserGuns = data[itemPos + 23],
                    WeirdingMod = data[itemPos + 24],
                    Atomics = data[itemPos + 25],
                    Bulbs = data[itemPos + 26],
                    Water = data[itemPos + 27],
                };
                for (int j = 0; j < 4; j++)
                {
                    int coordinatesPartOffset = itemPos + 2 + j;
                    location.Coordinates += Convert.ToString(data[coordinatesPartOffset]);
                }
                locations.Add(location);
            }
            return locations;
        }

        private List<Troop> PopulateTroops(List<byte> data)
        {
            var troops = new List<Troop>();
            for(int i = 0; i < 68; i++)
            {
                int itemPos = _offsets.Troops + i * 27;
                var troop = new Troop(equipment: data[itemPos + 25])
                {
                    StartOffset = itemPos,
                    TroopID = data[itemPos + 0],
                    NextTroopInLocation = data[itemPos + 1],
                    Job = data[itemPos + 3],
                    Dissatisfaction = data[itemPos + 18],
                    Speech = data[itemPos + 19],
                    Motivation = data[itemPos + 21],
                    SpiceSkill = data[itemPos + 22],
                    ArmySkill = data[itemPos + 23],
                    EcologySkill = data[itemPos + 24],
                    Equipment = data[itemPos + 25],
                    Population = data[itemPos + 26] * 10
                };
                for(int j = 0; j < 4; j++)
                {
                    troop.Coordinates += Convert.ToString(data[itemPos + 6 + j]);
                }
                troops.Add(troop);
            }
            return troops;
        }

        public List<Location> GetSietches() => new(_locations);

        public List<Troop> GetTroops() => new(_troops);

        internal void UpdateTroop(Troop troop)
        {
            int startOffset = troop.StartOffset;
            _uncompressedData[startOffset + 3] = troop.Job;
            _uncompressedData[startOffset + 18] = troop.Dissatisfaction;
            _uncompressedData[startOffset + 21] = troop.Motivation;
            _uncompressedData[startOffset + 22] = troop.SpiceSkill;
            _uncompressedData[startOffset + 23] = troop.ArmySkill;
            _uncompressedData[startOffset + 24] = troop.EcologySkill;
            _uncompressedData[startOffset + 25] = (byte)troop.Equipment;
            _uncompressedData[startOffset + 26] = (byte)Math.Round(troop.Population / 10.0);
        }

        internal void UpdateSietch(Location location)
        {
            int startOffset = location.StartOffset;
            _uncompressedData[startOffset + 3] = location.PosXmap;
            _uncompressedData[startOffset + 4] = location.PosYmap;
            _uncompressedData[startOffset + 5] = location.Unknown1;
            _uncompressedData[startOffset + 6] = location.PosX;
            _uncompressedData[startOffset + 7] = location.PosY;
            _uncompressedData[startOffset + 8] = location.Appearance;
            _uncompressedData[startOffset + 9] = location.HousedTroopID;
            _uncompressedData[startOffset + 10] = (byte)location.Status;
            _uncompressedData[startOffset + 11] = location.GameStage;
            _uncompressedData[startOffset + 12] = location.Unknown3;
            _uncompressedData[startOffset + 13] = location.Unknown4;
            _uncompressedData[startOffset + 14] = location.Unknown5;
            _uncompressedData[startOffset + 15] = location.Unknown6;
            _uncompressedData[startOffset + 16] = location.SpicefieldID;
            _uncompressedData[startOffset + 17] = location.Spice;
            _uncompressedData[startOffset + 18] = location.SpiceDensity;
            _uncompressedData[startOffset + 19] = location.Unknown2;
            _uncompressedData[startOffset + 20] = location.Harvesters;
            _uncompressedData[startOffset + 21] = location.Ornis;
            _uncompressedData[startOffset + 22] = location.Krys;
            _uncompressedData[startOffset + 23] = location.LaserGuns;
            _uncompressedData[startOffset + 24] = location.WeirdingMod;
            _uncompressedData[startOffset + 25] = location.Atomics;
            _uncompressedData[startOffset + 26] = location.Bulbs;
            _uncompressedData[startOffset + 27] = location.Water;
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
            bool result = true;
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
                    result = false;
                }
                return result;
            }
        }

        internal void UpdateGameStage(byte gameStageValue)
        {
            _uncompressedData[_offsets.GameStage] = gameStageValue;
        }

        internal void UpdateContactDistance(int contactDistanceValue)
        {
            _uncompressedData[_offsets.ContactDistance] = (byte)checked(contactDistanceValue);
        }

        internal void UpdateSpice(int spiceValue)
        {
            string spiceString = checked((int)Math.Round(spiceValue / 10.0)).ToString("X");
            byte[] spice = SequenceParser.SplitTwo(spiceString);
            _uncompressedData[_offsets.Spice] = spice[0];
            if (spice.Length > 1)
            {
                _uncompressedData[_offsets.Spice + 1] = spice[1];
            }
        }

        internal void UpdateCharisma(byte charismaValue) => _uncompressedData[_offsets.Charisma] = (byte)(unchecked(charismaValue) * 2);

        public bool SaveCompressed() => SaveCompressedAs(_fileName);

        public bool SaveCompressedAs(string newFileName)
        {
            bool result = true;
            CompressData();
            FileStream? fileStream = null;
            try
            {
                if(File.Exists(newFileName))
                {
                    File.Delete(newFileName + ".bak");
                    File.Copy(newFileName, newFileName + ".bak");
                    File.Delete(newFileName);
                }
                fileStream = new FileStream(newFileName, FileMode.CreateNew, FileAccess.Write);
                for (int i = 0; i < _compressedData.Count; i++)
                {
                    byte item = _compressedData[i];
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
                    if (firstByte == 247 && secondByte == thirdByte)
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