﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Sys = Cosmos.System;

namespace Medli.SysInternal
{
    class Fsfunc
    {
        /// <summary>
        /// Retrieves a list of volumes currently connected to the system and prints out each one
        /// </summary>
        public static void getVols()
        {
            var vols = Kernel.fs.GetVolumes();
            int volno = 0;
            foreach (var vol in vols)
            {
                volno += 1;
                Console.WriteLine("Volume " + volno);
                Console.WriteLine("Volume name: " + vol.mName + "Size: " + vol.mSize + "\n" + "Path: " + vol.mFullPath);
                Console.WriteLine(" ");
            }
        }


        /// <summary>
        /// dir() lists the contents of the current working directory
        /// Lists directories first with the tag <Directory>,
        /// then lists files by extension
        /// </summary>
        public static void dir()
        {
            try
            {
                foreach (var dir in Directory.GetDirectories(MEnvironment.current_dir))
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
                foreach (var dir in Directory.GetFiles(MEnvironment.current_dir))
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
                if (!Directory.Exists(MEnvironment.current_dir + @"\" + dirname))
                {
                    Directory.CreateDirectory(MEnvironment.current_dir + @"\" + dirname);
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
            string path = input; //cd <- 2 chars
            if (Directory.Exists(MEnvironment.current_dir + path))
            {
                MEnvironment.current_dir = MEnvironment.current_dir + path;
            }
            else if (Directory.Exists(path))
            {
                MEnvironment.current_dir = path + MEnvironment.dir_ext;
            }
            else
            {
                Console.WriteLine("Folder does not exist " + MEnvironment.current_dir + @"\" + path);
            }
        }

        /// <summary>
        /// Deletes a directory, with the chosen directory name passed as 'dirname'
        /// </summary>
        /// <param name="dirname"></param>
        public static void deldir(string dirname)
        {
            if (dirname == "sys" || dirname == "bin" || dirname == "usr" || dirname == "home" || dirname == "root")
            {
                Console.WriteLine("This directory is a protected directory file.\nNon-root access is not permitted!");
            }
            Directory.Delete(MEnvironment.current_dir + @"\" + dirname);
        }

        /// <summary>
        /// Deletes a file, with the chosen filename passed as 'filename'
        /// </summary>
        /// <param name="filename"></param>
        public static void delfile(string filename)
        {
            if (filename.EndsWith(".sys") && KernelVariables.username != "root")
            {
                Console.WriteLine("This file is a protected system file.\nNon-root access is not permitted!");
            }
            else if (filename.EndsWith(".sys") && KernelVariables.username == "root")
            {
                Console.WriteLine("Deleting .sys files and other system files can have an unexpected and potentially catastrophic result.");
                Console.WriteLine("Are you sure you wish to continue? y/n");
                //Might work without
                //var key = Console.ReadKey();
                var key_pressed = Console.ReadKey().Key;
                if (key_pressed == ConsoleKey.Y)
                {
                    File.Delete(MEnvironment.current_dir + @"\" + filename);
                }
                else
                {
                    Console.WriteLine("Aborted.");
                }
            }
            else
            {
                File.Delete(MEnvironment.current_dir + @"\" + filename);
            }
            
        }
    }
}