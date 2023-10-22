namespace DuneSaveDescriptor.Decompression;

internal static class SequenceParser
{
    public const int SaveCompressionSequenceStart = 0xF7;

    public static bool IsControlSequence(byte[] sequence) => sequence[0] == SaveCompressionSequenceStart && sequence[1] == 1 && sequence[2] == 0xF7;

    public static bool IsDeflateSequence(byte[] sequence) => sequence[0] == SaveCompressionSequenceStart && sequence[1] > 2;

    public static bool IsTrapSequence(byte[] sequence) => sequence[0] == SaveCompressionSequenceStart && sequence[1] == sequence[2];
}