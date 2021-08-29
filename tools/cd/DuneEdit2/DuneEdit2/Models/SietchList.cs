namespace DuneEdit2.Models
{
    using System;
    using System.Collections.Generic;

    public record SietchList
    {
        private List<Sietch> _sietchsList = new();

        private const int StartOffSet = 17695;

        private const int SietchLength = 28;

        private const int SietchCount = 70;

        private const int CoordinatesOffset = 2;
    }
}