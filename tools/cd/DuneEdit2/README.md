The complete DUNE CD savegame editor. Uses AvaloniaUI for the GUI.

If a command line option is provided, the GUI won't be launched. Instead, it will be a console program.

Command Line Options:
  -c, --Compress           (Default: ) Compress the specified file

  -i, --InputFile          Required. The savegame to edit or describe on the standard output, for example DUNE37S1.SAV. For reading ONLY, it can be several files, separated by a space.

  -o, --OutputFile         (Default: ) Savegame output file name after a Compress or Write

  -r, --Read               (Default: true) Describe the entire save game on the standard output (partially implemented)

  -u, --Uncompress         (Default: false) Save the uncompressed input savegame to disk as [Filename.SAV.UNCOMPRESSED]

  -p, --PauseBeforeExit    (Default: false) Wait a key press before exiting

  -w, --Write              Write hex at position to the FIRST input file, before recompressing it. Format: ByteHexValue,UncompressedSaveGameHexPosition. Can be several Byte and Position couples, separated by a space. For example: 0x00,0x1 0x01,0x2

  --help                   Display this help screen.

  --version                Display version information.
