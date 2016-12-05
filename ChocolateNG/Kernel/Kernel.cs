using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.FileSystem.VFS;
using Sys = Cosmos.System;
using System.IO;
using MedliSystem;
using Medli.Applications;

namespace Medli
{
    /// <summary>
    /// version 2.2.7/golf
    /// Changelog:
    /// Fixed commands not being implemented fully, still W.I.P (can't believe there was no 'echo' command c':)
    /// Get OS version function added, saves having to retype the same thing over and over again
    /// 
    /// 
    /// How about 'Cocoa' as a name for the kernel? - 
    /// Cocoashell exists but is not developed and NGShell has surpassed it by a 
    /// fair bit so I can always remove the repo
    /// 
    /// I wonder what to do with Chocolate also...
    /// </summary>


    //    
    //Update on versioning!
    //
    //Because I'm ridiculous with versioning, I thought of a new versioning system, so we can get rid of the mess that was the old one. 
    //Half of it is a typical Major.Minor numbering scheme but the last build number goes upto 25, so one can use the 
    //alpha/beta/charlie names. A very limited graphical representation of this is the following:
    //
    // Major  . Minor .   Build
    //   ∞    .   ∞   .  Up to 25
    //
    public class OSVars
    {
        public static double ver_no = 2.2;
        public static int build_no = 7;
        public static string build_name = "Golf";
        public static string wlcm1 = "Medli " + OSVars.ver_no;
        public static string wlcm2 = "(C) All Rights Reserved - CaveSponge";
        public static string wlcm3 = "The next gen version of the original Chocolate";
        public static void ver()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(wlcm1);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(wlcm2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(wlcm3);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public class Kernel : Sys.Kernel
    {
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
            if (!File.Exists(Kernel.current_dir + "pcname.sys"))
            {
                Console.WriteLine("Please enter in a new machine name which will be used for this session.");
                Console.WriteLine("Future revisions of Medli will store the machinename for static use.");
                machinename = Console.ReadLine();
                File.Create(Kernel.current_dir + "pcname.sys");
                File.WriteAllText(Kernel.current_dir + "pcname.sys", machinename);
            }
            else if (File.Exists(Kernel.current_dir + "pcname.sys"))
            {
                try
                {
                    string[] lines = File.ReadAllLines(Kernel.current_dir + "pcname.sys");
                    foreach (string line in lines)
                    {
                        machinename = line;
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
