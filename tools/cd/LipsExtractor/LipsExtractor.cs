// Decompiled with JetBrains decompiler
// Type: LipsExtractor.LipsExtractor
// Assembly: LipsExtractor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 393FD261-700B-4FDB-9835-6F5E433805E3
// Assembly location: C:\Users\noalm\source\repos\OpenRakis\tools\cd\LipsExtractor.exe

using LipsExtractor.My;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LipsExtractor
{
  [StandardModule]
  internal sealed class LipsExtractor
  {
    private static void Print_Welcome()
    {
      Console.WriteLine("Unofficial DUNE CD (PC VERSION) LipsExtractor V1.1");
      Console.WriteLine("Tgames (c) 2019\r\n");
    }

    private static void Print_Help()
    {
      Console.WriteLine("Usage : LipsExtractor.exe C:\\VOCFOLDER\\");
      Console.WriteLine("Write the full path to the VOC Folder, in this folder put all .VOC files.");
      Console.WriteLine("It will creates a \"LIPS\" folder with all .LIP files extracted.");
    }

    public static void WriteLog(string log, string folder)
    {
      string contents = "Unofficial DUNE CD (PC VERSION) LipsExtractor V1.0\r\nTgames (c) 2019\r\n\r\n-------------- Begin Log File --------------\r\n\r\n" + log + "\r\n\r\n-------------- End Log File ----------------";
      File.WriteAllText(folder + "lipsextractor.log", contents, Encoding.Default);
    }

    public static void ExtracsLips(string folder)
    {
      string log = "";
      string[] strArray1 = new string[0];
      if (!Directory.Exists(folder + "LIPS"))
        Directory.CreateDirectory(folder + "LIPS");
      try
      {
        foreach (string file in MyProject.Computer.FileSystem.GetFiles(folder))
        {
          if (file.Contains(".VOC") | file.Contains(".voc"))
          {
            string[] strArray2 = file.Split('\\');
            string str = strArray2[checked (strArray2.Length - 1)].Split('.')[0];
            byte num1 = 0;
            int num2 = 0;
            FileStream fileStream1 = new FileStream(file, FileMode.Open);
            fileStream1.Seek(26L, SeekOrigin.Begin);
            if (fileStream1.ReadByte() == 5)
            {
              fileStream1.Seek(26L, SeekOrigin.Begin);
              FileStream fileStream2 = new FileStream(folder + "LIPS\\" + str + ".LIP", FileMode.Create);
              fileStream2.Seek(0L, SeekOrigin.Begin);
              while ((long) num2 < fileStream1.Length & num1 != byte.MaxValue)
              {
                num1 = checked ((byte) fileStream1.ReadByte());
                fileStream2.WriteByte(num1);
                checked { ++num2; }
              }
              if ((long) num2 < fileStream1.Length)
              {
                byte num3 = checked ((byte) fileStream1.ReadByte());
                if (num3 != (byte) 1)
                {
                  fileStream2.WriteByte(num3);
                  while ((long) num2 < fileStream1.Length & num3 != byte.MaxValue)
                  {
                    num3 = checked ((byte) fileStream1.ReadByte());
                    fileStream2.WriteByte(num3);
                    checked { ++num2; }
                  }
                }
              }
              if ((long) num2 >= fileStream1.Length)
              {
                Console.WriteLine("FATAL ERROR : " + str + ".VOC readed seems to be corrupted");
                File.Delete(folder + "\\" + str + ".LIP");
                log = log + "FATAL Error : " + str + ".VOC readed seems to be corrupted\r\n";
              }
              else
              {
                Console.WriteLine("Lips data from " + str + ".VOC has been successfully extracted !");
                log = log + "Lips data from " + str + ".VOC has been successfully extracted !\r\n";
              }
              fileStream2.Close();
            }
            else
            {
              Console.WriteLine("No Lips data for " + str + ".VOC");
              log = log + "No Lips data for " + str + ".VOC\r\n";
            }
            fileStream1.Close();
          }
        }
      }
      finally
      {
        IEnumerator<string> enumerator;
        enumerator?.Dispose();
      }
      LipsExtractor.LipsExtractor.WriteLog(log, folder);
    }

    [STAThread]
    public static void Main()
    {
      string[] commandLineArgs = Environment.GetCommandLineArgs();
      LipsExtractor.LipsExtractor.Print_Welcome();
      if (((IEnumerable<string>) commandLineArgs).Count<string>() != 2)
      {
        Console.WriteLine("FATAL ERROR: too much or not enough arguments.\r\n");
        LipsExtractor.LipsExtractor.Print_Help();
      }
      else
      {
        string str = commandLineArgs[1].Replace("\"", "").Replace("'", "").Replace("/", "\\");
        if ((uint) Operators.CompareString(Conversions.ToString(str.Last<char>()), "\\", false) > 0U)
          str += "\\";
        if (Directory.Exists(str))
          LipsExtractor.LipsExtractor.ExtracsLips(str);
        else
          Console.WriteLine("FATAL ERROR: " + str + " directory not exist !\r\n");
      }
      Console.WriteLine("\r\nPress Enter to exit.");
      Console.Read();
    }
  }
}
