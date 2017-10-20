using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Medli.SysInternal;
using Medli;

namespace Medli.UserManagement
{
    class UsrMgmt
    {
        public static void NewUser(string usrname)
        {
            Directory.CreateDirectory(KernelVariables.homedir + @"\" + usrname);
            Console.WriteLine("Created new user directory: " + KernelVariables.homedir + @"\" + usrname);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("     Done!");
            Console.ForegroundColor = ConsoleColor.White;
            Installer.username = usrname;
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
        /*
        public static void PermCheck()
        {
            if (Terminal.current_user != user_directory)
            {
                SystemRing.ErrorHandler.Warning(1, "oud");
            }
            else
            {

            }
        }
        public static void CheckUser()
        {
            Console.Clear();
            Console.WriteLine("User Logon:");
            Console.CursorTop = 5;
            Console.WriteLine("You can either log in as an existing user or create a new one.\n");
            Console.Write(">");
            string usrlogon = Console.ReadLine();
            if (!Directory.Exists(Terminal.usrs_dir + usrlogon))
            {
                NewUser(usrlogon);
            }
            else if (Directory.Exists(Terminal.usrs_dir + usrlogon))
            {
                Terminal.current_user = usrlogon;
            }
        }
        */        
    }
}
