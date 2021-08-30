namespace DuneEdit2.Parsers
{
    using System.Collections.Generic;

    using DuneEdit2.Enums;

    public class SaveGameIndex
    {
        private enum SavegameFieldsOffsetsEnum
        {
            TroopsStartOffset = 19657,
            SietchsStartOffset = 17695,
            SietchLength = 28,
            CharismaStartOffset = 17480,
            ContactDistanceStartOffset = 21909,
            DateTimeStartOffset = 21907,
            GameStageOffset = 17481,
            SpiceStartOffset = 17599,
        }

        private Dictionary<FieldName, SaveGameFieldInfo> _tableOfContents = new Dictionary<FieldName, SaveGameFieldInfo>();

        public SaveGameIndex()
        {
            _tableOfContents.Add(FieldName.Spice, new SaveGameFieldInfo(FieldName.Spice, (int)SavegameFieldsOffsetsEnum.SpiceStartOffset));
            _tableOfContents.Add(FieldName.Charisma, new SaveGameFieldInfo(FieldName.Charisma, (int)SavegameFieldsOffsetsEnum.CharismaStartOffset));
            _tableOfContents.Add(FieldName.ContactDistance, new SaveGameFieldInfo(FieldName.ContactDistance, (int)SavegameFieldsOffsetsEnum.ContactDistanceStartOffset));
            _tableOfContents.Add(FieldName.DateTime, new SaveGameFieldInfo(FieldName.DateTime, (int)SavegameFieldsOffsetsEnum.DateTimeStartOffset));
            _tableOfContents.Add(FieldName.GameStage, new SaveGameFieldInfo(FieldName.GameStage, (int)SavegameFieldsOffsetsEnum.GameStageOffset));
            _tableOfContents.Add(FieldName.Sietchs, new SaveGameFieldInfo(FieldName.Sietchs, (int)SavegameFieldsOffsetsEnum.SietchsStartOffset));
            _tableOfContents.Add(FieldName.Troops, new SaveGameFieldInfo(FieldName.Troops, (int)SavegameFieldsOffsetsEnum.TroopsStartOffset));
        }

        public static Dictionary<FieldName, SaveGameFieldInfo> TableOfContents => new SaveGameIndex()._tableOfContents;

        public static int GetFieldStartPos(FieldName name) => SaveGameIndex.TableOfContents[name].StartPos;
    }
}