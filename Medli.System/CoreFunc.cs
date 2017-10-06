using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.Core;
using Medli.Hardware;

namespace Medli.System
{
    public class CoreFunc
    {
        public static uint GetRam() { return Machine.GetRam(); }
        public static void Reboot() { Machine.Reboot(); }
        public static void Shutdown() { Machine.ShutDown(); }
        public static void EnableACPI() { Machine.EnableACPI(); }
    }
}
