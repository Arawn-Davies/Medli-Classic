using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Medli.Applications
{
    class mdscript
    {
        public static void Execute(string scriptname)
        {
            try
            {
                if (scriptname.EndsWith(".mds"))
                {
                    string[] lines = File.ReadAllLines(scriptname);
                    foreach (string line in lines)
                    {
                        mshell.cmd(line);
                        //Console.WriteLine("");
                    }
                }
                else
                {
                    Console.WriteLine("Not a valid Midnascript file.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
