using System;
using System.IO;
using Medli.System;
using Medli.SysInternal;
using Medli.GUI;
using Medli.Command_db.Commands;

namespace Medli.Applications
{
    class Shell
    {
        public static void invalidCommand(string args, int errorlvl)
        {
            if (errorlvl == 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(args);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" is not a valid command, see 'help' for a list of commands");
            }
            else if (errorlvl == 2)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("The file ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(args);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("could not be found!");

            }
            else if (errorlvl == 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (errorlvl == 4)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            
            
        }
        public static void prompt()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(OSVars.username+ "@" +OSVars.pcname + ":");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(Kernel.current_dir);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("$");
            cmd(Console.ReadLine());
        }
        public static void cmd(string input)
        {
            var command = input.ToLower();
            if (command == "cd ..")
            {
                try
                {
                    if (Kernel.current_dir == Kernel.root_dir)
                    {
                        Console.WriteLine("Cannot go up any more levels!");
                    }
                    else
                    {
                        var pos = Kernel.current_dir.LastIndexOf('\\');
                        if (pos >= 0)
                        {
                            Kernel.current_dir = Kernel.current_dir.Substring(0, pos) + @"\";
                        }
                        /*                        
                        var dir = FSfunc.fs.GetDirectory(Kernel.current_dir);
                        string p = dir.mParent.mName;
                        if (!string.IsNullOrEmpty(p))
                        {
                            Kernel.current_dir = p;
                        }
                        */
                    }
                }
                catch (Exception ex)
                {
                    ErrorHandler.Init(0, ex.Message, false, "");
                }
            }
            else if (command.StartsWith("cd "))
            {
                Fsfunc.cd(command);
            }
            
            else if (command == "reinstall")
            {
                try
                {
                    //Directory.Delete(Kernel.root_dir + @"\" + OSVars.username);
                    Fsfunc.delfile(OSVars.usrinfo);
                    Fsfunc.delfile(OSVars.pcinfo);
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Reinstallation failed!");
                    Console.WriteLine("Reason: " + ex.Message);
                }
                Console.WriteLine("Press any key to reboot...");
                Console.ReadKey(true);
                Sysfunc.reboot();

            }
            else if (command == "time")
            {
                MedliTime.printTime();
            }
            else if (command == "date")
            {
                MedliTime.printDate();
            }
            /*
            else if (command == "day")
            {
                DateTime.Now.Day.ToString();
            }
            */
            else if (command.StartsWith("run "))
            {
                if (!File.Exists(Kernel.current_dir + command.Remove(0, 4)))
                {
                    invalidCommand(command.Remove(0, 4), 2);
                }
                else
                {
                    mdscript.Execute(Kernel.current_dir + command.Remove(0, 4));
                }
            }
            else if (command.StartsWith("rmf "))
            {
                try
                {
                    Fsfunc.delfile(command.Remove(0, 4));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    invalidCommand(command.Remove(0, 4), 2);
                }
            }
            else if (command.StartsWith("rmd "))
            {
                try
                {
                    Fsfunc.deldir(command.Remove(0, 4));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    invalidCommand(command.Remove(0, 4), 2);
                }
            }
            else if (command == "getram")
            {
                Sysfunc.ram();
            }
            else if (command.StartsWith("dir"))
            {
                Fsfunc.dir();
            }
            else if (command.StartsWith("miv"))
            {
                MIV.StartMIV();
            }
            else if (command == "reboot")
            {
                CoreFunc.Reboot();
            }
            else if (command == "shutdown")
            {
                CoreFunc.Shutdown();
            }
            else if (command == "panic")
            {
                ErrorHandler.Init(1, "Medli received the 'panic' command, Nothing's gonna happen.", false, "");
            }
            else if (command.StartsWith("panic"))
            {
                if (command.Remove(0, 6) == "critical")
                {
                    ErrorHandler.Init(0, "Medli received the 'panic critical' command , Nothings gonna happen", true, "User-invoked panic");
                }
                else if (command.Remove(0, 6) == "userlvl")
                {
                    ErrorHandler.Init(1, "Medli received the 'panic userlvl' command, Nothing's gonna happen.", false, "");
                }
            }
            else if (command == "startm")
            {
                MUI.Init();
            }
            else if (command.StartsWith("cowsay"))
            {
                Cowsay.Main(input.Remove(0, 7));
            }
            else if (command.StartsWith("mkdir "))
            {
                Fsfunc.mkdir(command.Remove(0, 6));
            }
            else if (command == "shell2")
            {
                NuShell.Run();
                Console.Clear();
            }
            else if (command == "clear")
            {
                Sysfunc.clearScreen();
            }
            else if (command.StartsWith("cp "))
            {
                cpedit.Run(command.Remove(0, 3));
            }
            else if (command == "cv")
            {
                cpview.Run();
            }
            else if (command.StartsWith("cv "))
            {
                cpview.ViewFile(command.Remove(0, 3));
            }
            else if (command == "")
            {

            }
            else if (command.StartsWith("echo "))
            {
                try
                {
                    Console.WriteLine(input.Remove(0, 5));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (command == "help")
            {
                Help.full();
            }
            else if (command.StartsWith("help "))
            {
                if (command == "help 1")
                {
                    Help.pages(1);
                }
                else if (command == "help 2")
                {
                    Help.pages(2);
                }
                else if (command == "help 3")
                {
                    Help.pages(3);
                }
                else
                {
                    Help.specific(command.Remove(0, 5));
                }
            }
            else if (command == "ver")
            {
                OSVars.Ver();
            }
            else
            {
                invalidCommand(command, 1);
            }
        }
    }
}
