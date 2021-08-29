namespace DuneEdit2.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using DuneEdit2.Enums;
    using DuneEdit2.Models;
    using DuneEdit2.Parsers;

    public record SaveGame
    {
        private readonly string _fileName = "";

        private readonly List<byte> _original = new();

        private List<byte> _compressedData = new();

        private List<Control> _control = new();

        private Generals _generals = new Generals();

        private List<Trap> _traps = new();

        private List<byte> _uncompressedData = new();

        public SaveGame()
        {
        }

        public SaveGame(List<byte> data, bool isCompressed = true)
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

        public SaveGame(string fileName)
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
            _generals = new Generals(this.Uncompressed);
        }

        public Generals Generals
        {
            get => _generals;
        }

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
                    int num = 0;
                    int num2 = _uncompressedData.Count - 3;
                    int num3 = 0;
                    byte b2 = 0;
                    byte b3 = 0;
                    while (true)
                    {
                        int num4 = num3;
                        int num5 = num2;
                        if (num4 > num5)
                        {
                            break;
                        }
                        byte b = _uncompressedData[num3];
                        b2 = _uncompressedData[num3 + 1];
                        b3 = _uncompressedData[num3 + 2];
                        unchecked
                        {
                            if (IsNonDeflate(num3) && b == 247)
                            {
                                _compressedData.Add(247);
                                _compressedData.Add(1);
                                _compressedData.Add(247);
                            }
                            else
                            {
                                int num6 = 0;
                                int num7 = 255;
                                Trap t = new();
                                bool flag = false;
                                if (GetTrapByRealOffset(num3, ref t))
                                {
                                    num7 = t.Repeat;
                                    flag = true;
                                }
                                if (b == b2 && b != b3)
                                {
                                    _compressedData.Add(b);
                                }
                                else if (b == b2 && b == b3)
                                {
                                    int num8 = num3;
                                    checked
                                    {
                                        int num9 = _uncompressedData.Count - 1;
                                        num = num8;
                                        while (true)
                                        {
                                            int num10 = num;
                                            num5 = num9;
                                            if (num10 > num5)
                                            {
                                                break;
                                            }
                                            if (b == _uncompressedData[num])
                                            {
                                                num6++;
                                                if (num6 == num7)
                                                {
                                                    num3 += num6 - 1;
                                                    _compressedData.Add(247);
                                                    _compressedData.Add((byte)num6);
                                                    _compressedData.Add(b);
                                                    num = _uncompressedData.Count;
                                                    if (flag)
                                                    {
                                                        _compressedData.Add(t.HexCode);
                                                        num3++;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                num3 += num6 - 1;
                                                _compressedData.Add(247);
                                                _compressedData.Add((byte)num6);
                                                _compressedData.Add(b);
                                                num = _uncompressedData.Count;
                                            }
                                            num++;
                                        }
                                    }
                                }
                                else
                                {
                                    _compressedData.Add(b);
                                }
                            }
                        }
                        num3++;
                    }
                    if (num3 == _uncompressedData.Count - 2)
                    {
                        _compressedData.Add(b2);
                        _compressedData.Add(b3);
                    }
                    if (num3 == _uncompressedData.Count - 1)
                    {
                        _compressedData.Add(b3);
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

        internal void UpdateCharisma(byte charismaValue)
        {
            _uncompressedData[SaveGameIndex.GetFieldStartPos(FieldName.Charisma)] = (byte)(unchecked(charismaValue) * 2);
        }

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
                            bool trap = GetTrap(num3, ref t);
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

        private bool GetTrap(int index, ref Trap t)
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

        private bool GetTrapByRealOffset(int index, ref Trap t)
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