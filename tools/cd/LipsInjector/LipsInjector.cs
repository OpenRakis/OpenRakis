// Decompiled with JetBrains decompiler
// Type: LipsInjector.LipsInjector
// Assembly: LipsInjector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A21D5E0-4A29-4B54-94F0-AD58B824FF22
// Assembly location: C:\Users\noalm\source\repos\OpenRakis\tools\cd\LipsInjector.exe

using LipsInjector.My;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LipsInjector
{
  [StandardModule]
  internal sealed class LipsInjector
  {
    private static void Print_Welcome()
    {
      Console.WriteLine("Unofficial DUNE CD (PC VERSION) LipsInjector V1.1");
      Console.WriteLine("Tgames (c) 2019\r\n");
    }

    private static void Print_Help()
    {
      Console.WriteLine("Usage : LipsInjector.exe C:\\VOCFOLDER\\");
      Console.WriteLine("Write the full path to the VOC Folder, in this folder put all .VOC files and the \"LIPS\" folder with all .LIP.");
      Console.WriteLine("It will creates a \"DONE\" folder with all .VOC files with lips injected.");
    }

    public static void WriteLog(string log, string folder)
    {
      string contents = "Unofficial DUNE CD (PC VERSION) LipsInjector V1.0\r\nTgames (c) 2019\r\n\r\n-------------- Begin Log File --------------\r\n\r\n" + log + "\r\n\r\n-------------- End Log File ----------------";
      File.WriteAllText(folder + "lipsinjector.log", contents, Encoding.Default);
    }

    private static object folder_fromFileName(string file)
    {
      string[] strArray1 = new string[0];
      string str;
      if (file.Contains("\\"))
      {
        string[] strArray2 = file.Split('\\');
        str = strArray2[checked (strArray2.Length - 1)].Substring(0, 2);
      }
      else
        str = file.Substring(0, 2);
      // ISSUE: reference to a compiler-generated method
      switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(str))
      {
        case 570421568:
          if (Operators.CompareString(str, "PO", false) == 0)
            return (object) "PO";
          break;
        case 587199187:
          if (Operators.CompareString(str, "PN", false) == 0)
            return (object) "PN";
          break;
        case 603976806:
          if (Operators.CompareString(str, "PM", false) == 0)
            return (object) "PM";
          break;
        case 620754425:
          if (Operators.CompareString(str, "PL", false) == 0)
            return (object) "PL";
          break;
        case 637532044:
          if (Operators.CompareString(str, "PK", false) == 0)
            return (object) "PK";
          break;
        case 654309663:
          if (Operators.CompareString(str, "PJ", false) == 0)
            return (object) "PJ";
          break;
        case 671087282:
          if (Operators.CompareString(str, "PI", false) == 0)
            return (object) "PI";
          break;
        case 687864901:
          if (Operators.CompareString(str, "PH", false) == 0)
            return (object) "PH";
          break;
        case 704642520:
          if (Operators.CompareString(str, "PG", false) == 0)
            return (object) "PG";
          break;
        case 721420139:
          if (Operators.CompareString(str, "PF", false) == 0)
            return (object) "PF";
          break;
        case 738197758:
          if (Operators.CompareString(str, "PE", false) == 0)
            return (object) "PE";
          break;
        case 754975377:
          if (Operators.CompareString(str, "PD", false) == 0)
            return (object) "PD";
          break;
        case 771752996:
          if (Operators.CompareString(str, "PC", false) == 0)
            return (object) "PC";
          break;
        case 788530615:
          if (Operators.CompareString(str, "PB", false) == 0)
            return (object) "PB";
          break;
        case 805308234:
          if (Operators.CompareString(str, "PA", false) == 0)
            return (object) "PA";
          break;
        case 922751567:
          if (Operators.CompareString(str, "PZ", false) == 0)
            return (object) "PZ";
          break;
        case 1808861065:
          if (Operators.CompareString(str, "GU", false) == 0)
            return (object) "PF";
          break;
      }
      return (object) "";
    }

    public static void DispatchVOC(string folder)
    {
      string[] strArray1 = new string[0];
      if (!Directory.Exists(folder + "PJ"))
        Directory.CreateDirectory(folder + "PJ");
      if (!Directory.Exists(folder + "PL"))
        Directory.CreateDirectory(folder + "PL");
      if (!Directory.Exists(folder + "PN"))
        Directory.CreateDirectory(folder + "PN");
      if (!Directory.Exists(folder + "PM"))
        Directory.CreateDirectory(folder + "PM");
      if (!Directory.Exists(folder + "PF"))
        Directory.CreateDirectory(folder + "PF");
      if (!Directory.Exists(folder + "PK"))
        Directory.CreateDirectory(folder + "PK");
      if (!Directory.Exists(folder + "PZ"))
        Directory.CreateDirectory(folder + "PZ");
      if (!Directory.Exists(folder + "PC"))
        Directory.CreateDirectory(folder + "PC");
      if (!Directory.Exists(folder + "PG"))
        Directory.CreateDirectory(folder + "PG");
      if (!Directory.Exists(folder + "PD"))
        Directory.CreateDirectory(folder + "PD");
      if (!Directory.Exists(folder + "PB"))
        Directory.CreateDirectory(folder + "PB");
      if (!Directory.Exists(folder + "PI"))
        Directory.CreateDirectory(folder + "PI");
      if (!Directory.Exists(folder + "PO"))
        Directory.CreateDirectory(folder + "PO");
      if (!Directory.Exists(folder + "PE"))
        Directory.CreateDirectory(folder + "PE");
      if (!Directory.Exists(folder + "PH"))
        Directory.CreateDirectory(folder + "PH");
      if (!Directory.Exists(folder + "PA"))
        Directory.CreateDirectory(folder + "PA");
      try
      {
        foreach (string file in MyProject.Computer.FileSystem.GetFiles(folder))
        {
          if (file.Contains(".VOC"))
          {
            string[] strArray2 = file.Split('\\');
            string[] strArray3 = strArray2[checked (strArray2.Length - 1)].Split('.');
            if (File.Exists(Conversions.ToString(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject((object) folder, LipsInjector.LipsInjector.folder_fromFileName(strArray3[0])), (object) "\\"), (object) strArray3[0]), (object) ".VOC"))))
              File.Delete(Conversions.ToString(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject((object) folder, LipsInjector.LipsInjector.folder_fromFileName(strArray3[0])), (object) "\\"), (object) strArray3[0]), (object) ".VOC")));
            File.Move(file, Conversions.ToString(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject((object) folder, LipsInjector.LipsInjector.folder_fromFileName(strArray3[0])), (object) "\\"), (object) strArray3[0]), (object) ".VOC")));
          }
        }
      }
      finally
      {
        IEnumerator<string> enumerator;
        enumerator?.Dispose();
      }
    }

    public static void InjectLips(string vocsDir)
    {
      string log = "";
      string[] strArray1 = new string[0];
      byte num1 = 0;
      string folder = vocsDir + "LIPS\\";
      if (!Directory.Exists(vocsDir + "DONE"))
        Directory.CreateDirectory(vocsDir + "DONE");
      try
      {
        foreach (string file in MyProject.Computer.FileSystem.GetFiles(vocsDir))
        {
          if (file.Contains(".VOC"))
          {
            string[] strArray2 = file.Split('\\');
            string str = strArray2[checked (strArray2.Length - 1)].Split('.')[0];
            num1 = (byte) 0;
            int num2 = 0;
            if (File.Exists(folder + str + ".LIP"))
            {
              FileStream fileStream1 = new FileStream(file, FileMode.Open);
              fileStream1.Seek(0L, SeekOrigin.Begin);
              FileStream fileStream2 = new FileStream(vocsDir + "DONE\\" + str + ".VOC", FileMode.Create);
              fileStream2.Seek(0L, SeekOrigin.Begin);
              fileStream1.Seek(26L, SeekOrigin.Begin);
              if (fileStream1.ReadByte() != 5)
              {
                fileStream1.Seek(0L, SeekOrigin.Begin);
                while ((long) num2 < fileStream1.Length & num2 < 26)
                {
                  byte num3 = checked ((byte) fileStream1.ReadByte());
                  fileStream2.WriteByte(num3);
                  checked { ++num2; }
                }
                if ((long) num2 >= fileStream1.Length)
                {
                  fileStream2.Close();
                  Console.WriteLine("FATAL ERROR : " + str + ".VOC readed seems to be corrupted");
                  File.Delete(vocsDir + "DONE\\" + str + ".VOC");
                  log = log + "FATAL Error : " + str + ".VOC readed seems to be corrupted\r\n";
                }
                else
                {
                  num1 = (byte) 0;
                  int num3 = 0;
                  FileStream fileStream3 = new FileStream(folder + str + ".LIP", FileMode.Open);
                  fileStream3.Seek(0L, SeekOrigin.Begin);
                  while ((long) num3 < fileStream3.Length)
                  {
                    byte num4 = checked ((byte) fileStream3.ReadByte());
                    fileStream2.WriteByte(num4);
                    checked { ++num3; }
                  }
                  fileStream3.Close();
                  if (num3 < 4)
                  {
                    fileStream2.Close();
                    Console.WriteLine("FATAL ERROR : " + str + ".LIP readed seems to be corrupted");
                    File.Delete(vocsDir + "DONE\\" + str + ".VOC");
                    log = log + "FATAL Error : " + str + ".LIP readed seems to be corrupted\r\n";
                  }
                  else
                  {
                    num1 = (byte) 0;
                    int position = checked ((int) fileStream1.Position);
                    while ((long) position < fileStream1.Length)
                    {
                      byte num4 = checked ((byte) fileStream1.ReadByte());
                      fileStream2.WriteByte(num4);
                      checked { ++position; }
                    }
                    if (position < 20)
                    {
                      fileStream2.Close();
                      Console.WriteLine("FATAL ERROR : " + str + ".VOC readed seems to be corrupted");
                      File.Delete(vocsDir + "DONE\\" + str + ".VOC");
                      log = log + "FATAL Error : " + str + ".VOC readed seems to be corrupted\r\n";
                    }
                    else
                    {
                      fileStream2.Close();
                      Console.WriteLine("The lip data has been successfully injected into " + str + ".VOC");
                      log = log + "The lip data has been successfully injected into " + str + ".VOC\r\n";
                    }
                  }
                }
              }
              else
              {
                fileStream2.Close();
                Console.WriteLine("FATAL ERROR : " + str + ".VOC has already LIP data !");
                File.Delete(vocsDir + "DONE\\" + str + ".VOC");
                log = log + "FATAL Error : " + str + ".VOC has already LIP data !\r\n";
              }
              fileStream1.Close();
            }
            else
            {
              Console.WriteLine("No Lips file available for " + str + ".VOC");
              log = log + "No Lips file available for " + str + ".VOC\r\n";
            }
          }
        }
      }
      finally
      {
        IEnumerator<string> enumerator;
        enumerator?.Dispose();
      }
      LipsInjector.LipsInjector.WriteLog(log, folder);
    }

    [STAThread]
    public static void Main()
    {
      string[] commandLineArgs = Environment.GetCommandLineArgs();
      LipsInjector.LipsInjector.Print_Welcome();
      if (((IEnumerable<string>) commandLineArgs).Count<string>() != 2)
      {
        Console.WriteLine("FATAL ERROR: too much or not enough arguments.\r\n");
        LipsInjector.LipsInjector.Print_Help();
      }
      else
      {
        string str = commandLineArgs[1].Replace("\"", "").Replace("'", "").Replace("/", "\\");
        if ((uint) Operators.CompareString(Conversions.ToString(str.Last<char>()), "\\", false) > 0U)
          str += "\\";
        if (!Directory.Exists(str))
          Console.WriteLine("FATAL ERROR: " + str + " directory not exist !\r\n");
        else if (!Directory.Exists(str + "LIPS\\"))
        {
          Console.WriteLine("FATAL ERROR: " + str + "LIPS\\ directory not exist !\r\n");
        }
        else
        {
          LipsInjector.LipsInjector.InjectLips(str);
          LipsInjector.LipsInjector.DispatchVOC(str + "DONE\\");
        }
      }
      Console.WriteLine("\r\nPress Enter to exit.");
      Console.Read();
    }
  }
}
