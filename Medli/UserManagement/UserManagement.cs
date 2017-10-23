using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Medli.SysInternal;
using Medli;

namespace Medli
{
    class UserManagement
    {
        public static void NewUser(string usrname)
        {
            Directory.CreateDirectory(KernelVariables.homedir + @"\" + usrname);
            Console.WriteLine("Created new user directory: " + KernelVariables.homedir + @"\" + usrname);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("     Done!");
            Console.ForegroundColor = ConsoleColor.White;
            KernelVariables.username = usrname;
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
        
        public static void PermCheck()
        {
            if (KernelVariables.username != KernelVariables.homedir + KernelVariables.username)
            {
                Console.WriteLine("You are not logged in as this user! Access Denied.");
            }
            else
            {

            }
        }
        
        public static void UserLogon()
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
                KernelVariables.username = usrlogon;
            }
        }        
    }
}
