//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Medli.System
//{
//    public class ErrorHandler
//    {
//        public static void Handle(errors error, string errdsc, string type)
//        {
//            if (error == errors.Permission)
//            {
//                message = errordsc[2];
//            }
//            else if (error == errors.FAT)
//            {
//                if (type == "fsnf")
//                {
//                    message = errordsc[0];
//                }
//                else if (type == "live")
//                {
//                    message = errordsc[1];
//                }
//            }
//            else if (error == errors.FATAL)
//            {
//                BlueScreen.Init(errdsc, true, error);
//            }

//            Console.BackgroundColor = ConsoleColor.Cyan;
//            Console.ForegroundColor = ConsoleColor.Black;
//            Console.WriteLine(":" + ErrorHandler.message + ":");
//            Console.WriteLine("Press any key to continue...");
//            Console.ReadKey(true);
//        }
//        public static string[] errordsc = new string[]
//        {
//            "File not found",
//            "Live user",
//            "The current user does not have permission to access this file."
//        };
//        public enum errors
//        {
//            FAT,
//            Missing,
//            Permission,
//            FATAL
//        };

//        public static string message;

//        public class BlueScreen
//        {
//            /// <summary>
//            /// BSoD equivalent message describing what has happened
//            /// </summary>
//            private static string Msg = @"
//System Sentinal
//ERROR DETECTED!";

//            /// <summary>
//            /// Initializes the BSoD equivalent, getting the error description and the error itself
//            /// TODO: Write contents of memory to a file on the hard disk
//            /// </summary>
//            /// <param name="errlvl"></param>
//            /// <param name="errdsc"></param>
//            /// <param name="err"></param>
//            public static void Show(string errdsc, errors err)
//            {
//                Console.BackgroundColor = ConsoleColor.DarkRed;
//                Console.Clear();
//                Console.WriteLine(Msg + "\n" + err);
//                Console.WriteLine("Error description: " + errdsc);
//                Console.WriteLine("Press any key to restart.");
//                Console.ReadKey(true);
//                Console.ForegroundColor = ConsoleColor.White;
//                Console.BackgroundColor = ConsoleColor.Black;
//                CoreFunc.Reboot();
//            }
//            /// <summary>
//            /// Initializes the error reporter,
//            /// works as an error handler for exceptions inside applications
//            /// </summary>
//            /// <param name="errdsc"></param>
//            /// <param name="critical"></param>
//            /// <param name="err"></param>

//            public static void Init(string errdsc, bool critical, errors error)
//            {
//                if (critical == true)
//                {
//                    BlueScreen.Show(errdsc, error);
//                }
//                else
//                {
//                    Console.BackgroundColor = ConsoleColor.Blue;
//                    Console.Clear();
//                    Console.WriteLine("Medli has encountered a system error. This means that: "); Console.WriteLine(errdsc);
//                    Console.WriteLine("Press any key to reboot.");
//                    Console.ReadKey(true);
//                    Console.ForegroundColor = ConsoleColor.White;
//                    Console.BackgroundColor = ConsoleColor.Black;
//                    Console.Clear();
//                }
//            }
//        }

//    }
//}
