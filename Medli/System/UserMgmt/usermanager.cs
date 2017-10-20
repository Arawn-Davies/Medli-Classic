using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Medli.SysInternal;
using Medli.System;

namespace Medli.UsrMgmt
{
    class UserManagement
    {   
        public static List<string> users = new List<string>();
        public static void LoadUsers()
        {
            var usersinfile = File.ReadAllLines(OSVars.usrinfo);
            foreach (string user in usersinfile)
            {
                users.Add(user);
            }
        }
        public static void NewUser(string usrname)
        {
            Directory.CreateDirectory(KernelVariables.homedir + usrname);
            Console.WriteLine("Created new user directory: " + KernelVariables.homedir + usrname);
            Console.WriteLine();
            Console.Write("Adding new user to user list...");
            users.Add(usrname);
            File.AppendAllText(KernelVariables.sysdir + @"\" + "usrinfo.sys", usrname);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("     Done!");
            Console.ForegroundColor = ConsoleColor.White;
            OSVars.username = usrname;
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
        public static void PermCheck()
        {
            if (Kernel.current_dir != OSVars.username)
            {
                Console.WriteLine("You do not have permission to access these files:");
                //return false;
                //System Error handler is not yet implemented!
                //System.ErrorHandler.Handle(System.ErrorHandler.errors.Permission, "", System.ErrorHandler.errordsc[2]);
            }
        }
        public static void UserLogin()
        {
            Console.Clear();
            Console.WriteLine("User Logon:");
            Console.CursorTop = 5;
            Console.WriteLine("You can either log in as an existing user or create a new one.\n");
            Console.Write(">");
            string usrlogon = Console.ReadLine();
            if (!Directory.Exists(KernelVariables.homedir + usrlogon))
            {
                NewUser(usrlogon);
            }
            else if (Directory.Exists(KernelVariables.homedir + usrlogon))
            {
                //users.Exists(x => string.Equals(x, usrlogon, StringComparison.OrdinalIgnoreCase)) ||
                if (users.Contains(usrlogon))
                {
                    OSVars.username = usrlogon;
                }
            }
        }

    }
}
