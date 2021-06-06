namespace DuneEdit2.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GameStageFinder
    {
        public GameStageFinder()
        {
        }

        public static string FindStage(byte id)
        {
            string result = string.Empty;
            if (id == 0x0) { result = "Game start"; }
            if (id == 0x01) { result = "met Gurney, Duncan appears in throne room"; }
            if (id == 0x02) { result = "go find the stillsuit maker"; }
            if (id == 0x04) { result = "find prospectors, visit sietch"; }
            if (id == 0x05) { result = "go back home"; }
            if (id == 0x06) { result = "look for hidden room (comms)"; }
            if (id == 0x08) { result = "getting warmer"; }
            if (id == 0x0c) { result = "found the comms room, go talk to Duncan"; }
            if (id == 0x0d) { result = "go find a harvester ?"; }
            if (id == 0x10) { result = "found the harvester in Tuono Harg"; }
            if (id == 0x14) { result = "go into the desert"; }
            if (id == 0x18) { result = "look for Gurney"; }
            if (id == 0x2c) { result = "take Stilgar home to meet your folks"; }
            if (id == 0x34) { result = "Leto is about to leave"; }
            if (id == 0x35) { result = "Leto has left"; }
            if (id == 0x48) { result = "morning song starts playing"; }
            if (id == 0x4f) { result = "can ride worms"; }
            if (id == 0x50) { result = "have ridden a worm, let's tell Thufir"; }
            if (id == 0x51) { result = "look for hidden rooms (greenhouse)"; }
            if (id == 0x54) { result = "show the greenhouse to Chani and Stilgar"; }
            if (id == 0x55) { result = "go meet Liet Kynes"; }
            if (id == 0x60) { result = "go find Chani"; }
            if (id == 0x64) { result = "Chani has been kidnapped"; }
            if (id == 0x68) { result = "Chani is back"; }
            if (id == 0xc8) { result = "ending"; }
            return result;
        }
    }
}