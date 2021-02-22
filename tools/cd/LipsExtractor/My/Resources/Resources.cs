// Decompiled with JetBrains decompiler
// Type: LipsExtractor.My.Resources.Resources
// Assembly: LipsExtractor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 393FD261-700B-4FDB-9835-6F5E433805E3
// Assembly location: C:\Users\noalm\source\repos\OpenRakis\tools\cd\LipsExtractor.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace LipsExtractor.My.Resources
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
        if (object.ReferenceEquals((object) LipsExtractor.My.Resources.Resources.resourceMan, (object) null))
          LipsExtractor.My.Resources.Resources.resourceMan = new ResourceManager("LipsExtractor.Resources", typeof (LipsExtractor.My.Resources.Resources).Assembly);
        return LipsExtractor.My.Resources.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => LipsExtractor.My.Resources.Resources.resourceCulture;
      set => LipsExtractor.My.Resources.Resources.resourceCulture = value;
    }
  }
}
