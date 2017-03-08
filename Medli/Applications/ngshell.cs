using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Medli.System;
using Medli.Applications;

namespace Medli.Applications
{

    class ngshell
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
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            
            
        }
        public static void prompt()
        {
            Console.ForegroundColor = ConsoleColor.Green;
<<<<<<< HEAD
            
            Console.Write(" $ ");
            Console.ForegroundColor = ConsoleColor.White;
=======
            Console.Write(OSVars.pcname + ":");
            Console.Write(Kernel.current_dir);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("$ ");
>>>>>>> origin/master
            cmd(Console.ReadLine());
        }
        public static void cmd(string input)
        {
            var command = input.ToLower();
            if (command.StartsWith("cd "))
            {
                fsfunctions.cd(command);
            }
<<<<<<< HEAD
            else if (command == "miv")
            {
                Applications.MIV.Start();
=======
            else if (command.StartsWith("run"))
            {
                ngscript.Execute(command.Remove(0, 4));
>>>>>>> origin/master
            }
            else if (command == "sysinfo")
            {
                Console.WriteLine("");
                Console.WriteLine("Amount of RAM installed: " + machineinfo.getRam() + " megabytes");
            }
            else if (command.StartsWith("dir"))
            {
                fsfunctions.dir();
            }
            else if (command == "miv")
            {
                MIV.StartMIV();
            }
            else if (command == "easteregg")
            {
                Tests.Test();
            }
            else if (command == "reboot")
            {
                Cosmos.System.Power.Reboot();
                //machineinfo.reboot();
            }
            else if (command == "shutdown")
            {
                machineinfo.shutdown();
            }
            else if (command.StartsWith("panic"))
            {
                if (command.Remove(0, 6) == "critical")
                {
                    ErrorHandler.Init(0, "Medli received the 'panic critical' command , Nothings gonna happen", true, "User-invoked panic");
                }
                else
                {
                    ErrorHandler.Init(1, "Medli received the 'panic' command, Nothing's gonna happen.", false, "");
                }
            }
            else if (command.StartsWith("mkdir "))
            {
                fsfunctions.mkdir(command.Remove(0, 6));
            }
            else if (command == "clear")
            {
                Console.Clear();
                OSVars.ver();

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
                    Console.WriteLine(command.Remove(0, 5));
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
