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
    class MIDE
    {
        public static string AppTitle;
        public static string AppDesc;
        public static string AppAuthor;
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
        private static void DrawScreen()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.CursorTop = 0;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("|Medli Application IDE|");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.CursorTop = 3;
        }
        private static void AppInfo()
        {
            DrawScreen();
            Console.WriteLine("Enter the application title:");
            AppTitle = "Title=" + Console.ReadLine();
            Console.WriteLine("Enter the application description:");
            AppDesc = "Desc=" + Console.ReadLine();
            Console.WriteLine("Enter the application author:");
            AppAuthor = "Author=" + Console.ReadLine();
            DrawScreen();
            Console.WriteLine(AppTitle + "\n" + AppDesc + "\n" + AppAuthor);
            text = AppTitle + Environment.NewLine + AppDesc + Environment.NewLine + AppAuthor + Environment.NewLine;
        }
        /// <summary>
        /// Main method for the Cocoapad edit
        /// Originally from Chocolate OS (pre-Medli) but won't rename this application
        /// </summary>
        /// <param name="file"></param>
        public static void Run(string file)
        {
            DrawScreen();
            Console.WriteLine("The Medli Application IDE is an Integrated Development Environment");
            Console.WriteLine("users can use to develop applications for Medli.");
            Console.WriteLine("The same basic commands are used as Cocoapad Editor, but with a few");
            Console.WriteLine("extra commands to allow for the creation and running of apps.\n");
            Console.WriteLine("IDE commands:");Console.WriteLine("\n$END - Exits the IDE without saving, $SAVE - Saves the current file");
            Console.WriteLine("$RESET - Resets the IDE and file to start again from fresh\n$RUN - Saves the file and executes it in the Medli Application Launcher");
            MEnvironment.PressAnyKey("Press any key to begin!");
            DrawScreen();
            text = "";
            AppInfo();
            string line;
            var num = 4;
            while (running == true)
            {
                Console.Write(num + ": ");
                num = num + 1;
                line = Console.ReadLine();
                if (line == "$END")
                {
                    Console.WriteLine("Would you like to save first?");
                    string notsaved = Console.ReadLine();
                    if (notsaved == "y")
                    {
                        File.Create(MEnvironment.current_dir + file);
                        File.WriteAllText(MEnvironment.current_dir + file, text);
                        savedtext = text;
                        Console.BackgroundColor = ConsoleColor.Black;
                        running = false;
                        Shell.prompt();
                    }
                    else if (notsaved == "n")
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        running = false;
                        Shell.prompt();
                    }
                }
                if (line == "$RESET")
                {
                    text = "";
                    AppInfo();
                }
                if (line == "$SAVE")
                {
                    File.WriteAllText(MEnvironment.current_dir + @"\" + file, text);
                    savedtext = text;
                    running = false;
                    Shell.prompt();
                }
                if (line == "$RUN")
                {
                    File.WriteAllText(MEnvironment.current_dir + @"\" + file, text);
                    savedtext = text;
                    running = false;
                    Console.Clear();
                    AppLauncher.PreExecute(file);
                    MEnvironment.PressAnyKey();
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Clear();
                    Shell.prompt();
                }
                text = text + (Environment.NewLine + line);
                if (Console.CursorTop == 24)
                {
                    DrawScreen();
                }
            }
        }
    }
}