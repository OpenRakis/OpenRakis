﻿// Decompiled with JetBrains decompiler
// Type: LipsExtractor.My.MyProject
// Assembly: LipsExtractor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 393FD261-700B-4FDB-9835-6F5E433805E3
// Assembly location: C:\Users\noalm\source\repos\OpenRakis\tools\cd\LipsExtractor.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LipsExtractor.My
{
  [StandardModule]
  [HideModuleName]
  [GeneratedCode("MyTemplate", "11.0.0.0")]
  internal sealed class MyProject
  {
    private static readonly MyProject.ThreadSafeObjectProvider<MyComputer> m_ComputerObjectProvider = new MyProject.ThreadSafeObjectProvider<MyComputer>();
    private static readonly MyProject.ThreadSafeObjectProvider<MyApplication> m_AppObjectProvider = new MyProject.ThreadSafeObjectProvider<MyApplication>();
    private static readonly MyProject.ThreadSafeObjectProvider<User> m_UserObjectProvider = new MyProject.ThreadSafeObjectProvider<User>();
    private static readonly MyProject.ThreadSafeObjectProvider<MyProject.MyWebServices> m_MyWebServicesObjectProvider = new MyProject.ThreadSafeObjectProvider<MyProject.MyWebServices>();

    [HelpKeyword("My.Computer")]
    internal static MyComputer Computer
    {
      [DebuggerHidden] get => MyProject.m_ComputerObjectProvider.GetInstance;
    }

    [HelpKeyword("My.Application")]
    internal static MyApplication Application
    {
      [DebuggerHidden] get => MyProject.m_AppObjectProvider.GetInstance;
    }

    [HelpKeyword("My.User")]
    internal static User User
    {
      [DebuggerHidden] get => MyProject.m_UserObjectProvider.GetInstance;
    }

    [HelpKeyword("My.WebServices")]
    internal static MyProject.MyWebServices WebServices
    {
      [DebuggerHidden] get => MyProject.m_MyWebServicesObjectProvider.GetInstance;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [MyGroupCollection("System.Web.Services.Protocols.SoapHttpClientProtocol", "Create__Instance__", "Dispose__Instance__", "")]
    internal sealed class MyWebServices
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      [DebuggerHidden]
      public override bool Equals(object o) => base.Equals(RuntimeHelpers.GetObjectValue(o));

      [EditorBrowsable(EditorBrowsableState.Never)]
      [DebuggerHidden]
      public override int GetHashCode() => base.GetHashCode();

      [EditorBrowsable(EditorBrowsableState.Never)]
      [DebuggerHidden]
      internal new Type GetType() => typeof (MyProject.MyWebServices);

      [EditorBrowsable(EditorBrowsableState.Never)]
      [DebuggerHidden]
      public override string ToString() => base.ToString();

      [DebuggerHidden]
      private static T Create__Instance__<T>(T instance) where T : new() => (object) instance == null ? new T() : instance;

      [DebuggerHidden]
      private void Dispose__Instance__<T>(ref T instance) => instance = default (T);

      [DebuggerHidden]
      [EditorBrowsable(EditorBrowsableState.Never)]
      public MyWebServices()
      {
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [ComVisible(false)]
    internal sealed class ThreadSafeObjectProvider<T> where T : new()
    {
      internal T GetInstance
      {
        [DebuggerHidden] get
        {
          if ((object) MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue == null)
            MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue = new T();
          return MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue;
        }
      }

      [DebuggerHidden]
      [EditorBrowsable(EditorBrowsableState.Never)]
      public ThreadSafeObjectProvider()
      {
      }
    }
  }
}
