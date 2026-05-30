namespace DuneTools.ViewModels;

using System.Collections.Generic;
using System.Globalization;

internal static class KnownFieldCatalogFactory
{
    private const int LocationsBaseOffset = 17695;
    private const int LocationsCount = 70;
    private const int LocationsStride = 28;

    private const int TroopsBaseOffset = 19657;
    private const int TroopsCount = 68;
    private const int TroopsStride = 27;

    private const int NpcsBaseOffset = 21493;
    private const int NpcsCount = 16;
    private const int NpcsStride = 16;

    private const int SmugglersBaseOffset = 21751;
    private const int SmugglersCount = 6;
    private const int SmugglersStride = 17;

    public static IReadOnlyList<KnownFieldDescriptor> Build()
    {
        List<KnownFieldDescriptor> fields = [];
        AddGlobals(fields);
        AddTroops(fields);
        AddLocations(fields);
        AddSmugglers(fields);
        AddNpcs(fields);
        return fields;
    }

    private static void AddGlobals(List<KnownFieldDescriptor> fields)
    {
        fields.Add(ByteField("Globals Number Of Rallied Troops", GlobalsViewModel.NumberOfRalliedTroopsOffset));
        fields.Add(new KnownFieldDescriptor(
            "Globals Charisma",
            GlobalsViewModel.CharismaOffset,
            1,
            "n/a",
            "unsigned byte",
            "raw <= 1 ? 0 : raw / 2.0",
            bytes =>
            {
                byte raw = bytes[0];
                int decoded = GlobalsViewModel.DecodeCharisma(raw);
                return new KnownFieldDecodeResult(decoded.ToString(CultureInfo.InvariantCulture), $"raw: {raw}", null);
            }));
        fields.Add(new KnownFieldDescriptor(
            "Globals Game Stage",
            GlobalsViewModel.GameStageOffset,
            1,
            "n/a",
            "unsigned byte",
            "lookup game stage description",
            bytes =>
            {
                byte raw = bytes[0];
                string description = GlobalsViewModel.DescribeGameStage(raw);
                if (description == "Unused / not yet discovered.")
                {
                    description = "Unknown";
                }

                return new KnownFieldDecodeResult(GlobalsViewModel.FormatGameStage(raw), $"raw: {raw}", description);
            }));
        fields.Add(new KnownFieldDescriptor(
            "Globals Spice",
            GlobalsViewModel.SpiceOffset,
            2,
            "little-endian",
            "unsigned uint16",
            "little-endian uint16 * 10",
            bytes =>
            {
                int raw = bytes[0] | (bytes[1] << 8);
                int decoded = GlobalsViewModel.DecodeSpice(bytes[0], bytes[1]);
                return new KnownFieldDecodeResult(decoded.ToString(CultureInfo.InvariantCulture), $"raw: {raw:X4}", null);
            }));
        fields.Add(new KnownFieldDescriptor(
            "Globals Contact Distance",
            GlobalsViewModel.ContactDistanceOffset,
            1,
            "n/a",
            "unsigned byte",
            "parse raw byte as hexadecimal decimal",
            bytes =>
            {
                byte raw = bytes[0];
                int decoded = GlobalsViewModel.DecodeContactDistance(raw);
                return new KnownFieldDecodeResult(decoded.ToString(CultureInfo.InvariantCulture), $"raw: {raw}", null);
            }));
    }

    private static void AddTroops(List<KnownFieldDescriptor> fields)
    {
        for (int i = 0; i < TroopsCount; i++)
        {
            int start = TroopsBaseOffset + (i * TroopsStride);
            string prefix = $"Troop {i:D2}";

            fields.Add(ByteField($"{prefix} ID", start + 0));
            fields.Add(ByteField($"{prefix} Next Troop In Location", start + 1));
            fields.Add(ByteField($"{prefix} Position Around Location", start + 2));
            fields.Add(ByteField($"{prefix} Job", start + 3));
            fields.Add(new KnownFieldDescriptor(
                $"{prefix} Coordinates",
                start + 6,
                4,
                "n/a",
                "4 bytes",
                "concatenate coordinate bytes",
                bytes => new KnownFieldDecodeResult(
                    string.Join(",", bytes[0], bytes[1], bytes[2], bytes[3]),
                    $"raw: {bytes[0]} {bytes[1]} {bytes[2]} {bytes[3]}",
                    null)));
            fields.Add(ByteField($"{prefix} Dissatisfaction", start + 18));
            fields.Add(new KnownFieldDescriptor(
                $"{prefix} Origin",
                start + 18,
                1,
                "n/a",
                "derived",
                "raw > 100 => Southern Tribe, else Northern Tribe",
                bytes => new KnownFieldDecodeResult(
                    bytes[0] > 100 ? "Southern Tribe" : "Northern Tribe",
                    $"raw: {bytes[0]}",
                    null)));
            fields.Add(ByteField($"{prefix} Speech", start + 19));
            fields.Add(ByteField($"{prefix} Motivation", start + 21));
            fields.Add(ByteField($"{prefix} Spice Skill", start + 22));
            fields.Add(ByteField($"{prefix} Army Skill", start + 23));
            fields.Add(ByteField($"{prefix} Ecology Skill", start + 24));
            fields.Add(new KnownFieldDescriptor(
                $"{prefix} Equipment Bitfield",
                start + 25,
                1,
                "n/a",
                "unsigned byte",
                "equipment bitfield",
                bytes => new KnownFieldDecodeResult(
                    $"0x{bytes[0]:X2}",
                    $"raw: {bytes[0]}",
                    null)));
            fields.Add(BitField($"{prefix} Equipment Bulbs", start + 25, 1));
            fields.Add(BitField($"{prefix} Equipment Atomics", start + 25, 2));
            fields.Add(BitField($"{prefix} Equipment Weirdings", start + 25, 3));
            fields.Add(BitField($"{prefix} Equipment Laser Guns", start + 25, 4));
            fields.Add(BitField($"{prefix} Equipment Krys Knives", start + 25, 5));
            fields.Add(BitField($"{prefix} Equipment Ornithopters", start + 25, 6));
            fields.Add(BitField($"{prefix} Equipment Harvesters", start + 25, 7));
            fields.Add(new KnownFieldDescriptor(
                $"{prefix} Population",
                start + 26,
                1,
                "n/a",
                "unsigned byte",
                "raw * 10",
                bytes =>
                {
                    int decoded = bytes[0] * 10;
                    return new KnownFieldDecodeResult(decoded.ToString(CultureInfo.InvariantCulture), $"raw: {bytes[0]}", null);
                }));
        }
    }

    private static void AddLocations(List<KnownFieldDescriptor> fields)
    {
        for (int i = 0; i < LocationsCount; i++)
        {
            int start = LocationsBaseOffset + (i * LocationsStride);
            string prefix = $"Location {i:D2}";

            fields.Add(ByteField($"{prefix} Region", start + 0));
            fields.Add(ByteField($"{prefix} SubRegion", start + 1));
            fields.Add(ByteField($"{prefix} PosX Map", start + 3));
            fields.Add(ByteField($"{prefix} PosY Map", start + 4));
            fields.Add(ByteField($"{prefix} PosX", start + 6));
            fields.Add(ByteField($"{prefix} PosY", start + 7));
            fields.Add(ByteField($"{prefix} Appearance", start + 8));
            fields.Add(ByteField($"{prefix} Housed Troop ID", start + 9));
            fields.Add(new KnownFieldDescriptor(
                $"{prefix} Status Bitfield",
                start + 10,
                1,
                "n/a",
                "unsigned byte",
                "status bitfield",
                bytes => new KnownFieldDecodeResult($"0x{bytes[0]:X2}", $"raw: {bytes[0]}", null)));
            fields.Add(BitField($"{prefix} Status Has Vegetation", start + 10, 0));
            fields.Add(BitField($"{prefix} Status In Battle", start + 10, 1));
            fields.Add(BitField($"{prefix} Status Infiltrated", start + 10, 2));
            fields.Add(BitField($"{prefix} Status Battle Won", start + 10, 3));
            fields.Add(BitField($"{prefix} Status See Inventory", start + 10, 4));
            fields.Add(BitField($"{prefix} Status Has Windtrap", start + 10, 5));
            fields.Add(BitField($"{prefix} Status Prospected", start + 10, 6));
            fields.Add(BitField($"{prefix} Status Not Discovered", start + 10, 7));
            fields.Add(ByteField($"{prefix} Game Stage", start + 11));
            fields.Add(ByteField($"{prefix} Spicefield ID", start + 16));
            fields.Add(ByteField($"{prefix} Spice", start + 17));
            fields.Add(ByteField($"{prefix} Spice Density", start + 18));
            fields.Add(ByteField($"{prefix} Harvesters", start + 20));
            fields.Add(ByteField($"{prefix} Ornithopters", start + 21));
            fields.Add(ByteField($"{prefix} Krys Knives", start + 22));
            fields.Add(ByteField($"{prefix} Laser Guns", start + 23));
            fields.Add(ByteField($"{prefix} Weirding Modules", start + 24));
            fields.Add(ByteField($"{prefix} Atomics", start + 25));
            fields.Add(ByteField($"{prefix} Bulbs", start + 26));
            fields.Add(ByteField($"{prefix} Water", start + 27));
        }
    }

    private static void AddSmugglers(List<KnownFieldDescriptor> fields)
    {
        for (int i = 0; i < SmugglersCount; i++)
        {
            int start = SmugglersBaseOffset + (i * SmugglersStride);
            string prefix = $"Smuggler {i:D2}";

            fields.Add(ByteField($"{prefix} Region", start + 0));
            fields.Add(ByteField($"{prefix} Willingness To Haggle", start + 1));
            fields.Add(ByteField($"{prefix} Harvesters", start + 4));
            fields.Add(ByteField($"{prefix} Ornithopters", start + 5));
            fields.Add(ByteField($"{prefix} Krys Knives", start + 6));
            fields.Add(ByteField($"{prefix} Laser Guns", start + 7));
            fields.Add(ByteField($"{prefix} Weirding Modules", start + 8));
            fields.Add(ByteField($"{prefix} Harvesters Price", start + 9));
            fields.Add(ByteField($"{prefix} Ornithopters Price", start + 10));
            fields.Add(ByteField($"{prefix} Krys Knives Price", start + 11));
            fields.Add(ByteField($"{prefix} Laser Guns Price", start + 12));
            fields.Add(ByteField($"{prefix} Weirding Modules Price", start + 13));
        }
    }

    private static void AddNpcs(List<KnownFieldDescriptor> fields)
    {
        for (int i = 0; i < NpcsCount; i++)
        {
            int start = NpcsBaseOffset + (i * NpcsStride);
            string prefix = $"NPC {i:D2}";

            fields.Add(ByteField($"{prefix} Sprite ID", start + 0));
            fields.Add(ByteField($"{prefix} Room Location", start + 2));
            fields.Add(ByteField($"{prefix} Type Of Place", start + 3));
            fields.Add(ByteField($"{prefix} Dialogue Available", start + 4));
            fields.Add(ByteField($"{prefix} Exact Place", start + 5));
            fields.Add(ByteField($"{prefix} For Dialogue", start + 6));
        }
    }

    private static KnownFieldDescriptor ByteField(string name, int offset)
    {
        return new KnownFieldDescriptor(
            name,
            offset,
            1,
            "n/a",
            "unsigned byte",
            "raw byte",
            bytes => new KnownFieldDecodeResult(
                bytes[0].ToString(CultureInfo.InvariantCulture),
                $"raw: {bytes[0]}",
                null));
    }

    private static KnownFieldDescriptor BitField(string name, int offset, int bit)
    {
        return new KnownFieldDescriptor(
            name,
            offset,
            1,
            "n/a",
            "boolean bit",
            $"bit {bit} of byte",
            bytes =>
            {
                bool value = (bytes[0] & (1 << bit)) != 0;
                return new KnownFieldDecodeResult(value ? "True" : "False", $"raw: {bytes[0]}", null);
            });
    }
}
