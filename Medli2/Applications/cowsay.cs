using System;
using System.Collections.Generic;
using System.Linq;

namespace Medli.Applications
{
    /// <summary>
    /// A must-have for any command line operating system
    /// Doesn't have the other characters from the actual cowsay,
    /// but it'll do for now :P
    /// </summary>
    public class Cowsay
    {
        /// <summary>
        /// Prints the argument passed to 'cowsay' into the
        /// speech bubble said by the cow
        /// </summary>
        /// <param name="args"></param>
        public static void print(string args)
        {
            int length = args.Length;
            for (int i = 1; i <= length; i++)
            {
                Console.Write("-");
            }
        }
        /// <summary>
        /// Main method for Medli cowsay, standard feature :P
        /// This basically prints the cow onto the screen, 
        /// then calls 'print()' to render the message
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string args)
        {

            Console.Write("/--"); print(args); Console.WriteLine(@"--\");
            Console.WriteLine("|- " + args + @" -|");
            Console.Write(@"\--"); print(args); Console.Write(@"--/");
            Console.WriteLine(@"
       \
        \   ^__^
         \  (oo)\_______
            (__)\       )\/\
                ||----w |
                ||     ||");
        }
    }
}