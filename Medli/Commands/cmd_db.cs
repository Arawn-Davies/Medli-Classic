using System;
using System.Collections.Generic;
using System.Text;

namespace Medli.Command_db
{
    public abstract class CMD_DB
    {
        public string func_name;
        public string cmd_name;
        public string short_help;
        public string full_help;

        public abstract void Run(string args);
    }
}
