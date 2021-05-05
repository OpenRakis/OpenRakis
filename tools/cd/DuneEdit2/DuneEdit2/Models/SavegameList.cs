namespace DuneEdit2.Models
{
    using System.Collections.Generic;

    public class SavegameList : List<SavegameItem>
    {
        private readonly string _fileName;

        public SavegameList(string fileName)
        {
            _fileName = fileName;
            Add(new SavegameItem(fileName));
        }
    }
}