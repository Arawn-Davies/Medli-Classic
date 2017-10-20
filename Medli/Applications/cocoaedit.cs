using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Medli.Applications
{
    /// <summary>
    /// Cocoapad Editor class
    /// contains methods needed for the editor to function
    /// </summary>
    class cpedit
    {
        /// <summary>
        /// The current text inside the editor is stored in a string
        /// It gets transferred to the 'savedtext' string when saved.
        /// </summary>
        public static string text = "";
        /// <summary>
        /// Saved text is stored in a string
        /// </summary>
        public static string savedtext = "";
        /// <summary>
        /// Boolean to see whether Cocoapad is running or not
        /// </summary>
        public static bool running = true;
        /// <summary>
        /// Main method for the Cocoapad edit
        /// Originally from Chocolate OS (pre-Medli) but won't rename this application
        /// </summary>
        /// <param name="file"></param>
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
                        Shell.prompt();
                    }
                    else if (notsaved == "n")
                    {
                        running = false;
                        Shell.prompt();
                    }
                }
                if (line == "save")
                {
                    File.Create(Kernel.current_dir + file);
                    File.WriteAllText(Kernel.current_dir + file, text);
                    savedtext = text;
                    running = false;
                    Shell.prompt();
                }
                text = text + (Environment.NewLine + line);
            }
        }
    }
}