﻿namespace LipsInjector;

using System;
using System.IO;
using System.Text;

internal static class LipsInjector
{
    public static void Print_Welcome()
    {
        Console.WriteLine("Unofficial DUNE CD (PC VERSION) LipsInjector V1.1");
        Console.WriteLine($"Tgames (c) 2019{Environment.NewLine}");
    }

    public static void Print_Help()
    {
        Console.WriteLine($"Usage : LipsInjector.exe VOCFOLDER{Path.DirectorySeparatorChar}");
        Console.WriteLine("Write the full path to the VOC Folder, in this folder put all .VOC files and the \"LIPS\" folder with all .LIP.");
        Console.WriteLine("It will creates a \"DONE\" folder with all .VOC files with lips injected.");
    }

    public static void WriteLog(string log, string folder)
    {
        string contents = $"Unofficial DUNE CD (PC VERSION) LipsInjector V1.0{Environment.NewLine}Tgames (c) 2019{Environment.NewLine}{Environment.NewLine}-------------- Begin Log File --------------{Environment.NewLine}{Environment.NewLine}{log}{Environment.NewLine}{Environment.NewLine}-------------- End Log File ----------------";
        File.WriteAllText(Path.Combine(folder, "lipsinjector.log"), contents, Encoding.Default);
    }

    private static string FolderFromFileName(string file)
    {
        string str;
        if (file.Contains(Path.DirectorySeparatorChar))
        {
            string[] strArray2 = file.Split(Path.DirectorySeparatorChar);
            str = strArray2[checked(^1)].Substring(0, 2);
        }
        else
        {
            str = file.Substring(0, 2);
        }
        switch (str.GetHashCode())
        {
            case 570421568:
                if (String.Equals(str, "PO", StringComparison.InvariantCultureIgnoreCase))
                    return "PO";
                break;

            case 587199187:
                if (String.Equals(str, "PN", StringComparison.InvariantCultureIgnoreCase))
                    return "PN";
                break;

            case 603976806:
                if (String.Equals(str, "PM", StringComparison.InvariantCultureIgnoreCase))
                    return "PM";
                break;

            case 620754425:
                if (String.Equals(str, "PL", StringComparison.InvariantCultureIgnoreCase))
                    return "PL";
                break;

            case 637532044:
                if (String.Equals(str, "PK", StringComparison.InvariantCultureIgnoreCase))
                    return "PK";
                break;

            case 654309663:
                if (String.Equals(str, "PJ", StringComparison.InvariantCultureIgnoreCase))
                    return "PJ";
                break;

            case 671087282:
                if (String.Equals(str, "PI", StringComparison.InvariantCultureIgnoreCase))
                    return "PI";
                break;

            case 687864901:
                if (String.Equals(str, "PH", StringComparison.InvariantCultureIgnoreCase))
                    return "PH";
                break;

            case 704642520:
                if (String.Equals(str, "PG", StringComparison.InvariantCultureIgnoreCase))
                    return "PG";
                break;

            case 721420139:
                if (String.Equals(str, "PF", StringComparison.InvariantCultureIgnoreCase))
                    return "PF";
                break;

            case 738197758:
                if (String.Equals(str, "PE", StringComparison.InvariantCultureIgnoreCase))
                    return "PE";
                break;

            case 754975377:
                if (String.Equals(str, "PD", StringComparison.InvariantCultureIgnoreCase))
                    return "PD";
                break;

            case 771752996:
                if (String.Equals(str, "PC", StringComparison.InvariantCultureIgnoreCase))
                    return "PC";
                break;

            case 788530615:
                if (String.Equals(str, "PB", StringComparison.InvariantCultureIgnoreCase))
                    return "PB";
                break;

            case 805308234:
                if (String.Equals(str, "PA", StringComparison.InvariantCultureIgnoreCase))
                    return "PA";
                break;

            case 922751567:
                if (String.Equals(str, "PZ", StringComparison.InvariantCultureIgnoreCase))
                    return "PZ";
                break;

            case 1808861065:
                if (String.Equals(str, "GU", StringComparison.InvariantCultureIgnoreCase))
                    return "PF";
                break;
        }
        return "";
    }

    public static void DispatchVOC(string folder)
    {
        if (!Directory.Exists(Path.Combine(folder, "PJ")))
            Directory.CreateDirectory(Path.Combine(folder, "PJ"));
        if (!Directory.Exists(Path.Combine(folder, "PL")))
            Directory.CreateDirectory(Path.Combine(folder, "PL"));
        if (!Directory.Exists(Path.Combine(folder, "PN")))
            Directory.CreateDirectory(Path.Combine(folder, "PN"));
        if (!Directory.Exists(Path.Combine(folder, "PM")))
            Directory.CreateDirectory(Path.Combine(folder, "PM"));
        if (!Directory.Exists(Path.Combine(folder, "PF")))
            Directory.CreateDirectory(Path.Combine(folder, "PF"));
        if (!Directory.Exists(Path.Combine(folder, "PK")))
            Directory.CreateDirectory(Path.Combine(folder, "PK"));
        if (!Directory.Exists(Path.Combine(folder, "PZ")))
            Directory.CreateDirectory(Path.Combine(folder, "PZ"));
        if (!Directory.Exists(Path.Combine(folder, "PC")))
            Directory.CreateDirectory(Path.Combine(folder, "PC"));
        if (!Directory.Exists(Path.Combine(folder, "PG")))
            Directory.CreateDirectory(Path.Combine(folder, "PG"));
        if (!Directory.Exists(Path.Combine(folder, "PD")))
            Directory.CreateDirectory(Path.Combine(folder, "PD"));
        if (!Directory.Exists(Path.Combine(folder, "PB")))
            Directory.CreateDirectory(Path.Combine(folder, "PB"));
        if (!Directory.Exists(Path.Combine(folder, "PI")))
            Directory.CreateDirectory(Path.Combine(folder, "PI"));
        if (!Directory.Exists(Path.Combine(folder, "PO")))
            Directory.CreateDirectory(Path.Combine(folder, "PO"));
        if (!Directory.Exists(Path.Combine(folder, "PE")))
            Directory.CreateDirectory(Path.Combine(folder, "PE"));
        if (!Directory.Exists(Path.Combine(folder, "PH")))
            Directory.CreateDirectory(Path.Combine(folder, "PH"));
        if (!Directory.Exists(Path.Combine(folder, "PA")))
            Directory.CreateDirectory(Path.Combine(folder, "PA"));
        try
        {
            string[] array = Directory.GetFiles(folder);
            for (int i = 0; i < array.Length; i++)
            {
                string file = array[i];
                if (file.ToUpperInvariant().Contains(".VOC"))
                {
                    string[] strArray2 = file.Split(Path.DirectorySeparatorChar);
                    string[] strArray3 = strArray2[checked(^1)].Split('.');
                    string path = $"{folder}{FolderFromFileName(strArray3[0])}{Path.DirectorySeparatorChar}{strArray3[0]}.VOC";
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    File.Move(file, path);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e.GetBaseException().GetType()}{Environment.NewLine}{e.GetBaseException().Message}{Environment.NewLine}{e.GetBaseException().StackTrace}");
        }
    }

    public static void InjectLips(string vocsDir)
    {
        var log = "";
        var folder = $"{Path.Combine(vocsDir, "LIPS")}{Path.DirectorySeparatorChar}";
        if (!Directory.Exists(Path.Combine(vocsDir, "DONE")))
        {
            Directory.CreateDirectory(Path.Combine(vocsDir, "DONE"));
        }

        try
        {
            string[] array = Directory.GetFiles(vocsDir);
            for (int i = 0; i < array.Length; i++)
            {
                string file = array[i];
                if (file.Contains(".VOC"))
                {
                    string[] vocFileName = file.Split(Path.DirectorySeparatorChar);
                    string str = Path.GetFileNameWithoutExtension(vocFileName[0]);
                    int num2 = 0;
                    if (File.Exists(Path.Combine(folder, $"{str}.LIP")))
                    {
                        FileStream fileStream1 = new(file, FileMode.Open);
                        fileStream1.Seek(0L, SeekOrigin.Begin);
                        FileStream fileStream2 = new(Path.Combine(vocsDir, Path.Combine("DONE", $"{str}.VOC")), FileMode.Create);
                        fileStream2.Seek(0L, SeekOrigin.Begin);
                        fileStream1.Seek(26L, SeekOrigin.Begin);
                        if (fileStream1.ReadByte() != 5)
                        {
                            fileStream1.Seek(0L, SeekOrigin.Begin);
                            while (num2 < fileStream1.Length & num2 < 26)
                            {
                                byte num3 = checked((byte)fileStream1.ReadByte());
                                fileStream2.WriteByte(num3);
                                checked { ++num2; }
                            }
                            if (num2 >= fileStream1.Length)
                            {
                                fileStream2.Close();
                                Console.WriteLine($"FATAL ERROR : {str}.VOC read seems to be corrupted");
                                File.Delete(Path.Combine(vocsDir, Path.Combine("DONE", $"{str}.VOC")));
                                log = $"{log}FATAL Error : {str}.VOC read seems to be corrupted{Environment.NewLine}";
                            }
                            else
                            {
                                int num3 = 0;
                                FileStream fileStream3 = new(Path.Combine(folder, $"{str}.LIP"), FileMode.Open);
                                fileStream3.Seek(0L, SeekOrigin.Begin);
                                while (num3 < fileStream3.Length)
                                {
                                    byte num4 = checked((byte)fileStream3.ReadByte());
                                    fileStream2.WriteByte(num4);
                                    checked { ++num3; }
                                }
                                fileStream3.Close();
                                if (num3 < 4)
                                {
                                    fileStream2.Close();
                                    Console.WriteLine($"FATAL ERROR : {str}.LIP read seems to be corrupted");
                                    File.Delete(Path.Combine(vocsDir, Path.Combine("DONE", $"{str}.VOC")));
                                    log = $"{log}FATAL Error : {str}.LIP read seems to be corrupted{Environment.NewLine}";
                                }
                                else
                                {
                                    int position = checked((int)fileStream1.Position);
                                    while (position < fileStream1.Length)
                                    {
                                        byte num4 = checked((byte)fileStream1.ReadByte());
                                        fileStream2.WriteByte(num4);
                                        checked { ++position; }
                                    }
                                    if (position < 20)
                                    {
                                        fileStream2.Close();
                                        Console.WriteLine($"FATAL ERROR : {str}.VOC read seems to be corrupted");
                                        File.Delete(Path.Combine(vocsDir, Path.Combine("DONE", $"{str}.VOC")));
                                        log = $"{log}FATAL Error : {str}.VOC read seems to be corrupted{Environment.NewLine}";
                                    }
                                    else
                                    {
                                        fileStream2.Close();
                                        Console.WriteLine("The lip data has been successfully injected into " + str + ".VOC");
                                        log = $"{log}The lip data has been successfully injected into {str}.VOC{Environment.NewLine}";
                                    }
                                }
                            }
                        }
                        else
                        {
                            fileStream2.Close();
                            Console.WriteLine($"FATAL ERROR : {str}.VOC has LIP data already !");
                            File.Delete(Path.Combine(vocsDir, Path.Combine("DONE", $"{str}.VOC")));
                            log = $"{log}FATAL Error : {str}.VOC has already LIP data !{Environment.NewLine}";
                        }
                        fileStream1.Close();
                    }
                    else
                    {
                        Console.WriteLine($"No Lips file available for {str}.VOC");
                        log = $"{log}No Lips file available for {str}.VOC{Environment.NewLine}";
                    }
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