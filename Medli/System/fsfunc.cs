using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Sys = Cosmos.System;

namespace Medli
{
    class FSfunc
    {
        public static Sys.FileSystem.CosmosVFS fs;

        public static void dir()
        {
            try
            {
                foreach (var dir in Directory.GetDirectories(Kernel.current_dir))
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("<Directory>\t" + dir);
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                foreach (var dir in Directory.GetFiles(Kernel.current_dir))
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        string[] sp = dir.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                        Console.WriteLine(sp[sp.Length - 1] + "\t" + dir);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void mkdir(string dirname)
        {
            try
            {
                if (!Directory.Exists(Kernel.current_dir + "/" + dirname))
                {
                    Directory.CreateDirectory(Kernel.current_dir + "/" + dirname);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("mkdir: " + ex.Message);
            }
        }
        public static void cd(string input)
        {
            string path = input.Remove(0, 3); //cd <- 2 chars
            if (Directory.Exists(Kernel.current_dir + path))
            {
                Kernel.current_dir = Kernel.current_dir + path;
            }
            else if (Directory.Exists(path))
            {
                Kernel.current_dir = path;
            }
            else
            {
                Console.WriteLine("Folder does not exist " + Kernel.current_dir + "/" + path);
            }
        }
        public static void deldir(string dirname)
        {
            Directory.Delete(Kernel.current_dir + "/" + dirname);
        }
        public static void delfile(string filename)
        {
            File.Delete(Kernel.current_dir + "/" + filename);
        }
    }
}