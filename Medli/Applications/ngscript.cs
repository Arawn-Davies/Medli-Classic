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
            if (scriptname.EndsWith(".ngs"))
            {
                string[] lines = File.ReadAllLines(scriptname);
                foreach (string line in lines)
                {
                    ngshell.cmd(line);
                    Console.WriteLine("");
                    ngshell.prompt();
                }
            }
            else
            {
                Console.WriteLine("Not a valid Midnascript file.");
            }
        }
    }
}
