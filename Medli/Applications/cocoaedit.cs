using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Medli.Applications
{
    class cpedit
    {
        public static string text = "";
        public static string savedtext = "";
        public static bool running = true;
        public static void Run(string file)
        {
            Console.WriteLine("--|:Welcome to Cocoapad Editor:|--");
            Console.WriteLine("Cocoapad is a multi line text editor you can use to create many files.");
            Console.WriteLine("Once you have finished you can type 'save' to save your file or 'end' to close without saving.");
            Console.WriteLine("Filenames can only have 3 letter extensions.");
            text = "";
            string line;
            var num = 1;
            while (running == true)
            {
                Console.Write(num + ":");
                num = num + 1;
                line = Console.ReadLine();
                if (line == "end")
                {
                    Console.WriteLine("Would you like to save first?");
                    string notsaved = Console.ReadLine();
                    if (notsaved == "y")
                    {
                        File.Create(Kernel.current_dir + file);
                        File.WriteAllText(Kernel.current_dir + file, text);
                        savedtext = text;
                        running = false;
                        ngshell.prompt();
                    }
                    else if (notsaved == "n")
                    {
                        running = false;
                        ngshell.prompt();
                    }
                }
                if (line == "save")
                {
                    File.Create(Kernel.current_dir + file);
                    File.WriteAllText(Kernel.current_dir + file, text);
                    savedtext = text;
                    running = false;
                    ngshell.prompt();
                }
                text = text + (Environment.NewLine + line);
            }
        }
    }
}