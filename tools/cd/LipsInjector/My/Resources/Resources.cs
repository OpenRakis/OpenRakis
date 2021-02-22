// Decompiled with JetBrains decompiler
// Type: LipsInjector.My.Resources.Resources
// Assembly: LipsInjector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A21D5E0-4A29-4B54-94F0-AD58B824FF22
// Assembly location: C:\Users\noalm\source\repos\OpenRakis\tools\cd\LipsInjector.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace LipsInjector.My.Resources
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
        if (object.ReferenceEquals((object) LipsInjector.My.Resources.Resources.resourceMan, (object) null))
          LipsInjector.My.Resources.Resources.resourceMan = new ResourceManager("LipsInjector.Resources", typeof (LipsInjector.My.Resources.Resources).Assembly);
        return LipsInjector.My.Resources.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => LipsInjector.My.Resources.Resources.resourceCulture;
      set => LipsInjector.My.Resources.Resources.resourceCulture = value;
    }
  }
}
