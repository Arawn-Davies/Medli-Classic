using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Medli
{
    class UserManagement
    {
        public static void NewUser(string usrname, string pass, UserType type)
        {
            Directory.CreateDirectory(KernelVariables.homedir + usrname + MEnvironment.dir_ext);
            Console.WriteLine("Created new user directory: " + KernelVariables.homedir + usrname + MEnvironment.dir_ext);
            MEnvironment.WriteUserPass();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("     Done!");
            Console.ForegroundColor = ConsoleColor.White;
            KernelVariables.username = usrname;
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
            Account.Accounts.Add(new Account(usrname, pass, type = UserType.Normal));
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
            if (usrlogon == "root")
            {
                Console.Write("Password >");
                Console.ForegroundColor = ConsoleColor.Black;
                string pass = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                MEnvironment.rootpass_md5 = File.ReadAllLines(MEnvironment.rpf)[0];
                if (AIC_Framework.Crypto.MD5.hash(pass) == MEnvironment.rootpass_md5)
                {
                    KernelVariables.username = "root";
                    MEnvironment.PressAnyKey();
                }
                else
                {
                    Console.WriteLine("Incorrect root password. ");
                    MEnvironment.PressAnyKey();
                    UserLogin();
                }
            }
            else if (Directory.Exists(KernelVariables.homedir + usrlogon))
            {
                Console.Write("Password >");
                Console.ForegroundColor = ConsoleColor.Black;
                string pass = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                MEnvironment.usrpass_md5 = File.ReadAllLines(MEnvironment.upf)[0];
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
            else
            {
                Console.WriteLine("User does not exist!");
                Console.WriteLine("Press any key to retry...");
                Console.ReadKey(true);
                UserLogin();
            }
        }
        class Account
        {
            public static List<Account> Accounts;
            public string Name { get; set; }
            public string Password { get; set; }
            public string userhomedir { get; set; }

            public string usrpass_md5 { get; set; }
            public string upf = "pass.sys";
            public UserType Type { get; set; }

            /// <summary>
            /// Create an account.
            /// </summary>
            /// <param name="nm">The user name.</param>
            /// <param name="pass">The user password.</param>
            public Account(string nm, string pass, UserType type = UserType.Normal)
            {
                Name = nm;
                Password = pass;
                Type = type;
                userhomedir = KernelVariables.homedir + Name + MEnvironment.dir_ext;
                usrpass_md5 = AIC_Framework.Crypto.MD5.hash(Password);
                Directory.CreateDirectory(userhomedir);
                File.WriteAllText(userhomedir + upf, usrpass_md5);
            }
        }
    }
    public enum UserType
    {
        Guest = 0,
        Normal = 1,
        Root = 2,
    }
}