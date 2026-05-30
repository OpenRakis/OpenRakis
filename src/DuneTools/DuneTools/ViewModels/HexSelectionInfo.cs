namespace DuneTools.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;

using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
public partial class HexSelectionInfo : ObservableObject
{
    [ObservableProperty]
    [property: ReadOnly(true)]
    [property: Category("Status")]
    [property: DisplayName("Message")]
    private string? _statusMessage;

    [ObservableProperty]
    [property: ReadOnly(true)]
    [property: Category("Selection")]
    [property: DisplayName("Offset (hex)")]
    private string? _offsetHex;

    [ObservableProperty]
    [property: ReadOnly(true)]
    [property: Category("Selection")]
    [property: DisplayName("Offset (decimal)")]
    private string? _offsetDecimal;

    [ObservableProperty]
    [property: ReadOnly(true)]
    [property: Category("Selection")]
    [property: DisplayName("Length (bytes)")]
    private string? _selectionLength;

    [ObservableProperty]
    [property: ReadOnly(true)]
    [property: Category("Selection")]
    [property: DisplayName("Truncated")]
    private bool _isTruncated;

    [ObservableProperty]
    [property: ReadOnly(true)]
    [property: Category("Bytes")]
    [property: DisplayName("Hex")]
    private string? _rawHex;

    [ObservableProperty]
    [property: ReadOnly(true)]
    [property: Category("Bytes")]
    [property: DisplayName("Decimal")]
    private string? _rawDecimal;

    [ObservableProperty]
    [property: ReadOnly(true)]
    [property: Category("Bytes")]
    [property: DisplayName("ASCII preview")]
    private string? _asciiPreview;

    [ObservableProperty]
    [property: ReadOnly(true)]
    [property: Category("Integers")]
    [property: DisplayName("UInt16 LE")]
    private string? _uint16LittleEndian;

    [ObservableProperty]
    [property: ReadOnly(true)]
    [property: Category("Integers")]
    [property: DisplayName("UInt32 LE")]
    private string? _uint32LittleEndian;

    [ObservableProperty]
    [property: ReadOnly(true)]
    [property: Category("Known Fields")]
    [property: DisplayName("Intersecting fields")]
    private string? _intersectingFields;

    [ObservableProperty]
    [property: ReadOnly(true)]
    [property: Category("Known Fields")]
    [property: DisplayName("Primary field")]
    private string? _primaryFieldName;

    [ObservableProperty]
    [property: ReadOnly(true)]
    [property: Category("Known Fields")]
    [property: DisplayName("Encoding")]
    private string? _primaryFieldEncoding;

    [ObservableProperty]
    [property: ReadOnly(true)]
    [property: Category("Known Fields")]
    [property: DisplayName("Decoded value")]
    private string? _primaryFieldValue;

    [ObservableProperty]
    [property: ReadOnly(true)]
    [property: Category("Known Fields")]
    [property: DisplayName("Description")]
    private string? _primaryFieldDescription;
}