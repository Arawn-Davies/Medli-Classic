using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.Core;
using Medli.Hardware;

namespace Medli.System
{
    public class machineinfo
    {
        public static uint getRam() { return machine.getRam(); }
        public static void reboot() { Cosmos.HAL.Power.Reboot(); }
        public static void shutdown() { machine.ShutDown(); }
        public static void enableACPI() { if (ACPI.Init() >= 0) ACPI.Enable(); }
    }
}
