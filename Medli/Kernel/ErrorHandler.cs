using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medli.System;
using System.Threading.Tasks;

namespace Medli
{
    public class ErrorHandler
    {
        public class BlueScreen
        {
            public static string Msg = @"
OOOOO  H  H    SSSSS  H  H  i  TTTTT    MMMMM  AAAAA  TTTTT  EEEEE 
O   O  H  H    S      H  H  I    T      M M M  A   A    T    E
O   O  HHHH    SSSSS  HHHH  I    T      M M M  AAAAA    T    EEEEE
O   O  H  H        S  H  H  I    T      M M M  A   A    T    E
OOOOO  H  H    SSSSS  H  H  I    T      M M M  A   A    T    EEEEE

What's happend? ";
            public static void Init(int errlvl, string errdsc, string err)
            {

                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.Clear();
                Console.WriteLine(Msg + err);
                Console.WriteLine("This means that: " + errdsc);
                Console.WriteLine("Press any key to restart :/");
                Console.ReadKey(true);
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

            }
        }

    }
}
