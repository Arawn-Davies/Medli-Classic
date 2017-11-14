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
    class ShellInfo
    {
        public static MultiShell AdminShell = new MultiShell(0);
        public static MultiShell shell1 = new MultiShell(1);
        public static MultiShell shell2 = new MultiShell(2);
        public static MultiShell shell3 = new MultiShell(3);
        public static MultiShell shell4 = new MultiShell(4);
    }
    class MultiShell
    {
        public void Run(int num_shell)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Shell " + num_shell + "> ");
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
            Shell.cmd(Console.ReadLine());
        }
        public int no_shell;
        public MultiShell(int number_shell)
        {
            no_shell = number_shell;
        }
    }
}
