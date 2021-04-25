using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;

namespace DuneEdit
{
	public class Troops_List : List<Troops_Item>
	{
		private const int StartOffSet = 19657;

		private const int CoordinatesOffset = 6;

		private const int TroopLength = 27;

		private const int TroopCount = 67;

		private List<byte> _Data;

		public void Update(int troopID, byte job, byte dissatisfaction, byte motivation, byte spiceSkill, byte armySkill, byte ecologySkill, byte equipment, int population)
		{
			checked
			{
				using Enumerator enumerator = GetEnumerator();
				while (enumerator.MoveNext())
				{
					Troops_Item current = enumerator.Current;
					if (current.troopID == troopID)
					{
						Troops_Item troops_Item = current;
						troops_Item.job = job;
						troops_Item.Dissatisfaction = dissatisfaction;
						troops_Item.Motivation = motivation;
						troops_Item.SpiceSkill = spiceSkill;
						troops_Item.ArmySkill = armySkill;
						troops_Item.EcologySkill = ecologySkill;
						troops_Item.Equipment = equipment;
						troops_Item.Population = population;
						troops_Item = null;
						int startOffset = current.startOffset;
						_Data[startOffset + 3] = job;
						_Data[startOffset + 18] = dissatisfaction;
						_Data[startOffset + 21] = motivation;
						_Data[startOffset + 22] = spiceSkill;
						_Data[startOffset + 23] = armySkill;
						_Data[startOffset + 24] = ecologySkill;
						_Data[startOffset + 25] = equipment;
						_Data[startOffset + 26] = (byte)Math.Round((double)current.Population / 10.0);
						break;
					}
				}
			}
		}

		public Troops_List(List<byte> Data)
		{
			_Data = Data;
			int num = 0;
			checked
			{
				int num6;
				int num5;
				do
				{
					int num2 = 19657 + num * 27;
					Troops_Item troops_Item = new Troops_Item
					{
						startOffset = num2,
						troopID = Data[num2 + 0],
						nextTroopInSietch = Data[num2 + 1],
						job = Data[num2 + 3],
						Dissatisfaction = Data[num2 + 18],
						speech = Data[num2 + 19],
						Motivation = Data[num2 + 21],
						SpiceSkill = Data[num2 + 22],
						ArmySkill = Data[num2 + 23],
						EcologySkill = Data[num2 + 24],
						Equipment = Data[num2 + 25],
						Population = unchecked((int)Data[checked(num2 + 26)]) * 10
					};
					int num3 = 0;
					int num4;
					do
					{
						troops_Item.Coordinates += Conversions.ToString(Data[num2 + 6 + num3]);
						num3++;
						num4 = num3;
						num5 = 3;
					}
					while (num4 <= num5);
					Add(troops_Item);
					num++;
					num6 = num;
					num5 = 66;
				}
				while (num6 <= num5);
			}
		}
	}
}
