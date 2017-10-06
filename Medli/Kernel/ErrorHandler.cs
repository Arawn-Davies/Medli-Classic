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
            /// <summary>
            /// BSoD equivalent message, when users see this, then they know it truly is an 'O shit' situation
            /// </summary>
            public static string Msg = @"

                '||            ||    
                 ||      ''    ||    
.|''|,    (''''  ||''|,  ||  ''||''  
||  ||     `'')  ||  ||  ||    ||    
`|..|'    `...' .||  || .||.   `|..'

What's happened now?! ";
            /// <summary>
            /// Initializes the BSoD equivalent, getting the error level, 
            /// error description and the error itself
            /// </summary>
            /// <param name="errlvl"></param>
            /// <param name="errdsc"></param>
            /// <param name="err"></param>
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
                CoreFunc.Reboot();
            }
        }
        /// <summary>
        /// Initializes the error reporter,
        /// works as an error handler for exceptions inside applications
        /// </summary>
        /// <param name="errlvl"></param>
        /// <param name="errdsc"></param>
        /// <param name="critical"></param>
        /// <param name="err"></param>
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
