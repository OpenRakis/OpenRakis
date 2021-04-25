namespace DuneImpactor
{
    using Microsoft.VisualBasic.CompilerServices;

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    internal static class DuneImpactor
    {
        public static byte[] DatFileVersion = new byte[2]
        {
            (byte) 61,
            (byte) 10
        };

        public static void Print_Welcome()
        {
            Console.WriteLine("Unofficial Dune (PC VERSION) Impactor V1.0");
            Console.WriteLine($"Tgames (c) 2019{Environment.NewLine}");
        }

        public static void Print_Help()
        {
            Console.WriteLine("Usage : DuneImpactor.exe folder");
            Console.WriteLine("Write the full path to the folder.");
            Console.WriteLine("It will creates a DatFile with all files from the folder impacted.");
        }

        public static void WriteLog(string log, string folder)
        {
            var contents = $"Unofficial Dune (PC VERSION) Impactor V1.0{Environment.NewLine}Tgames (c) 2019{Environment.NewLine}{Environment.NewLine}-------------- Begin Log File --------------{Environment.NewLine}{Environment.NewLine}{log}{Environment.NewLine}{Environment.NewLine}-------------- End Log File -----------------";
            File.WriteAllText(Path.Combine(folder, "DuneImpactor.log"), contents, Encoding.Default);
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

        public static void WriteDuneDatFile(string path)
        {
            string str1 = "";
            string str2 = path.Substring(0, checked(path.Length - 1));
            string[] strArray1 = str2.Split('\\');
            string str3 = strArray1[checked(strArray1.Length - 1)].ToUpperInvariant() + ".DAT";
            string folder = str2.Substring(0, str2.LastIndexOf("\\")) + "\\";
            Console.WriteLine($"[STARTING] Packing of the DatFile has started...{Environment.NewLine}");
            string str4 = str1 + $"[STARTING] Packing of the DatFile has started...{Environment.NewLine}{Environment.NewLine}";
            if (File.Exists(folder + str3))
            {
                File.Delete(folder + str3);
            }

            FileStream input1 = new FileStream(folder + str3, FileMode.Create);
            input1.Seek(0L, SeekOrigin.Begin);
            WritetoFileStream(input1, DatFileVersion);
            Console.WriteLine($"[STEP 1] Reading all files and folders...{Environment.NewLine}");
            string str5 = str4 + $"[STEP 1] Reading all files and folders...{Environment.NewLine}{Environment.NewLine}";
            string[] listOfFiles = DuneFiles.ListOfFiles;
            Console.WriteLine(listOfFiles.Length.ToString() + $" files founds...{Environment.NewLine}");
            string str6 = str5 + listOfFiles.Length.ToString() + $" files founds...{Environment.NewLine}{Environment.NewLine}";
            var arrayList = new ArrayList();
            int number = 65536;
            int num = checked(listOfFiles.Length - 1);
            int index1 = 0;
            while (index1 <= num)
            {
                listOfFiles[index1] = listOfFiles[index1].Replace(path, "");
                if (File.Exists(path + listOfFiles[index1]))
                {
                    DatasSection datasSection2 = new DatasSection();
                    int length = checked((int)new FileInfo(path + listOfFiles[index1]).Length);
                    byte[] bytes = UnicodeStringToBytes(listOfFiles[index1]);
                    datasSection2.NameOfFile = (byte[])AddNullBytesUntilEnd(bytes, 16);
                    datasSection2.SizeOfFile = UnicodeIntegerToBytes(length);
                    datasSection2.OffsetOfFile = UnicodeIntegerToBytes(number);
                    datasSection2.Unused = new byte[1];
                    arrayList.Add((object)datasSection2);
                    checked { number += length; }
                }
                checked { ++index1; }
            }
            Console.WriteLine($"[STEP 2] Writing the header to the DatFile...{Environment.NewLine}");
            string str7 = str6 + $"[STEP 2] Writing the header to the DatFile...{Environment.NewLine}{Environment.NewLine}";
            for (int i = 0; i < arrayList.Count; i++)
            {
                DatasSection datasSection2 = (DatasSection)arrayList[i];
                WritetoFileStream(input1, datasSection2.NameOfFile);
                WritetoFileStream(input1, datasSection2.SizeOfFile);
                WritetoFileStream(input1, datasSection2.OffsetOfFile);
                WritetoFileStream(input1, datasSection2.Unused);
            }
            int position = checked((int)input1.Position);
            while (position <= (int)ushort.MaxValue)
            {
                WritetoFileStream(input1, new byte[1]);
                checked { ++position; }
            }
            Console.WriteLine($"The header has been successfully written.{Environment.NewLine}");
            string str8 = str7 + $"The header has been successfully written.{Environment.NewLine}{Environment.NewLine}";
            Console.WriteLine($"[STEP 3] Writing all files to the DatFile...{Environment.NewLine}");
            string str9 = str8 + $"[STEP 3] Writing all files to the DatFile...{Environment.NewLine}{Environment.NewLine}";
            input1.Close();
            string[] strArray2 = listOfFiles;
            int index2 = 0;
            while (index2 < strArray2.Length)
            {
                string str10 = strArray2[index2];
                var input2 = new FileStream(path + str10, FileMode.Open);
                input2.Seek(0L, SeekOrigin.Begin);
                ReadWritetoFileStream(input2, folder + str3, checked((uint)input2.Length));
                input2.Close();
                Console.WriteLine("Writing... " + str10);
                str9 = str9 + "Writing... " + str10 + $"{Environment.NewLine}";
                checked { ++index2; }
            }
            Console.WriteLine($"{Environment.NewLine}[DONE] Packing finished !{Environment.NewLine}");
            WriteLog(str9 + $"{Environment.NewLine}[DONE] Packing finished !{Environment.NewLine}{Environment.NewLine}", folder);
        }
    }
}