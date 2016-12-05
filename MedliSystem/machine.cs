using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.Core;
using Medli.Hardware;

namespace Medli.System
{
    public class machineinfo
    {
        public static uint getRam() { return machine.getRam(); }
        public static void reboot() { Cosmos.System.Power.Reboot(); }
        public static void shutdown() { machine.ShutDown(); }
        public static void enableACPI() { if (ACPI.Init() >= 0) ACPI.Enable(); }
    }
}
