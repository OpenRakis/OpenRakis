using DuneEdit2;

using System;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("DuneEdit2.UnitTests")]

internal class SaveGameEditor
{
    private Options _options;

    public SaveGameEditor(Options options)
    {
        this._options = options;
    }

    internal void Run()
    {

    }
}