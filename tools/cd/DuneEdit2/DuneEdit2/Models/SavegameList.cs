using System;
using System.Collections.Generic;

using Microsoft.VisualBasic.CompilerServices;

namespace DuneEdit2.Models
{
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