// Decompiled with JetBrains decompiler
// Type: LipsInjector.My.MySettings
// Assembly: LipsInjector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A21D5E0-4A29-4B54-94F0-AD58B824FF22
// Assembly location: C:\Users\noalm\source\repos\OpenRakis\tools\cd\LipsInjector.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace LipsInjector.My
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal sealed class MySettings : ApplicationSettingsBase
  {
    private static MySettings defaultInstance = (MySettings) SettingsBase.Synchronized((SettingsBase) new MySettings());

    public static MySettings Default
    {
      get
      {
        MySettings defaultInstance = MySettings.defaultInstance;
        return defaultInstance;
      }
    }
  }
}
