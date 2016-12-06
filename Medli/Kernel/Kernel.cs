using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.FileSystem.VFS;
using Sys = Cosmos.System;
using System.IO;
using Medli.System;
using Medli.Applications;

namespace Medli
{
    public class OSVars
    {
        public static double ver_no = 0.1;
        public static string wlcm1 = "Medli  - Version " + OSVars.ver_no;
        public static string wlcm2 = "-=A free and open-source operating system=-";
        public static string wlcm3 = "Maintained by CaveSponge under the MIT License";
        public static string wlcm4 = "This copy of Medli is registered to: " + Kernel.regname;
        public static void ver()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(wlcm1);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(wlcm2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(wlcm3);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(wlcm4);
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
    public class Kernel : Sys.Kernel
    {
        public static string regname = "";
        public static string machinename = "";
        public static string current_dir = "0:\\";
        public static string root_dir = "0:\\";
        Sys.FileSystem.CosmosVFS fs;
        protected override void BeforeRun()
        {
            fs = new Sys.FileSystem.CosmosVFS();
            VFSManager.RegisterVFS(fs);
            fs.Initialize();
            Console.Clear();
            OSVars.ver();

            #region MNinit
            //This is just to identify the users machine, much later in Medli will this have any usage however
            //i.e. not until networking is set up, FS Permissions are working etc...
            
            //----------->>> Checks filesystem for a document called 'machinename' that contains the machinename,
            //----------->>> stored as a simple text document without the txt extension.
            //----------->>> 
            //----------->>> Because machine names have no use right now, this code is only for testing as it is not in use.
            
            //If it wasn't able to find a machinename file, then it will try and create one.
            //This is why initializing the filesystem is vital before executing this code.
            if (!File.Exists(Kernel.current_dir + "pcinfo.sys"))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Medli Installer:");
                Console.WriteLine(" "); 
                Console.WriteLine("Press any key and let's get started!");
                Console.ReadKey(true);
                Console.WriteLine("");
                Console.WriteLine("Please enter a machine name:");
                machinename = Console.ReadLine();
                File.Create(Kernel.current_dir + "pcinfo.sys");
                File.WriteAllText(Kernel.current_dir + "pcinfo.sys", machinename);
                Console.WriteLine("Excellent! Please enter who this copy of Medli is registered to:");
                regname = Console.ReadLine();
                File.Create(Kernel.current_dir + "reginfo.sys");
                File.WriteAllText(Kernel.current_dir + "reginfo.sys", regname);
                Console.WriteLine("");
                Console.WriteLine("Awesome - you're all set!");
                Console.WriteLine("Press any key to start Medli!");
                Console.ReadKey(true);
                Console.Clear();
            }
            else if (File.Exists(Kernel.current_dir + "pcinfo.sys") || File.Exists(Kernel.current_dir + "regname.sys"))
            {
                try
                {
                    string[] lines = File.ReadAllLines(Kernel.current_dir + "regname.sys");
                    foreach (string line in lines)
                    {
                        regname = line;
                    }
                    string[] pcnames = File.ReadAllLines(Kernel.current_dir + "pcinfo.sys");
                    foreach (string pcname in pcnames)
                    {
                        machinename = pcname;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            
            #endregion

            Console.Clear();
            OSVars.ver();
        }

        protected override void Run()
        {
            ngshell.prompt();
        }
        
    }
}
