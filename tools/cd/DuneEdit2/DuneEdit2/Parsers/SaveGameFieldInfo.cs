using DuneEdit2.Enums;

namespace DuneEdit2.Parsers
{
    public record SaveGameFieldInfo
    {
        private int _length = 1;

        public SaveGameFieldInfo(FieldName name, int startPos)
        {
            Name = name;
            StartPos = startPos;
        }

        public SaveGameFieldInfo(FieldName name, int startPos, int length)
        {
            Name = name;
            StartPos = startPos;
            _length = length;
        }

        public FieldName Name { get; set; }

        public string NameAsString => Name.ToString();

        public int StartPos { get; set; }
        public int EndPos => StartPos + Length;
        public int Length => _length;
    }
}