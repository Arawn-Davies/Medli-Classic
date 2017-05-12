﻿using System;
using System.IO;
using Medli.System;

namespace Medli.Applications
{

    class mshell
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
            Console.Write(OSVars.pcname + ":");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Kernel.current_dir);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" $");
            cmd(Console.ReadLine());
        }
        public static void cmd(string input)
        {
            var command = input.ToLower();
            if (command.StartsWith("cd "))
            {
                FSfunc.cd(command);
            }
            else if (command.StartsWith("run"))
            {
                if (!File.Exists(Kernel.current_dir + command.Remove(0, 4)))
                {
                    invalidCommand(command.Remove(0, 4), 2);
                }
                else
                {
                    ngscript.Execute(command.Remove(0, 4));
                }
            }
            else if (command == "getram")
            {
                Sysfunc.ram();
            }
            else if (command.StartsWith("dir"))
            {
                FSfunc.dir();
            }
            else if (command.StartsWith("miv"))
            {
                MIV.StartMIV();
            }
            else if (command == "easteregg")
            {
                Tests.Test();
            }
            else if (command == "reboot")
            {
                machineinfo.reboot();
            }
            else if (command == "shutdown")
            {
                machineinfo.shutdown();
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
            else if (command.StartsWith("cowsay"))
            {
                Cowsay.Main(input.Remove(0, 7));
            }
            else if (command.StartsWith("mkdir "))
            {
                FSfunc.mkdir(command.Remove(0, 6));
            }
            else if (command == "shell2")
            {
                Shell.Run();
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
                getHelp.full();
            }
            else if (command.StartsWith("help "))
            {
                getHelp.specific(command.Remove(0, 5));
            }
            else if (command == "ver")
            {
                OSVars.ver();
            }
            else
            {
                invalidCommand(command, 1);
            }
        }
    }
}
