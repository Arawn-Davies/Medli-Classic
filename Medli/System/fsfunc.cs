using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Sys = Cosmos.System;

namespace Medli
{
    class Fsfunc
    {
        /// <summary>
        /// Declares the variable 'fs' to be the Virtual Filesystem
        /// Required assignment due to this class containing the file system functions
        /// </summary>
        public static Sys.FileSystem.CosmosVFS fs;

        /// <summary>
        /// dir() lists the contents of the current working directory
        /// Lists directories first with the tag <Directory>,
        /// then lists files by extension
        /// </summary>
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

        /// <summary>
        /// Creates a directory with the parameter being 'dirname'
        /// 'dirname' is passed as an argument with it being the desired
        /// directory name to be created in the current working directory
        /// </summary>
        /// <param name="dirname"></param>
        public static void mkdir(string dirname)
        {
            try
            {
                if (!Directory.Exists(Kernel.current_dir + @"\" + dirname))
                {
                    Directory.CreateDirectory(Kernel.current_dir + @"\" + dirname);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("mkdir: " + ex.Message);
            }
        }

        /// <summary>
        /// Another similar mkdir() function, will simply but this is for the Installer, where current_dir isn't assigned
        /// </summary>
        /// <param name="dirname"></param>
        public static void mksysdir(string dirname)
        {
            try
            {
                if (!Directory.Exists(dirname))
                {
                    Directory.CreateDirectory(dirname);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("mkdir: " + ex.Message);
            }
        }

        /// <summary>
        /// Changes the current working directory to 'input'
        /// Passing '..' as a directory name will change the
        /// current working directory to it's parent, 
        /// unless already at the root
        /// </summary>
        /// <param name="input"></param>
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
                Console.WriteLine("Folder does not exist " + Kernel.current_dir + @"\" + path);
            }
        }

        /// <summary>
        /// Deletes a directory, with the chosen directory name passed as 'dirname'
        /// </summary>
        /// <param name="dirname"></param>
        public static void deldir(string dirname)
        {
            Directory.Delete(Kernel.current_dir + @"\" + dirname);
        }

        /// <summary>
        /// Deletes a file, with the chosen filename passed as 'filename'
        /// </summary>
        /// <param name="filename"></param>
        public static void delfile(string filename)
        {
            File.Delete(Kernel.current_dir + @"\" + filename);
        }
    }
}