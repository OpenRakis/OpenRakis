---
name: dune-savegame-decompress
description: 'Implement or debug Dune (1992, Cryo) savegame decompression in any language. Use when: decoding .SAV files from the DOS/CD game Dune, writing a savegame parser, porting the decompression algorithm, understanding the RLE-like byte encoding, handling control sequences and deflate sequences, extracting raw game state from compressed saves.'
argument-hint: 'Describe the target language, file, or specific decompression issue'
---

# Dune Savegame Decompression

## When to Use
- Implementing a savegame reader/editor for Dune (1992, Cryo/Virgin)
- Porting or re-implementing the decompression algorithm
- Debugging incorrect output from a decompressed save
- Understanding the binary format of `.SAV` files

## Background

Dune savegame files use a custom RLE-like compression. The format has two special 3-byte patterns
and a trailing 3-byte suffix that must be excluded from processing.

The algorithm was reverse-engineered from the original game executable and is implemented in
DuneEdit2 (the reference C# implementation) — but this skill is self-contained.

## File Layout

```
[compressed bytes...] [3-byte trailing suffix]
```

- The last 3 bytes of the file are **not part of the compressed stream** — exclude them.
- The active stream length is: `fileLength - 3`.

## Byte Sequence Classification

Read the stream 1 byte at a time (with a 2-byte lookahead). At each position `offset`, read:

```
b0 = data[offset]
b1 = data[offset + 1]
b2 = data[offset + 2]
```

Classify the 3-byte window:

### 1. Control Sequence
```
b0 == 0xF7 (247)  AND  b1 == 0x01  AND  b2 == 0xF7 (247)
```
- Emit a single `0xF7` byte to the output.
- Advance offset by **3** (consume all 3 bytes).
- Record this position in the output as a "control" marker (used for re-compression only).

### 2. Deflate (RLE repeat) Sequence
```
b0 == 0xF7 (247)  AND  b1 > 2
```
- Repeat byte `b2` exactly `b1` times in the output.
- Advance offset by **3** (consume all 3 bytes).

> Note: `b1 == 2` is **not** a deflate sequence — it falls through to the literal case.

### 3. Literal Byte
- Neither of the above patterns matched.
- Emit `b0` to the output.
- Advance offset by **1**.
- Special case: if `offset == streamLength` (last byte), also emit `b1` and `b2`.

## Decision Tree

```
Read b0, b1, b2 at current offset
│
├─ b0==0xF7 AND b1==0x01 AND b2==0xF7?
│   → Emit 0xF7; advance 3; mark as control
│
├─ b0==0xF7 AND b1>2?
│   → Emit b2 repeated b1 times; advance 3
│
└─ else (literal)
    → Emit b0; advance 1
      (if this is the last position: also emit b1, b2)
```

## Loop Condition

```
while offset <= streamLength:
    process...
```

`streamLength = fileBytes.Length - 3`  (the 3-byte suffix is excluded)

## Reference Implementation (pseudocode)

```
function decompress(fileBytes):
    streamLength = len(fileBytes) - 3
    output = []
    offset = 0

    while offset <= streamLength:
        b0 = fileBytes[offset]
        b1 = fileBytes[offset + 1]
        b2 = fileBytes[offset + 2]

        if b0 == 0xF7 and b1 == 0x01 and b2 == 0xF7:
            # Control sequence
            output.append(0xF7)
            offset += 3

        elif b0 == 0xF7 and b1 > 2:
            # RLE repeat: emit b2 exactly b1 times
            for i in range(b1):
                output.append(b2)
            offset += 3

        else:
            # Literal byte
            output.append(b0)
            if offset == streamLength:
                output.append(b1)
                output.append(b2)
            offset += 1

    return output
```

## Edge Cases and Gotchas

| Situation | Handling |
|-----------|----------|
| `b1 == 0x01` with `b0 == 0xF7` | Only a control sequence if `b2` is also `0xF7`; otherwise literal |
| `b1 == 0x02` with `b0 == 0xF7` | **Not** a deflate — treated as literal |
| `b1 == 0x00` with `b0 == 0xF7` | Not deflate — treated as literal |
| Last byte at `offset == streamLength` | Flush remaining 3 bytes as literals |
| Empty or very short files | Guard against out-of-bounds reads on `offset + 1` and `offset + 2` |

## Validation

After decompression, known offsets can be sanity-checked against game constants (version-dependent):

- **DUNE 37/38** (most common CD release): offsets defined in `Dune37Offsets`
- The uncompressed data is raw binary; fields are little-endian single bytes unless noted.

To verify your output, compare a known save against DuneEdit2's `WriteUncompressedSaveGameInTheSameFolder()` dump (`.SAV.UNCOMPRESSED` file).

## Known Save File Variants

| Format ID | Version |
|-----------|---------|
| DUNE_21   | v2.1    |
| DUNE_23   | v2.3    |
| DUNE_24   | v2.4    |
| DUNE_37   | v3.7 (most common CD) |
| DUNE_38   | v3.8    |

The compression format is the same across all versions; only the uncompressed field offsets differ.
