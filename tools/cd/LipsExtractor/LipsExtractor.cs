namespace LipsExtractor;

using System;
using System.IO;
using System.Text;

internal static class LipsExtractor
{
    public static void Print_Welcome()
    {
        Console.WriteLine("Unofficial DUNE CD (PC VERSION) LipsExtractor V1.1");
        Console.WriteLine($"Tgames (c) 2019{Environment.NewLine}");
    }

    public static void Print_Help()
    {
        Console.WriteLine($"Usage : LipsExtractor.exe VOCFOLDER{Path.DirectorySeparatorChar}");
        Console.WriteLine("Write the full path to the VOC Folder, in this folder put all .VOC files.");
        Console.WriteLine("It will creates a \"LIPS\" folder with all .LIP files extracted.");
    }

    public static void WriteLog(string log, string folder)
    {
        string contents = $"Unofficial DUNE CD (PC VERSION) LipsExtractor V1.0{Environment.NewLine}Tgames (c) 2019{Environment.NewLine}{Environment.NewLine}-------------- Begin Log File --------------{Environment.NewLine}{Environment.NewLine}{log}{Environment.NewLine}{Environment.NewLine}-------------- End Log File ----------------";
        File.WriteAllText(Path.Combine(folder, "lipsextractor.log"), contents, Encoding.Default);
    }

    public static void ExtractLips(string folder)
    {
        string log = "";
        if (!Directory.Exists(Path.Combine(folder, "LIPS")))
        {
            Directory.CreateDirectory(Path.Combine(folder, "LIPS"));
        }

        try
        {
            string[] array = Directory.GetFiles(Path.GetFullPath(folder));
            for (int i = 0; i < array.Length; i++)
            {
                var file = array[i];
                if (file.Contains(".VOC") | file.Contains(".voc"))
                {
                    string[] strArray2 = file.Split(Path.DirectorySeparatorChar);
                    string str = strArray2[checked(^1)].Split('.')[0];
                    byte num1 = 0;
                    int num2 = 0;
                    FileStream fileStream1 = new(file, FileMode.Open);
                    fileStream1.Seek(26L, SeekOrigin.Begin);
                    if (fileStream1.ReadByte() == 5)
                    {
                        fileStream1.Seek(26L, SeekOrigin.Begin);
                        FileStream fileStream2 = new(Path.Combine(Path.Combine(folder, "LIPS"), $"{str}.LIP"), FileMode.Create);
                        fileStream2.Seek(0L, SeekOrigin.Begin);
                        while (num2 < fileStream1.Length & num1 != byte.MaxValue)
                        {
                            num1 = checked((byte)fileStream1.ReadByte());
                            fileStream2.WriteByte(num1);
                            checked { ++num2; }
                        }
                        if (num2 < fileStream1.Length)
                        {
                            byte num3 = checked((byte)fileStream1.ReadByte());
                            if (num3 != 1)
                            {
                                fileStream2.WriteByte(num3);
                                while (num2 < fileStream1.Length & num3 != byte.MaxValue)
                                {
                                    num3 = checked((byte)fileStream1.ReadByte());
                                    fileStream2.WriteByte(num3);
                                    checked { ++num2; }
                                }
                            }
                        }
                        if (num2 >= fileStream1.Length)
                        {
                            Console.WriteLine($"FATAL ERROR : {str}.VOC readed seems to be corrupted");
                            File.Delete(Path.Combine(folder, $"{str}.LIP"));
                            log = $"{log}FATAL Error : {str}.VOC readed seems to be corrupted{Environment.NewLine}";
                        }
                        else
                        {
                            Console.WriteLine($"Lips data from {str}.VOC has been successfully extracted !");
                            log = $"{log}Lips data from {str}.VOC has been successfully extracted !{Environment.NewLine}";
                        }
                        fileStream2.Close();
                    }
                    else
                    {
                        Console.WriteLine($"No Lips data for {str}.VOC");
                        log = $"{log}No Lips data for {str}.VOC{Environment.NewLine}";
                    }
                    fileStream1.Close();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e.GetBaseException().GetType()}{Environment.NewLine}{e.GetBaseException().Message}{Environment.NewLine}{e.GetBaseException().StackTrace}");
        }
        WriteLog(log, folder);
    }
}