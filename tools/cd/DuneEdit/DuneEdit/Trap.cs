using System.Diagnostics;

namespace DuneEdit
{
	public class Trap
	{
		public int Offset;

		public int realOffset;

		public int Repeat;

		public byte HexCode;

		[DebuggerNonUserCode]
		public Trap()
		{
		}

		public void SetRealOffset(int v)
		{
			realOffset = v;
		}
	}
}
