// Decompiled with JetBrains decompiler
// Type: DuneExtractor.DuneExtractor
// Assembly: DuneExtractor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D4B4927-803D-4A37-8906-B7E3ADF4C1FD
// Assembly location: C:\Users\noalm\source\repos\OpenRakis\tools\cd\DuneExtractor.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DuneExtractor
{
  [StandardModule]
  internal sealed class DuneExtractor
  {
    private static void Print_Welcome()
    {
      Console.WriteLine("Unofficial Dune (PC VERSION) Extractor V1.0");
      Console.WriteLine("Private Tool - Don't share !");
      Console.WriteLine("Tgames (c) 2019\r\n");
    }

    private static void Print_Help()
    {
      Console.WriteLine("Usage : DuneExtractor.exe DUNE.DAT");
      Console.WriteLine("Write the full path to the DUNE.DAT.");
      Console.WriteLine("It will creates a folder with all files extracted.");
    }

    public static void WriteLog(string log, string folder)
    {
      string contents = "Unofficial Dune (PC VERSION) Extractor V1.0\r\nTgames (c) 2019\r\n\r\n-------------- Begin Log File --------------\r\n\r\n" + log + "\r\n\r\n-------------- End Log File ----------------";
      File.WriteAllText(folder + "DuneExtractor.log", contents, Encoding.Default);
    }

    public static string TimeToUnix(DateTime dteDate)
    {
      if (dteDate.IsDaylightSavingTime())
        dteDate = DateAndTime.DateAdd(DateInterval.Hour, -1.0, dteDate);
      return Conversions.ToString(DateAndTime.DateDiff(DateInterval.Second, new DateTime(621355968000000000L), dteDate));
    }

    private static string UnicodeBytesToString(byte[] bytes) => Encoding.UTF8.GetString(bytes);

    private static int UnicodeBytesToInteger(byte[] bytes) => BitConverter.ToInt32(bytes, 0);

    public static byte[] ReadData(FileStream DatFile, int size)
    {
      int index = 0;
      byte[] numArray = new byte[checked (size + 1)];
      while ((long) index < DatFile.Length & index < size)
      {
        numArray[index] = checked ((byte) DatFile.ReadByte());
        checked { ++index; }
      }
      return numArray;
    }

    public static byte[] ReadDataFileName(FileStream DatFile, int size)
    {
      int index = 0;
      int newSize = 0;
      byte[] array = new byte[checked (size + 1)];
      while ((long) index < DatFile.Length & index < size)
      {
        byte num = checked ((byte) DatFile.ReadByte());
        if (num > (byte) 0)
        {
          array[index] = num;
          checked { ++newSize; }
        }
        checked { ++index; }
      }
      Array.Resize<byte>(ref array, newSize);
      return array;
    }

    public static void ReadDatFileSectionHeader(
      FileStream DatFile,
      DuneExtractor.DuneExtractor.DatasSection section)
    {
      section.NameOfFile = DuneExtractor.DuneExtractor.UnicodeBytesToString(DuneExtractor.DuneExtractor.ReadDataFileName(DatFile, 16));
      section.SizeOfFile = DuneExtractor.DuneExtractor.UnicodeBytesToInteger(DuneExtractor.DuneExtractor.ReadData(DatFile, 4));
      section.OffsetOfFile = DuneExtractor.DuneExtractor.UnicodeBytesToInteger(DuneExtractor.DuneExtractor.ReadData(DatFile, 4));
    }

    public static void ReadWritetoFileStream(FileStream input, string target, int length)
    {
      byte[] numArray = new byte[8193];
      Stream stream = (Stream) File.OpenWrite(target);
      int count = 1;
      while (length > 0 & count > 0)
      {
        count = input.Read(numArray, 0, Math.Min(length, numArray.Length));
        stream.Write(numArray, 0, count);
        checked { length -= count; }
      }
      stream.Close();
    }

    public static void ReadWriteSectionData(
      FileStream DatFile,
      string filepath,
      int offset,
      int size)
    {
      string[] strArray = filepath.Split('\\');
      string str = strArray[checked (strArray.Length - 1)];
      string path = filepath.Substring(0, filepath.LastIndexOf("\\")) + "\\";
      DatFile.Seek((long) offset, SeekOrigin.Begin);
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      if (File.Exists(path + str))
        File.Delete(path + str);
      DuneExtractor.DuneExtractor.ReadWritetoFileStream(DatFile, path + str, size);
    }

    public static void ExtractBig(string file)
    {
      string str1 = "";
      string[] strArray = file.Split('\\');
      string str2 = strArray[checked (strArray.Length - 1)];
      string folder = file.Substring(0, file.LastIndexOf("\\")) + "\\";
      Console.WriteLine("[STARTING] Extraction of the DAT file has started...\r\n");
      string str3 = str1 + "[STARTING] Extraction of the DAT file has started...\r\n\r\n";
      FileStream DatFile = new FileStream(file, FileMode.Open);
      DatFile.Seek(0L, SeekOrigin.Begin);
      Console.WriteLine("[STEP 1] Reading DatFile header... PLEASE WAIT\r\n");
      string str4 = str3 + "[STEP 1] Reading DatFile header... PLEASE WAIT\r\n\r\n";
      byte[] numArray1 = new byte[3];
      byte[] numArray2 = new byte[2]{ (byte) 61, (byte) 10 };
      byte[] numArray3 = DuneExtractor.DuneExtractor.ReadData(DatFile, 2);
      string log;
      if ((int) numArray3[0] == (int) numArray2[0] & (int) numArray3[1] == (int) numArray2[1])
      {
        Console.WriteLine("Cryo DatFile Detected... \r\n");
        string str5 = str4 + "DatFile Detected... \r\n\r\n";
        Console.WriteLine("[STEP 2] Extracting each files... PLEASE WAIT\r\n");
        string str6 = str5 + "[STEP 2] Extracting each files... PLEASE WAIT\r\n\r\n";
        ArrayList arrayList = new ArrayList();
        int num = 0;
        do
        {
          DuneExtractor.DuneExtractor.DatasSection section = new DuneExtractor.DuneExtractor.DatasSection();
          DuneExtractor.DuneExtractor.ReadDatFileSectionHeader(DatFile, section);
          if ((uint) Operators.CompareString(section.NameOfFile, "", false) > 0U)
          {
            arrayList.Add((object) section);
            DatFile.ReadByte();
          }
          else
            break;
        }
        while (!Operators.ConditionalCompareObjectGreater((object) num, NewLateBinding.LateGet(arrayList[0], (Type) null, "OffsetOfFile", new object[0], (string[]) null, (Type[]) null, (bool[]) null), false));
        folder = folder + str2 + "_\\";
        try
        {
          foreach (DuneExtractor.DuneExtractor.DatasSection datasSection in arrayList)
          {
            Console.WriteLine("Extracting... " + datasSection.NameOfFile + " (" + Math.Round((double) datasSection.SizeOfFile / 1024.0, 2).ToString() + " KB)");
            str6 = str6 + "Extracting... " + datasSection.NameOfFile + " (" + Math.Round((double) datasSection.SizeOfFile / 1024.0, 2).ToString() + " KB)\r\n";
            DuneExtractor.DuneExtractor.ReadWriteSectionData(DatFile, folder + datasSection.NameOfFile, datasSection.OffsetOfFile, datasSection.SizeOfFile);
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        Console.WriteLine("\r\n[DONE] Extraction finished !\r\n");
        log = str6 + "\r\n[DONE] Extraction finished !\r\n";
      }
      else
      {
        Console.WriteLine("FATAL ERROR: " + file + " is not a valid Cryo DatFile !\r\n");
        log = str4 + "FATAL ERROR: " + file + " is not a valid Cryo DatFile !\r\n\r\n";
      }
      DatFile.Close();
      DuneExtractor.DuneExtractor.WriteLog(log, folder);
    }

    [STAThread]
    public static void Main()
    {
      string[] commandLineArgs = Environment.GetCommandLineArgs();
      DuneExtractor.DuneExtractor.Print_Welcome();
      if (((IEnumerable<string>) commandLineArgs).Count<string>() != 2)
      {
        Console.WriteLine("FATAL Error: too much or not enough arguments.\r\n");
        DuneExtractor.DuneExtractor.Print_Help();
      }
      else
      {
        string str = commandLineArgs[1].Replace("\"", "").Replace("'", "").Replace("/", "\\");
        if (File.Exists(str))
          DuneExtractor.DuneExtractor.ExtractBig(str);
        else
          Console.WriteLine("FATAL ERROR: " + str + " file not exist !\r\n");
      }
      Console.WriteLine("\r\nPress Enter to exit.");
      Console.Read();
    }

    public class DatasSection
    {
      public string NameOfFile;
      public int SizeOfFile;
      public int OffsetOfFile;

      public DatasSection()
      {
        this.NameOfFile = "";
        this.SizeOfFile = 0;
        this.OffsetOfFile = 0;
      }
    }
  }
}
