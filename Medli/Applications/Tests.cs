using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Medli.Applications
{
    class Tests
    {
        public static string bsod = @"A problem has been detected and Windows has commited suicide to damage your computer.
The problem seems to be caused by the following file: POKEMON.SYS

LULZ_FAULT_IN_EASTEREGG_APP

If this is the first time you've seen this scary as f*** screen,
give up all hope. If this screen appears again, follow these steps:
Throw away this computer.
If this is a new installation, ask your hardware or software manufacturer
what's the point in buying Microsoft Windows products are.
If problems continue, don't bother using computers. 
Get rid of memories such as who you are or your bank account PIN.
If you need to use a computer to look at dank memes,
press F8 on an imaginary keyboard to look like an absolute fool, and then select 'Why am I alive?'.
Not-so Technical information:
*** STOP: H4ck3rT1m3 (0xNeverGonnaGive,0xYouUp,0xNeverGonnaLet,0xYouDown)
***  POKEMON.SYS - Address lastase at FUCKm333, DateStamp DayAfterYesterday";
        public static void Test()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("test");
            Console.Clear();
            Console.Write(bsod);
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey(true);
        }
    }
}
