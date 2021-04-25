using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.CompilerServices;

namespace DuneEdit
{
	public class Savegame_Item
	{
		private List<byte> _original;

		private List<byte> _compressed;

		private List<byte> _uncompressed;

		private string _FileName;

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

		public Savegame_Item()
		{
		}

		public Savegame_Item(List<byte> data)
		{
			_compressed = data;
		}

		public Savegame_Item(ref Exception result, string fileName)
		{
			bool flag = true;
			_FileName = fileName;
			FileStream fileStream = null;
			try
			{
				_original = new List<byte>();
				fileStream = new FileStream(fileName, FileMode.Open)
				{
					Position = 0L
				};
				while (fileStream.Position < fileStream.Length)
				{
					_original.Add(checked((byte)fileStream.ReadByte()));
				}
				DetectTraps();
				UnCompress();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = (result = ex);
				ProjectData.ClearProjectError();
			}
			fileStream?.Close();
		}

		public bool UnCompress()
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
						if (!ThreadingModule.IsControlSequence(ref ba))
						{
							Trap t = null;
							bool trap = GetTrap(num3, ref t);
							if (!ThreadingModule.isDeflateSequence(ref ba))
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
							Control control = new Control();
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
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					result = false;
					ProjectData.ClearProjectError();
				}
				return result;
			}
		}

		private bool GetTrap(int index, ref Trap t)
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

		private bool GetTrapByRealOffset(int index, ref Trap t)
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

		private bool isNonDeflate(int i)
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

		public bool Compress(ref Exception result)
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
					byte b2 = default(byte);
					byte b3 = default(byte);
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
							if (isNonDeflate(num3) && b == 247)
							{
								_compressed.Add(247);
								_compressed.Add(1);
								_compressed.Add(247);
							}
							else
							{
								int num6 = 0;
								int num7 = 255;
								Trap t = new Trap();
								bool flag = false;
								if (GetTrapByRealOffset(num3, ref t))
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
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					result2 = false;
					ProjectData.ClearProjectError();
				}
				return result2;
			}
		}

		public bool SaveCompressed(ref Exception Result)
		{
			return SaveCompressedAs(ref Result, _FileName);
		}

		public bool SaveUnCompressed(ref Exception Result)
		{
			return SaveUnCompressedAs(ref Result, _FileName);
		}

		public bool SaveCompressedAs(ref Exception Result, string fileName)
		{
			bool result = true;
			Compress(ref Result);
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
				Result = new Exception(Conversions.ToString(1));
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				fileStream?.Close();
				Result = ex2;
				result = false;
				ProjectData.ClearProjectError();
			}
			return result;
		}

		public bool SaveUnCompressedAs(ref Exception Result, string fileName)
		{
			bool result = true;
			FileStream fileStream = null;
			try
			{
				File.Delete(fileName);
				fileStream = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write);
				foreach (byte item in _uncompressed)
				{
					fileStream.WriteByte(item);
				}
				fileStream.Close();
				Result = new Exception(Conversions.ToString(1));
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				fileStream?.Close();
				Result = ex2;
				result = false;
				ProjectData.ClearProjectError();
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
							Trap trap = new Trap();
							trap.Offset = num2;
							trap.Repeat = repeat;
							trap.HexCode = b2;
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
