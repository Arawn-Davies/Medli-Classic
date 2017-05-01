using System;
using System.Collections.Generic;
using System.Linq;

namespace Medli.Applications
{
    public class Cowsay
    {
        public static void print(string args)
        {
            int length = args.Length;
            for (int i = 1; i <= length; i++)
            {
                Console.Write("-");
            }
        }
        public static void Main(string args)
        {

            Console.Write("/-"); print(args); Console.WriteLine(@"-\");
            Console.WriteLine("|-" + args + @"-|");
            Console.Write(@"\-"); print(args); Console.Write(@"-/");
            Console.WriteLine(@"
        \   ^__^
         \  (oo)\_______
            (__)\       )\/\
                ||----w |
                ||     ||");
            Console.ReadKey(true);
        }
    }
}