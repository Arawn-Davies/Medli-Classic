using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medli.FileSystem.Physical
{
    public class PartInfo
    {
        public readonly byte SystemID;
        public readonly UInt32 StartSector;
        public readonly UInt32 SectorCount;

        /// <summary>
        /// Creates a new PartitionInfo
        /// </summary>
        /// <param name="aSystemID">The current partition ID</param>
        /// <param name="aStartSector">The partition start sector</param>
        /// <param name="aSectorCount">The partition number of sectors</param>
        public PartInfo(byte aSystemID, UInt32 aStartSector, UInt32 aSectorCount)
        {
            SystemID = aSystemID;
            StartSector = aStartSector;
            SectorCount = aSectorCount;
        }
    }
}
