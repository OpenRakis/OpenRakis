The complete DUNE CD savegame editor.

Options:
  -c, --Compress           (Default: ) Compress the specified file

  -i, --InputFile          Required. (Default: ) The savegame to edit or describe on the standard output, for example
                           DUNE37S1.SAV

  -o, --OutputFile         (Default: ) Savegame output file name after a Compress or Write

  -r, --Read               (Default: true) Describe the entire save game on the standard output (partially implemented)

  -t, --TimeOfDay          Edit the time of day in the input savegame file (not implemented yet!)

  -u, --Uncompress         (Default: false) Save the uncompressed savegame to disk as [Filename.SAV.UNCOMPRESSED]

  -p, --PauseBeforeExit    (Default: false) Wait a key press before exiting

  -w, --Write              (Default: ) [Not implemented yet!] Write hex at position to the save game and exit. Format:
                           ByteHexValue,UncompressedSaveGameHexOffset

  --help                   Display this help screen.

  --version                Display version information.
