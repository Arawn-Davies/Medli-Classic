using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Medli.Applications
{
    class cpview
    {
        public static void Run()
        {
            Console.WriteLine("Cocoapad Viewer\n");
            Console.Write(cpedit.savedtext);
            Console.CursorTop = Console.CursorTop + 1;
            Console.CursorLeft = 0;
        }
        public static void ViewFile(string file)
        {
            try
            {
                if (File.Exists(Kernel.current_dir + file))
                {
                    string[] lines = File.ReadAllLines(Kernel.current_dir + file);
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }
                else if (!File.Exists(Kernel.current_dir + file))
                {
                    mshell.invalidCommand(file, 2);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
