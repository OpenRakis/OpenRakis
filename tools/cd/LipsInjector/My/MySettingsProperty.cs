// Decompiled with JetBrains decompiler
// Type: LipsInjector.My.MySettingsProperty
// Assembly: LipsInjector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A21D5E0-4A29-4B54-94F0-AD58B824FF22
// Assembly location: C:\Users\noalm\source\repos\OpenRakis\tools\cd\LipsInjector.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LipsInjector.My
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
