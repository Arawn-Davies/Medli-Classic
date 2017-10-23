using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Medli.SysInternal;
using Medli.System;

namespace Medli.Applications
{
    class Shell2
    {
        public static void Run()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(KernelVariables.username);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("@");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(KernelVariables.pcname + ":");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(MEnvironment.current_dir);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("$");
                var cinput = Console.ReadLine();
                var coutput = cinput.SplitAtFirstSpace()[1];
                CommandManager.Cmd_mgr.Run(cinput, coutput);
                if (cinput == "exit")
                {
                    break;
                }
                else if (cinput == "")
                {

                }
                else
                {
                    Console.WriteLine("Invalid command. See 'help' for more commands.");
                }
            }
        }
    }
}
