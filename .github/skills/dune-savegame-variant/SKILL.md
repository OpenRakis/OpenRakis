---
name: dune-savegame-variant
description: 'Identify the Dune (1992, Cryo) savegame version from the filename and select the correct field offsets. Use when: parsing or editing .SAV files, routing to the right offset table, detecting DUNE21/DUNE23/DUNE24/DUNE37/DUNE38 save formats, implementing version-aware savegame readers, handling SaveFileFormat enum, or deciding which offsets to apply. The repo focus is DUNE 3.7 (CD version).'
argument-hint: 'Provide the .SAV filename or describe the version detection task'
---

# Dune Savegame Variant Detection

## When to Use
- Parsing a `.SAV` file and need to know which offset table to apply
- Implementing version-aware savegame readers, editors, or converters
- Routing logic based on game version embedded in the filename
- Deciding whether a file is the primary repo target (DUNE 3.7 CD)

## Filename Convention

The original game names saves with a fixed pattern:

```
DUNE{VERSION}S{SLOT}.SAV
```

| Part | Meaning | Example |
|------|---------|---------|
| `DUNE` | Literal prefix | `DUNE` |
| `{VERSION}` | 2-digit version code (no dot) | `37` for v3.7 |
| `S` | Literal separator | `S` |
| `{SLOT}` | Save slot number (0–N) | `1` |
| `.SAV` | Extension (uppercase on DOS) | `.SAV` |

**Examples**: `DUNE37S1.SAV`, `DUNE21S0.SAV`, `DUNE38S2.SAV`

## Version Code → Format Mapping

| Filename version code | Format ID | Game release | Offset table | Notes |
|-----------------------|-----------|--------------|--------------|-------|
| `21` | `DUNE_21` or `DUNE_23` | v2.1 or v2.3 (floppy) | `Dune21Offsets` or `Dune23Offsets` | **Ambiguous** — see below |
| `23` | `DUNE_23` | v2.3 | `Dune23Offsets` | Rare; mostly v2.3 writes `DUNE21` |
| `24` | `DUNE_24` | v2.4 | `Dune24Offsets` | |
| `37` | `DUNE_37` | **v3.7 (CD — repo focus)** | `Dune37Offsets` | |
| `38` | `DUNE_38` | v3.8 | `Dune37Offsets` (shared) | Byte-identical to DUNE_37 |

> **DUNE 3.8 shares the same field offsets as DUNE 3.7.** There is no separate `Dune38Offsets`.

> **DUNE21 prefix ambiguity**: Real v2.3 game installations write saves named `DUNE21Sx.SAV`, not `DUNE23Sx.SAV`. The filename code `21` cannot reliably distinguish v2.1 from v2.3. If precision is needed between these two sub-versions, uncompressed data length or known byte differences at specific offsets must be used.

## Detection Algorithm

```
function detectFormat(filename):
    stem = basename(filename)   # always uppercase on DOS

    if stem starts with "DUNE":
        versionCode = stem[4:6]               # characters at index 4 and 5

        switch versionCode:
            "21" → return DUNE_21  # WARNING: may actually be v2.3 data (see ambiguity note)
            "23" → return DUNE_23
            "24" → return DUNE_24
            "37" → return DUNE_37
            "38" → return DUNE_38

    # No match — default to most common release
    return DUNE_37
```

> **No in-file version header exists.** The compressed stream has no magic bytes that identify the game version. The filename is the only available signal.

## Offset Tables by Field

All values are byte offsets into the **uncompressed** save data.

| Field | DUNE_21 | DUNE_23 | DUNE_24 | DUNE_37 / DUNE_38 |
|-------|---------|---------|---------|-------------------|
| Troops | 19557 | 19577 | 19587 | 19657 |
| Locations | 17695 | 17615 | 17625 | 17695 |
| NPCs | 21393 | 21413 | 21423 | 21493 |
| Smugglers | 21393 | 21671 | 21681 | 21751 |
| Charisma | 17480 | 17480 | 17480 | 17480 |
| ContactDistance | 21909 | 21909 | 21909 | 21909 |
| NumberOfRalliedTroops | 17479 | 17479 | 17479 | 17479 |
| GameStage | 17481 | 17481 | 17481 | 17481 |
| Spice | 17599 | 17599 | 17599 | 17599 |
| Dialogues | 13113 | 13113 | 13113 | 13113 |

> Offsets for DUNE_37 / DUNE_38 are verified against real save files. Offsets for earlier versions are partially unverified — use with caution. The main structural divergence across versions is in Troops, Locations, NPCs, and Smugglers.

## Repo Focus: DUNE 3.7

This repository (OpenRakis) targets **DUNE 3.7** as the primary version:
- It is the most widely distributed CD release.
- The reference save file in tests is `DUNE37S1.SAV`.
- DUNE 3.7 offsets are fully verified against real save files.
- When implementing new field reads, validate against DUNE 3.7 first.
- DUNE 3.8 is a minor variant with the same field layout as DUNE 3.7.

## Decision Checklist

When starting work on any savegame field:

1. Extract the version code from the filename using the algorithm above.
2. Select the offset table from the mapping table.
3. If the version is DUNE 2.1 / 2.3 / 2.4, treat non-structural field offsets as unverified.
4. For DUNE 3.7 or 3.8, offsets are verified — use the DUNE 3.7 table for both.
5. If filename doesn't match the pattern, default to DUNE 3.7 and note the assumption.

## Edge Cases

| Situation | Handling |
|-----------|----------|
| Version code not in known list | Warn; default to `DUNE_37` |
| Filename has no `S{slot}` suffix | Version code still extractable from chars 4–5 |
| File was renamed by user | Version from filename may be wrong; document the ambiguity |
| `DUNE38Sx.SAV` | Use DUNE 3.7 offsets — byte-identical layout confirmed on real files |
| `DUNE21Sx.SAV` from v2.3 install | Filename says `21` but data uses v2.3 offsets; treat as ambiguous |
| Slot number | Slots start at **0** (`S0`), not 1 — `DUNE37S0.SAV` is a valid auto-save slot |
