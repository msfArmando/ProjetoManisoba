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
        public static void Main(string[] args)
        {
            try
            {
                MprWrapper.WNetUseConnection(@"\\192.168.0.240", @"unibra.ub\armando", "trendkillbrutalkillpsychomurder");
                //Console.WriteLine("Conexão estabelecida com sucesso!");

                string path = @"\\192.168.0.240\ti sistema\UB SMART PRD";
                string[] files = System.IO.Directory.GetDirectories(path);
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

                string fileToCopy = "\\\\192.168.0.229\\WKRadar\\#2 RM SMART PRD\\UB SMART PRD";
                string destinationDirectory = $"C:\\{totvsDirectory}";

                var di = new DirectoryInfo(fileToCopy);

                if (di.Exists)
                {
                    if (di.Attributes.HasFlag(FileAttributes.ReadOnly))
                    {
                        Console.Write("READONLY");
                        //IsReadOnly...
                    }
                }

                File.Copy(fileToCopy, destinationDirectory + Path.GetFileName(fileToCopy));

                // + Path.GetFileName(fileToCopy)
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao estabelecer conexão: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}