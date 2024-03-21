using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjetoManisoba
{
    class Program
    {
        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        public static void Main(string[] args)
        {
            try
            {
                //MprWrapper.WNetUseConnection(@"\\192.168.0.240", @"unibra.ub\armando", "trendkillbrutalkillpsychomurder");
                MprWrapper.WNetUseConnection(@"\\192.168.0.229", @"unibra.ub\armando", "trendkillbrutalkillpsychomurder");
                //Console.WriteLine("Conexão estabelecida com sucesso!");

                //string path = @"\\192.168.0.229\WKRadar\#2 RM SMART PRD";
                string path = @"\\192.168.0.229\WKRadar\#2 RM SMART PRD\UB SMART PRD";
                string[] files = System.IO.Directory.GetFiles(path);
                string totvsDirectory = @"C:\TOTVStesste";

                //Console.WriteLine("Arquivos na pasta específica:");

                int count = 0;

                foreach (string file in files)
                {
                    if(!file.Contains("UB SMART PRD"))
                    {
                        count++;

                        if (count == files.Length)
                            throw new Exception("File not found");

                        continue;
                    }
                }

                if (!Directory.Exists(totvsDirectory))
                {
                    Directory.CreateDirectory(totvsDirectory);
                }

                //string fileToCopy = @"\\192.168.0.229\WKRadar\#2 RM SMART PRD\UB SMART PRD 12.1.2402.116 (VERSAO ATT).zip";
                string fileToCopy = @"\\192.168.0.229\WKRadar\#2 RM SMART PRD\UB SMART PRD";
                string destinationDirectory = @$"{totvsDirectory}";

                var di = new DirectoryInfo(fileToCopy);

                if (di.Exists)
                {
                    if (di.Attributes.HasFlag(FileAttributes.ReadOnly))
                    {
                        Console.Write("READONLY");
                        //IsReadOnly...
                    }
                }

                Copy(fileToCopy, destinationDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao estabelecer conexão: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}