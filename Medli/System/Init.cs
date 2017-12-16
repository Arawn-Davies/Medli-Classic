using System;
using System.Collections.Generic;
using System.Text;
using Medli.System.FileSystem;
using Medli.FileSystem.Physical;
using Cosmos.HAL.BlockDevice;
using Cosmos.System.FileSystem.Listing;
using Medli.FileSystem.Physical.Drivers;

namespace Medli.System
{
    class Init
    {
        public static bool Error = false;

        private static PrimaryPartition workPartition = null;

        public static void InitMain()
        {
            Console.WriteLine("Getting Disks...");
            IDE[] IDEs = IDE.Devices.ToArray();
            Console.WriteLine("Number of IDE disks: " + IDEs.Length);
            Console.WriteLine("Looking for valid partitions...");

            for (int i = 0; i<IDEs.Length; i++)
            {
                PrimaryPartition[] parts = IDEs[i].PrimaryPartitions;
                for (int j = 0; j<parts.Length; j++)
                {
                    if (parts[j].Infos.SystemID == 0xFA)
                    {
                        workPartition = parts[j];
                    }
}
            }
//#warning Revert to == null!!!
            if (workPartition == null)
            {
                FileSystem.Physical.DiskHandler.CreatePartitions(IDEs);
                Console.WriteLine("The machine needs to be restarted.");
                MEnvironment.PressAnyKey();
                SysInternal.Sysfunc.reboot();
            }
        }
    }
}
