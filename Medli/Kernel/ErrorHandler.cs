using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medli.System;

namespace Medli
{
    public class ErrorHandler
    {
        public class BlueScreen
        {
            public static string Msg = @"

                '||            ||    
                 ||      ''    ||    
.|''|,    (''''  ||''|,  ||  ''||''  
||  ||     `'')  ||  ||  ||    ||    
`|..|'    `...' .||  || .||.   `|..'

What's happened now?! ";
            public static void Init(int errlvl, string errdsc, string err)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Clear();
                Console.WriteLine(Msg + err);
                Console.WriteLine("This means that: "); Console.WriteLine(errdsc);
                Console.WriteLine("Press any key to restart.");
                Console.ReadKey(true);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                machineinfo.reboot();
            }
        }
        public static void Init(int errlvl, string errdsc, bool critical, string err)
        {
            if (critical == true)
            {
                BlueScreen.Init(errlvl, errdsc, err);
            }
            else if (critical == false)
            {
                if (errlvl == 5)
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Clear();
                Applications.Cowsay.Main("Whoops!");
                Console.WriteLine("You've encountered an error. This means that: "); Console.WriteLine(errdsc);
                Console.WriteLine("Press any key to return to shell.");
                Console.ReadKey(true);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
            }
        }

    }
}
