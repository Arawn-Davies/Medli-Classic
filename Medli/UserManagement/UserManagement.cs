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
        public static List<string> Users = new List<string>();
        public static void NewUser(string usrname, string pass)
        {
            KernelVariables.username = usrname;
            MEnvironment.usrpass = pass;
            Directory.CreateDirectory(KernelVariables.homedir + usrname + MEnvironment.dir_ext);
            Console.WriteLine("Created new user directory: " + KernelVariables.homedir + usrname + MEnvironment.dir_ext);
            MEnvironment.WriteUserPass();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("     Done!");
            Console.ForegroundColor = ConsoleColor.White;
            KernelVariables.username = usrname;
            Users.Add(usrname);
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
        
        public static void PermCheck()
        {
            if (KernelVariables.username != MEnvironment.current_usr_dir)
            {
                Console.WriteLine("You are not logged in as this user! Access Denied.");
            }
            else
            {

            }
        }
        public static void resetConsoleColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void changepass()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("Change password:");
            Console.CursorTop = 5;
            resetConsoleColor();
            Console.WriteLine("Enter the new user password");
            string usrpass = Console.ReadLine();
            File.WriteAllText(KernelVariables.homedir + KernelVariables.username + @"\pass.sys", AIC_Framework.Crypto.MD5.hash(usrpass));
        }
        public static void UserLogin()
        {
            
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("User Login:");
            resetConsoleColor();
            Console.CursorTop = 5;
            Console.WriteLine("You can either log in as an existing user or create a new one.\n");
            Console.Write("Username >");
            string usrlogon = Console.ReadLine();
            if (!Directory.Exists(KernelVariables.homedir + usrlogon) || !Directory.Exists(KernelVariables.rootdir))
            {
                Console.WriteLine("User does not exist!");
                Console.WriteLine("Press any key to retry...");
                Console.ReadKey(true);
                UserLogin();
            }
            else if (Directory.Exists(KernelVariables.homedir + usrlogon) || Directory.Exists(KernelVariables.rootdir))
            {
                Console.Write("Password >");
                string pass = Console.ReadLine();
                if (Kernel.isInitLogin == true)
                    MEnvironment.rootpass = File.ReadAllLines(MEnvironment.rpf)[0];
                if (usrlogon == "root")
                {
                    if (AIC_Framework.Crypto.MD5.hash(pass) == MEnvironment.rootpass_md5)
                    {
                        KernelVariables.username = usrlogon;
                        MEnvironment.PressAnyKey();
                    }
                    else
                    {
                        Console.WriteLine("Incorrect root password. ");
                        MEnvironment.PressAnyKey();
                        UserLogin();
                    }
                }
                else
                {
                    if (Kernel.isInitLogin == true)
                        MEnvironment.usrpass = File.ReadAllLines(MEnvironment.upf)[0];
                    if (AIC_Framework.Crypto.MD5.hash(pass) == MEnvironment.usrpass_md5)
                    {
                        KernelVariables.username = usrlogon;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect password.");
                        MEnvironment.PressAnyKey();
                        UserLogin();
                    }
                }
            }
        }
    }        
}
