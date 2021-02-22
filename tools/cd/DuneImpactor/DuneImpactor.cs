// Decompiled with JetBrains decompiler
// Type: DuneImpactor.DuneImpactor
// Assembly: DuneImpactor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57D56F88-797B-46D1-80DE-69E1258C78C2
// Assembly location: C:\temp\DuneImpactor.exe

using Microsoft.VisualBasic.CompilerServices;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DuneImpactor
{
    [StandardModule]
    internal sealed class DuneImpactor
    {
        public static byte[] DatFileVersion = new byte[2]
        {
      (byte) 61,
      (byte) 10
        };

        private static void Print_Welcome()
        {
            Console.WriteLine("Unofficial Dune (PC VERSION) Impactor V1.0");
            Console.WriteLine("Private Tool - Don't share !");
            Console.WriteLine("Tgames (c) 2019\r\n");
        }

        private static void Print_Help()
        {
            Console.WriteLine("Usage : DuneImpactor.exe folder");
            Console.WriteLine("Write the full path to the folder.");
            Console.WriteLine("It will creates a DatFile with all files from the folder impacted.");
        }

        public static void WriteLog(string log, string folder)
        {
            string contents = "Unofficial Dune (PC VERSION) Impactor V1.0\r\nTgames (c) 2019\r\n\r\n-------------- Begin Log File --------------\r\n\r\n" + log + "\r\n\r\n-------------- End Log File ----------------";
            File.WriteAllText(folder + "DuneImpactor.log", contents, Encoding.Default);
        }

        private static byte[] UnicodeStringToBytes(string str) => Encoding.UTF8.GetBytes(str);

        private static byte[] UnicodeIntegerToBytes(int number) => BitConverter.GetBytes(number);

        public static void WritetoFileStream(FileStream input, byte[] data)
        {
            byte[] numArray = data;
            int index = 0;
            while (index < numArray.Length)
            {
                byte num = numArray[index];
                input.WriteByte(num);
                checked { ++index; }
            }
        }

        public static void ReadWritetoFileStream(FileStream input, string target, uint length)
        {
            byte[] numArray = new byte[8193];
            Stream stream = (Stream)File.OpenWrite(target);
            stream.Seek(0L, SeekOrigin.End);
            for (int count = 1; length > 0U & count > 0; length = checked((uint)((long)length - (long)count)))
            {
                count = input.Read(numArray, 0, checked((int)Math.Min((long)length, (long)numArray.Length)));
                stream.Write(numArray, 0, count);
            }
            stream.Close();
        }

        public static object AddNullBytesUntilEnd(byte[] input, int size)
        {
            byte[] numArray = new byte[16];
            int num = checked(size - 1);
            int index = 0;
            while (index <= num)
            {
                numArray[index] = index >= input.Length ? (byte)0 : input[index];
                checked { ++index; }
            }
            return (object)numArray;
        }

        public static void ImpactDat(string path)
        {
            string str1 = "";
            string str2 = path.Substring(0, checked(path.Length - 1));
            string[] strArray1 = str2.Split('\\');
            string str3 = strArray1[checked(strArray1.Length - 1)].ToUpper() + ".DAT";
            string folder = str2.Substring(0, str2.LastIndexOf("\\")) + "\\";
            Console.WriteLine("[STARTING] Packing of the DatFile has started...\r\n");
            string str4 = str1 + "[STARTING] Packing of the DatFile has started...\r\n\r\n";
            if (File.Exists(folder + str3))
                File.Delete(folder + str3);
            FileStream input1 = new FileStream(folder + str3, FileMode.Create);
            input1.Seek(0L, SeekOrigin.Begin);
            DuneImpactor.WritetoFileStream(input1, DuneImpactor.DatFileVersion);
            Console.WriteLine("[STEP 1] Reading all files and folders...\r\n");
            string str5 = str4 + "[STEP 1] Reading all files and folders...\r\n\r\n";
            string[] listOfFiles = DuneFiles.ListOfFiles;
            Console.WriteLine(listOfFiles.Length.ToString() + " files founds...\r\n");
            string str6 = str5 + listOfFiles.Length.ToString() + " files founds...\r\n\r\n";
            ArrayList arrayList = new ArrayList();
            int number = 65536;
            int num = checked(listOfFiles.Length - 1);
            int index1 = 0;
            while (index1 <= num)
            {
                listOfFiles[index1] = listOfFiles[index1].Replace(path, "");
                if (File.Exists(path + listOfFiles[index1]))
                {
                    DuneImpactor.DatasSection datasSection2 = new DuneImpactor.DatasSection();
                    int length = checked((int)new FileInfo(path + listOfFiles[index1]).Length);
                    byte[] bytes = DuneImpactor.UnicodeStringToBytes(listOfFiles[index1]);
                    datasSection2.NameOfFile = (byte[])DuneImpactor.AddNullBytesUntilEnd(bytes, 16);
                    datasSection2.SizeOfFile = DuneImpactor.UnicodeIntegerToBytes(length);
                    datasSection2.OffsetOfFile = DuneImpactor.UnicodeIntegerToBytes(number);
                    datasSection2.Unused = new byte[1];
                    arrayList.Add((object)datasSection2);
                    checked { number += length; }
                }
                checked { ++index1; }
            }
            Console.WriteLine("[STEP 2] Writing the header to the DatFile...\r\n");
            string str7 = str6 + "[STEP 2] Writing the header to the DatFile...\r\n\r\n";
            foreach (DuneImpactor.DatasSection datasSection2 in arrayList)
            {
                DuneImpactor.WritetoFileStream(input1, datasSection2.NameOfFile);
                DuneImpactor.WritetoFileStream(input1, datasSection2.SizeOfFile);
                DuneImpactor.WritetoFileStream(input1, datasSection2.OffsetOfFile);
                DuneImpactor.WritetoFileStream(input1, datasSection2.Unused);
            }
            int position = checked((int)input1.Position);
            while (position <= (int)ushort.MaxValue)
            {
                DuneImpactor.WritetoFileStream(input1, new byte[1]);
                checked { ++position; }
            }
            Console.WriteLine("The header has been successfully written.\r\n");
            string str8 = str7 + "The header has been successfully written.\r\n\r\n";
            Console.WriteLine("[STEP 3] Writing all files to the DatFile...\r\n");
            string str9 = str8 + "[STEP 3] Writing all files to the DatFile...\r\n\r\n";
            input1.Close();
            string[] strArray2 = listOfFiles;
            int index2 = 0;
            while (index2 < strArray2.Length)
            {
                string str10 = strArray2[index2];
                FileStream input2 = new FileStream(path + str10, FileMode.Open);
                input2.Seek(0L, SeekOrigin.Begin);
                DuneImpactor.ReadWritetoFileStream(input2, folder + str3, checked((uint)input2.Length));
                input2.Close();
                Console.WriteLine("Writing... " + str10);
                str9 = str9 + "Writing... " + str10 + "\r\n";
                checked { ++index2; }
            }
            Console.WriteLine("\r\n[DONE] Packing finished !\r\n");
            DuneImpactor.WriteLog(str9 + "\r\n[DONE] Packing finished !\r\n\r\n", folder);
        }

        [STAThread]
        public static void Main()
        {
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            DuneImpactor.Print_Welcome();
            if (((IEnumerable<string>)commandLineArgs).Count<string>() != 2)
            {
                Console.WriteLine("FATAL Error: too much or not enough arguments.\r\n");
                DuneImpactor.Print_Help();
            }
            else
            {
                string str = commandLineArgs[1].Replace("\"", "").Replace("'", "").Replace("/", "\\");
                if ((uint)Operators.CompareString(Conversions.ToString(str.Last<char>()), "\\", false) > 0U)
                    str += "\\";
                if (Directory.Exists(str))
                    DuneImpactor.ImpactDat(str);
                else
                    Console.WriteLine("FATAL ERROR: " + str + " directory not exist !\r\n");
            }
            Console.WriteLine("\r\nPress Enter to exit.");
            Console.Read();
        }

        public class DatasSection
        {
            public byte[] NameOfFile;
            public byte[] SizeOfFile;
            public byte[] OffsetOfFile;
            public byte[] Unused;

            public DatasSection()
            {
                this.NameOfFile = new byte[16];
                this.SizeOfFile = new byte[5];
                this.OffsetOfFile = new byte[5];
                this.Unused = new byte[2];
            }
        }
    }
}