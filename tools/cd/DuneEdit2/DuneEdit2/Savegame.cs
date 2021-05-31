namespace DuneEdit2
{
    using DuneEdit2.Models;
    using DuneEdit2.Parsing;

    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Savegame
    {
        private readonly List<byte> _original;

        private List<byte> _compressed;

        private List<byte> _uncompressed;

        private readonly string _fileName;

        private List<Trap> _traps;

        private List<Control> _control;

        public List<byte> Uncompressed => _uncompressed;

        public List<byte> Compressed => _compressed;

        public void SetRealOffset(int i, int v)
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

        public Savegame()
        {
        }

        public Savegame(List<byte> data, bool compressed = true)
        {
            if (compressed)
            {
                _compressed = data;
            }
            else
            {
                _uncompressed = data;
            }
        }

        public Savegame(string fileName)
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
                Uncompress();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
            }
        }

        public bool Uncompress()
        {
            bool result = true;
            checked
            {
                int num = _original.Count - 3;
                _uncompressed = new List<byte>();
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
                            Trap t = null;
                            bool trap = GetTrap(num3, t);
                            if (!SequenceParser.IsDeflateSequence(ba))
                            {
                                _uncompressed.Add(b);
                                if (num3 == num)
                                {
                                    _uncompressed.Add(b2);
                                    _uncompressed.Add(b3);
                                }
                            }
                            else
                            {
                                if (trap)
                                {
                                    SetRealOffset(num3, _uncompressed.Count);
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
                                    _uncompressed.Add(b3);
                                    num7++;
                                }
                                num3 += 2;
                            }
                        }
                        else
                        {
                            Control control = new();
                            control.Offset = _uncompressed.Count;
                            control.ControlType = new byte[3] { b, b2, b3 };
                            _control.Add(control);
                            _uncompressed.Add(247);
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

        private bool GetTrap(int index, Trap t)
        {
            bool result = false;
            foreach (Trap trap in _traps)
            {
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
            foreach (Trap trap in _traps)
            {
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
            foreach (Control item in _control)
            {
                if (item.Offset == i)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool Compress()
        {
            bool result2 = true;
            _compressed = new List<byte>();
            checked
            {
                try
                {
                    int num = 0;
                    int num2 = _uncompressed.Count - 3;
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
                        byte b = _uncompressed[num3];
                        b2 = _uncompressed[num3 + 1];
                        b3 = _uncompressed[num3 + 2];
                        unchecked
                        {
                            if (IsNonDeflate(num3) && b == 247)
                            {
                                _compressed.Add(247);
                                _compressed.Add(1);
                                _compressed.Add(247);
                            }
                            else
                            {
                                int num6 = 0;
                                int num7 = 255;
                                Trap t = new();
                                bool flag = false;
                                if (GetTrapByRealOffset(num3, t))
                                {
                                    num7 = t.Repeat;
                                    flag = true;
                                }
                                if (b == b2 && b != b3)
                                {
                                    _compressed.Add(b);
                                }
                                else if (b == b2 && b == b3)
                                {
                                    int num8 = num3;
                                    checked
                                    {
                                        int num9 = _uncompressed.Count - 1;
                                        num = num8;
                                        while (true)
                                        {
                                            int num10 = num;
                                            num5 = num9;
                                            if (num10 > num5)
                                            {
                                                break;
                                            }
                                            if (b == _uncompressed[num])
                                            {
                                                num6++;
                                                if (num6 == num7)
                                                {
                                                    num3 += num6 - 1;
                                                    _compressed.Add(247);
                                                    _compressed.Add((byte)num6);
                                                    _compressed.Add(b);
                                                    num = _uncompressed.Count;
                                                    if (flag)
                                                    {
                                                        _compressed.Add(t.HexCode);
                                                        num3++;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                num3 += num6 - 1;
                                                _compressed.Add(247);
                                                _compressed.Add((byte)num6);
                                                _compressed.Add(b);
                                                num = _uncompressed.Count;
                                            }
                                            num++;
                                        }
                                    }
                                }
                                else
                                {
                                    _compressed.Add(b);
                                }
                            }
                        }
                        num3++;
                    }
                    if (num3 == _uncompressed.Count - 2)
                    {
                        _compressed.Add(b2);
                        _compressed.Add(b3);
                    }
                    if (num3 == _uncompressed.Count - 1)
                    {
                        _compressed.Add(b3);
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

        public bool SaveCompressed() => SaveCompressedAs(_fileName);

        public bool SaveUnCompressed() => SaveUnCompressedAs(_fileName, _uncompressed);

        public bool SaveCompressedAs(string fileName)
        {
            bool result = true;
            Compress();
            FileStream fileStream = null;
            try
            {
                File.Delete(fileName + ".bak");
                File.Copy(fileName, fileName + ".bak");
                File.Delete(fileName);
                fileStream = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write);
                foreach (byte item in _compressed)
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

        public static bool SaveUnCompressedAs(string fileName, List<byte> uncompressed)
        {
            bool result = true;
            FileStream fileStream = null;
            try
            {
                File.Delete(fileName);
                fileStream = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write);
                foreach (byte item in uncompressed)
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
    }
}