using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;

namespace DuneEdit
{
	public class Savegame_List : List<Savegame_Item>
	{
		private string _FileName;

		public Savegame_List(ref Exception result, string fileName)
		{
			Exception result2 = new Exception(Conversions.ToString(1));
			_FileName = fileName;
			Add(new Savegame_Item(ref result2, fileName));
			result = result2;
		}
	}
}
