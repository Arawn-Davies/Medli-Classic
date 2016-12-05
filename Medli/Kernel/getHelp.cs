using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medli
{
    public class getHelp
    {
        public static void full()
        {
            Console.WriteLine("mkdir\tMakes a directory");
            Console.WriteLine("echo\tPrints text to the console");
            Console.WriteLine("getram\tGets the amount of system RAM in megabytes");
            Console.WriteLine("mkdir\tMakes a directory");
            Console.WriteLine("dir\tPrints a list of directories in the current directory");
            Console.WriteLine("cd\tChanges the current directory");
            Console.WriteLine("clear\tClears the screen");
            Console.WriteLine("panic\tStarts a harmless kernel panic");
            Console.WriteLine("panic critical\tStarts a critical yet harmless kernel panic");
            Console.WriteLine("cv <file>\tPrints the contents of a file onto the screen.");
            Console.WriteLine("cp <file>\tLaunches the text editor");
        }
        public static void specific(string topic)
        {

            if (topic == "mkdir")
            {
                Console.WriteLine("mkdir\tMakes a directory");
            }
            else if (topic == "clear")
            {
                Console.WriteLine("clear\tClears the screen");
            }
            else if (topic == "dir")
            {
                Console.WriteLine("dir\tPrints a list of directories in the current directory");
            }
            else if (topic == "cd")
            {
                Console.WriteLine("cd\tChanges the current directory");
            }
            else if (topic == "echo")
            {
                Console.WriteLine("echo\tPrints text to the console");
            }
            else if (topic == "getram")
            {
                Console.WriteLine("getram\tGets the amount of system RAM in megabytes");
            }
            else if (topic == "")
            {
                full();
            }
            else
            {
                Console.WriteLine(topic + ": Not a valid command.");
                full();
            }
        }
    }
}
