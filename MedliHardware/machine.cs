using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.Core;

namespace Medli.Hardware
{
    public class machine
    {
        public static uint getRam() { return CPU.GetAmountOfRAM(); }
        public static void ShutDown()
        {
            ACPI.Shutdown();
            ACPI.Disable();
            Global.CPU.Halt();
        }
    }
}
