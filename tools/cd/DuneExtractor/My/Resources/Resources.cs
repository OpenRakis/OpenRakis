// Decompiled with JetBrains decompiler
// Type: DuneExtractor.My.Resources.Resources
// Assembly: DuneExtractor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D4B4927-803D-4A37-8906-B7E3ADF4C1FD
// Assembly location: C:\Users\noalm\source\repos\OpenRakis\tools\cd\DuneExtractor.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace DuneExtractor.My.Resources
{
  [StandardModule]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  [HideModuleName]
  internal sealed class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) DuneExtractor.My.Resources.Resources.resourceMan, (object) null))
          DuneExtractor.My.Resources.Resources.resourceMan = new ResourceManager("DuneExtractor.Resources", typeof (DuneExtractor.My.Resources.Resources).Assembly);
        return DuneExtractor.My.Resources.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => DuneExtractor.My.Resources.Resources.resourceCulture;
      set => DuneExtractor.My.Resources.Resources.resourceCulture = value;
    }
  }
}
