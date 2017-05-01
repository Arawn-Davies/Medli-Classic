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
        public static string pcinfo = Kernel.root_dir + "pcinfo.sys";
        public static string reginfo = Kernel.root_dir + "reginfo.sys";
        public static string regname;
        public static string pcname;
        public static double version;
        public static double ver_no = 0.3;
        public static string wlcm1 = "Medli-core - Version " + OSVars.ver_no;
        public static string wlcm2 = "Maintained by Arawn Davies under the MIT License";
        public static string wlcm3 = "This copy of Medli-core is registered to: ";

        public static void ver()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(wlcm1);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(wlcm2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(wlcm3);
            //Insert total asshattery here >>>
            if (File.Exists(OSVars.reginfo))
            {
                string[] lines = File.ReadAllLines(OSVars.reginfo);
                foreach (string line in lines)
                {
                    //     \/ Gotta be careful here hehehe \/
                    Console.Write(line + "\n");
                }
            }
            else
            {
                Console.Write("-=UNREGISTERED=-\n");
            }
            Console.ForegroundColor = ConsoleColor.White;


        }
    }
    public class Kernel : Sys.Kernel
    {
        
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
            //If it wasn't able to find a machinename file, then it will try and create one.
            //This is why initializing the filesystem is vital before executing this code.
            if (File.Exists(OSVars.reginfo))
            {
                string[] lines = File.ReadAllLines(OSVars.reginfo);
                foreach (string line in lines)
                {
                    OSVars.regname = line;
                }
                string[] pcnames = File.ReadAllLines(OSVars.pcinfo);
                foreach (string pcname in pcnames)
                {
                    OSVars.pcname = pcname;
                }
            }
            else if (File.Exists(Kernel.current_dir + "pcinfo.sys") || File.Exists(Kernel.current_dir + "regname.sys"))
            {
                try
                {
                    string[] lines = File.ReadAllLines(Kernel.current_dir + "regname.sys");
                    foreach (string line in lines)
                    {
                        OSVars.regname = line;
                    }
                    string[] pcnames = File.ReadAllLines(Kernel.current_dir + "pcinfo.sys");
                    foreach (string pcname in pcnames)
                    {
                        OSVars.pcname = pcname;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {

                Console.WriteLine("Medli was unable to find any registration info or any info regarding your PC.");
                Console.WriteLine("The Medli installer will now run.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Medli Installer:");
                Console.WriteLine(" ");
                Console.WriteLine("Press any key and let's get started!");
                Console.ReadKey(true);
                Console.WriteLine("");
                Console.WriteLine("Please enter a machine name:");

                OSVars.pcname = Console.ReadLine();
                File.Create(Kernel.current_dir + "pcinfo.sys");
                File.WriteAllText(Kernel.current_dir + "pcinfo.sys", OSVars.pcname);
                Console.WriteLine("Excellent! Please enter who this copy of Medli is registered to:");
                OSVars.regname = Console.ReadLine();
                File.Create(Kernel.current_dir + "reginfo.sys");
                File.WriteAllText(Kernel.current_dir + "reginfo.sys", OSVars.regname);

                OSVars.pcname = Console.ReadLine();
                File.Create(OSVars.pcinfo);
                File.WriteAllText(OSVars.pcinfo, OSVars.pcname);
                Console.WriteLine("Excellent! Please enter who this copy of Medli is registered to:");
                OSVars.regname = Console.ReadLine();
                File.Create(OSVars.reginfo);
                File.WriteAllText(OSVars.reginfo, OSVars.regname);

                Console.WriteLine("");
                Console.WriteLine("Awesome - you're all set!");
                Console.WriteLine("Press any key to start Medli!");
                Console.ReadKey(true);
                Console.Clear();
            }
            #endregion

            Console.Clear();
            OSVars.ver();
        }
        protected override void Run()
        {
            mshell.prompt();
        }
    }
}
