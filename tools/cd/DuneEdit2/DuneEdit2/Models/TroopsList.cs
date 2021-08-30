namespace DuneEdit2.Models
{
    using System;
    using System.Collections.Generic;

    public record TroopsList
    {
        private List<Troop> _troopsList = new();

        private const int StartOffSet = 19657;

        private const int CoordinatesOffset = 6;

        private const int TroopLength = 27;

        private const int TroopCount = 67;
    }
}