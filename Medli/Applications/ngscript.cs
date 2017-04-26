using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Medli.Applications
{
    class ngscript
    {
        public static void Execute(string scriptname)
        {
            if (scriptname.EndsWith(".txt"))
            {
                string[] lines = File.ReadAllLines(scriptname);
                foreach (string line in lines)
                {
                    mshell.cmd(line);
                    Console.WriteLine("");
                }
                mshell.prompt();
            }
            else if (scriptname.EndsWith(".mds"))
                {
                    string[] lines = File.ReadAllLines(scriptname);
                    foreach (string line in lines)
                    {
                        mshell.cmd(line);
                        Console.WriteLine("");
                    }
                    mshell.prompt();
            }
            else
            {
                Console.WriteLine("Not a valid Midnascript file.");
            }
        }
    }
}
