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
    class NuShell
    {
        public static void Run()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"
=======================================================
#                   Welcome to NuShell                #
#   Type help to display a list of accepted commands. #
#    Or go to this project's documentation for a more #
#        complete list of commands and actions.       #
=======================================================");
            bool running = true;
            while (running == true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("$");
                string input = Console.ReadLine();
                var input_args = input.Split(' ');
                if (input == "help")
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(@"
===========================================================
# Help  - displays this message                           #
# Echo  - displays userinput onto the console             #
# Clear - Clears the console                              #
# Exit  - exits the prompt                                #
# Lock  - Locks the system from accepting any user        #
#         input until a correct password/code is inputted #
# Halt  - Shuts down the system.                          #
#=========================================================#");
                }
                else if (input == "clear")
                {
                    Console.Clear();
                }
                else if (input == "halt")
                {
                    CoreFunc.Shutdown();
                }
                else if (input == "lock")
                {
                    Console.WriteLine("Missing password - try again.");
                }
                else if (input.StartsWith("lock"))
                {
                    Console.Clear();
                    bool locked = true;
                    while (locked == true)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("Insert correct password: ");
                        string pwd = Console.ReadLine();
                        if (pwd == input_args[1])
                        {
                            Console.WriteLine("Correct - unlocking system");
                            Console.Clear();
                            Console.BackgroundColor = ConsoleColor.Black;
                            locked = false;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect password - try again.");
                            locked = true;
                        }
                    }
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (input.StartsWith("echo"))
                {
                    Console.WriteLine(input_args[1]);
                }
                else if (input == "exit")
                {
                    break;
                }
                else if (input == "")
                {

                }
                else
                {
                    Console.WriteLine("Invalid command");
                }
            }
        }
    }
}
