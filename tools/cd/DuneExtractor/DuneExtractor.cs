namespace DuneExtractor
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;

    using System;
    using System.Collections;
    using System.IO;
    using System.Text;

    internal static class DuneExtractor
    {
        internal static void Print_Welcome()
        {
            Console.WriteLine("Unofficial Dune (PC VERSION) Extractor V1.0");
            Console.WriteLine($"Tgames (c) 2019{Environment.NewLine}");
        }

        internal static void Print_Help()
        {
            Console.WriteLine("Usage : DuneExtractor.exe DUNE.DAT");
            Console.WriteLine("Write the full path to the DUNE.DAT.");
            Console.WriteLine("It will creates a folder with all files extracted.");
        }

        public static void WriteLog(string log, string folder)
        {
            var contents = $"Unofficial Dune (PC VERSION) Extractor V1.0{Environment.NewLine}Tgames (c) 2019{Environment.NewLine}{Environment.NewLine}-------------- Begin Log File --------------{Environment.NewLine}{Environment.NewLine}{log}{{Environment.NewLine}}{{Environment.NewLine}}-------------- End Log File ----------------";
            File.WriteAllText(Path.Combine(folder, $"{nameof(DuneExtractor)}.log"), contents, Encoding.Default);
        }

        public static string TimeToUnix(DateTime dteDate)
        {
            if (dteDate.IsDaylightSavingTime())
            {
                dteDate = DateAndTime.DateAdd(DateInterval.Hour, -1.0, dteDate);
            }

            return Conversions.ToString(DateAndTime.DateDiff(DateInterval.Second, new DateTime(621355968000000000L), dteDate));
        }

        private static string UnicodeBytesToString(byte[] bytes) => Encoding.UTF8.GetString(bytes);

        private static int UnicodeBytesToInteger(byte[] bytes) => BitConverter.ToInt32(bytes, 0);

        public static byte[] ReadData(FileStream DatFile, int size)
        {
            int index = 0;
            byte[] numArray = new byte[checked(size + 1)];
            while (index < DatFile.Length & index < size)
            {
                numArray[index] = checked((byte)DatFile.ReadByte());
                checked { ++index; }
            }
            return numArray;
        }

        public static byte[] ReadDataFileName(FileStream DatFile, int size)
        {
            int index = 0;
            int newSize = 0;
            byte[] array = new byte[checked(size + 1)];
            while (index < DatFile.Length & index < size)
            {
                byte num = checked((byte)DatFile.ReadByte());
                if (num > (byte)0)
                {
                    array[index] = num;
                    checked { ++newSize; }
                }
                checked { ++index; }
            }
            Array.Resize(ref array, newSize);
            return array;
        }

        public static void ReadDatFileSectionHeader(FileStream DatFile, DatasSection section)
        {
            section.NameOfFile = UnicodeBytesToString(DuneExtractor.ReadDataFileName(DatFile, 16));
            section.SizeOfFile = UnicodeBytesToInteger(DuneExtractor.ReadData(DatFile, 4));
            section.OffsetOfFile = UnicodeBytesToInteger(DuneExtractor.ReadData(DatFile, 4));
        }

        public static void ReadWritetoFileStream(FileStream input, string target, int length)
        {
            byte[] numArray = new byte[8193];
            Stream stream = (Stream)File.OpenWrite(target);
            int count = 1;
            while (length > 0 & count > 0)
            {
                count = input.Read(numArray, 0, Math.Min(length, numArray.Length));
                stream.Write(numArray, 0, count);
                checked { length -= count; }
            }
            stream.Close();
        }

        public static void ReadWriteSectionData(FileStream DatFile, string filepath, int offset, int size)
        {
            string[] strArray = filepath.Split('\\');
            string str = strArray[checked(strArray.Length - 1)];
            string path = $"{filepath.Substring(0, filepath.LastIndexOf("\\"))}\\";
            DatFile.Seek(offset, SeekOrigin.Begin);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (File.Exists($"{path}{str}"))
            {
                File.Delete($"{path}{str}");
            }

            ReadWritetoFileStream(DatFile, path + str, size);
        }

        public static void ExtractDuneDatFile(string file)
        {
            string str1 = "";
            string[] strArray = file.Split('\\');
            string str2 = strArray[checked(strArray.Length - 1)];
            string folder = file.Substring(0, file.LastIndexOf("\\")) + "\\";
            Console.WriteLine($"[STARTING] Extraction of the DAT file has started...{Environment.NewLine}");
            string str3 = str1 + $"[STARTING] Extraction of the DAT file has started...{Environment.NewLine}{Environment.NewLine}";
            FileStream DatFile = new FileStream(file, FileMode.Open);
            DatFile.Seek(0L, SeekOrigin.Begin);
            Console.WriteLine($"[STEP 1] Reading DatFile header... PLEASE WAIT{Environment.NewLine}");
            string str4 = str3 + $"[STEP 1] Reading DatFile header... PLEASE WAIT{Environment.NewLine}{Environment.NewLine}";
            byte[] numArray2 = new byte[2] { (byte)61, (byte)10 };
            byte[] numArray3 = ReadData(DatFile, 2);
            string log;
            if ((int)numArray3[0] == (int)numArray2[0] & (int)numArray3[1] == (int)numArray2[1])
            {
                Console.WriteLine($"Cryo DatFile Detected... {Environment.NewLine}");
                string str5 = str4 + $"DatFile Detected... {Environment.NewLine}{Environment.NewLine}";
                Console.WriteLine($"[STEP 2] Extracting each files... PLEASE WAIT{Environment.NewLine}");
                string str6 = str5 + $"[STEP 2] Extracting each files... PLEASE WAIT{Environment.NewLine}{Environment.NewLine}";
                var arrayList = new ArrayList();
                int num = 0;
                do
                {
                    DatasSection section = new DatasSection();
                    ReadDatFileSectionHeader(DatFile, section);
                    if ((uint)Operators.CompareString(section.NameOfFile, "", false) > 0U)
                    {
                        arrayList.Add((object)section);
                        DatFile.ReadByte();
                    }
                    else
                        break;
                }
                while (!Operators.ConditionalCompareObjectGreater(num, NewLateBinding.LateGet(arrayList[0], null, "OffsetOfFile", Array.Empty<object>(), null, null, null), false));
                folder = folder + str2 + "_\\";
                try
                {
                    for (int i = 0; i < arrayList.Count; i++)
                    {
                        DatasSection datasSection = (DatasSection)arrayList[i];
                        Console.WriteLine($"Extracting... {datasSection.NameOfFile} ({Math.Round(datasSection.SizeOfFile / 1024.0, 2)} KB)");
                        str6 = $"{str6}Extracting... {datasSection.NameOfFile} ({Math.Round(datasSection.SizeOfFile / 1024.0, 2)} KB){Environment.NewLine}";
                        ReadWriteSectionData(DatFile, $"{folder}{datasSection.NameOfFile}", datasSection.OffsetOfFile, datasSection.SizeOfFile);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.GetBaseException().GetType()}{Environment.NewLine}{e.GetBaseException().Message}{Environment.NewLine}{e.GetBaseException().StackTrace}");
                }
                Console.WriteLine($"{Environment.NewLine}[DONE] Extraction finished !{Environment.NewLine}");
                log = $"{str6}{Environment.NewLine}[DONE] Extraction finished !{Environment.NewLine}";
            }
            else
            {
                Console.WriteLine($"FATAL ERROR: {file} is not a valid Cryo DatFile !{Environment.NewLine}");
                log = $"{str4}FATAL ERROR: {file} is not a valid Cryo DatFile !{Environment.NewLine}{Environment.NewLine}";
            }
            DatFile.Close();
            WriteLog(log, folder);
        }
    }
}