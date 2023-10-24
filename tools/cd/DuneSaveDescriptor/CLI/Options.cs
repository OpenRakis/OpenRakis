namespace DuneSaveDescriptor.CLI;

using System.ComponentModel.DataAnnotations;
using CommandLine;

internal readonly record struct Options
{
    [Option('s', nameof(SaveFile), HelpText = "Input savefile path (DUNE 3.7 format)", Required = true)]
    [Required]
    public required string SaveFile { get; init; }
}
