using System;
using System.Collections.Generic;
using System.Text;

namespace Medli.Command_db
{
    public class CMD_MGMT
    {
        public static List<CMD_DB> Commands = new List<CMD_DB>();

        public void Init()
        {
            Commands.Add(new Commands.GetHelp());
        }

        public void Run(string cmd, string args)
        {
            bool g = false;
            for (int i = 0; i < Commands.Count - 1; i++)
            {
                Console.WriteLine("commandname: " + Commands[i].cmd_name);
                Console.WriteLine("cmd: " + cmd);
                if (Commands[i].cmd_name.ToLower() == cmd.ToLower())
                {
                    Commands[i].Run(args);
                    g = true;
                }
            }
        }
    }
}
