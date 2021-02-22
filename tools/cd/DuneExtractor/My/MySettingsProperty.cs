// Decompiled with JetBrains decompiler
// Type: DuneExtractor.My.MySettingsProperty
// Assembly: DuneExtractor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D4B4927-803D-4A37-8906-B7E3ADF4C1FD
// Assembly location: C:\Users\noalm\source\repos\OpenRakis\tools\cd\DuneExtractor.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DuneExtractor.My
{
  [StandardModule]
  [HideModuleName]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal sealed class MySettingsProperty
  {
    [HelpKeyword("My.Settings")]
    internal static MySettings Settings
    {
      get
      {
        MySettings mySettings = MySettings.Default;
        return mySettings;
      }
    }
  }
}
