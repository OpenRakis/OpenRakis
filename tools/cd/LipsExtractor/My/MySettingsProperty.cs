// Decompiled with JetBrains decompiler
// Type: LipsExtractor.My.MySettingsProperty
// Assembly: LipsExtractor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 393FD261-700B-4FDB-9835-6F5E433805E3
// Assembly location: C:\Users\noalm\source\repos\OpenRakis\tools\cd\LipsExtractor.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LipsExtractor.My
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
