using System.Collections;

namespace DuneEdit2.Models;

public  interface ISaveGameOffsets
{
    public  int TimeCounters { get;}
    public  int Troops { get;}
    public  int Locations { get;}
    public  int NPCs { get;}
    public  int Smugglers { get;}
    public  int Charisma { get;}
    public  int ContactDistance { get;}
    public  int DateTime { get;}
    public  int DateTimeSaveScreen { get;}
    public int NumberOfRalliedTroops { get; }
    public  int GameStage { get;}
    public  int Spice { get;}
    public  int Dialogues { get;}
}

public record Dune21Offsets : ISaveGameOffsets
{
    int ISaveGameOffsets.TimeCounters => 17339;
    int ISaveGameOffsets.Troops => 19557;
    int ISaveGameOffsets.Locations => 17695;
    int ISaveGameOffsets.NPCs => 21393;
    int ISaveGameOffsets.Smugglers => 21393;
    int ISaveGameOffsets.Charisma => 17480; // TBD
    int ISaveGameOffsets.ContactDistance => 21909; // TBD
    int ISaveGameOffsets.DateTime => 21907; // TBD
    int ISaveGameOffsets.DateTimeSaveScreen => 0; // TBD
    int ISaveGameOffsets.GameStage => 17481; // TBD
    int ISaveGameOffsets.NumberOfRalliedTroops => 17479; //TBD
    int ISaveGameOffsets.Spice => 17599; // TBD
    int ISaveGameOffsets.Dialogues => 13113; // TBD
}

public record Dune23Offsets : ISaveGameOffsets
{
    int ISaveGameOffsets.TimeCounters => 17359;
    int ISaveGameOffsets.Troops => 19577;
    int ISaveGameOffsets.Locations => 17615;
    int ISaveGameOffsets.NPCs => 21413;
    int ISaveGameOffsets.Smugglers => 21671;
    int ISaveGameOffsets.Charisma => 17480; // TBD
    int ISaveGameOffsets.ContactDistance => 21909; // TBD
    int ISaveGameOffsets.DateTime => 21907; // TBD
    int ISaveGameOffsets.DateTimeSaveScreen => 0; // TBD
    int ISaveGameOffsets.NumberOfRalliedTroops => 17479; //TBD
    int ISaveGameOffsets.GameStage => 17481; // TBD
    int ISaveGameOffsets.Spice => 17599; // TBD
    int ISaveGameOffsets.Dialogues => 13113; // TBD
}

public record Dune24Offsets : ISaveGameOffsets
{
    int ISaveGameOffsets.TimeCounters => 17369;
    int ISaveGameOffsets.Troops => 19587;
    int ISaveGameOffsets.Locations => 17625;
    int ISaveGameOffsets.NPCs => 21423;
    int ISaveGameOffsets.Smugglers => 21681;
    int ISaveGameOffsets.Charisma => 17480; // TBD
    int ISaveGameOffsets.ContactDistance => 21909; // TBD
    int ISaveGameOffsets.DateTime => 21907; // TBD
    int ISaveGameOffsets.DateTimeSaveScreen => 0; // TBD
    int ISaveGameOffsets.NumberOfRalliedTroops => 17479; //TBD
    int ISaveGameOffsets.GameStage => 17481; // TBD
    int ISaveGameOffsets.Spice => 17599; // TBD
    int ISaveGameOffsets.Dialogues => 13113; // TBD
}

public record Dune37Offsets : ISaveGameOffsets
{
    int ISaveGameOffsets.TimeCounters => 17439;
    int ISaveGameOffsets.Troops => 19657;
    int ISaveGameOffsets.Locations => 17695;
    int ISaveGameOffsets.NPCs => 21493;
    int ISaveGameOffsets.Smugglers => 21751;
    int ISaveGameOffsets.Charisma => 17480;
    int ISaveGameOffsets.ContactDistance => 21909;
    int ISaveGameOffsets.DateTime => 21907;
    int ISaveGameOffsets.DateTimeSaveScreen => 0;
    int ISaveGameOffsets.NumberOfRalliedTroops => 17479;
    int ISaveGameOffsets.GameStage => 17481;
    int ISaveGameOffsets.Spice => 17599;
    int ISaveGameOffsets.Dialogues => 13113;
}
