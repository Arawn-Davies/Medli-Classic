using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AIC_Framework;

namespace Medli.Applications
{
    class cpview
    {
        private static void DrawScreen()
        {
            AConsole.Fill(ConsoleColor.Blue);
            Console.CursorTop = 0;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine(" Cocoapad Viewer ");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.CursorTop = 3;
        }
        public static void ViewFile(string file)
        {
            DrawScreen();
            try
            {
                if (File.Exists(MEnvironment.current_dir + @"\" + file))
                {
                    string[] lines = File.ReadAllLines(MEnvironment.current_dir + @"\" + file);
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }
                else if (!File.Exists(MEnvironment.current_dir + @"\" + file))
                {
                    Shell.invalidCommand(file, 2);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            MEnvironment.PressAnyKey();
        }
    }
}
