using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medli.System;

namespace Medli.FileSystem.Physical
{
    public class MBR
    {
        private List<PartInfo> _Partitions = new List<PartInfo>();

        /// <summary>
        /// An array containing all patitions
        /// </summary>
        public PartInfo[] Partitions
        {
            get
            {
                return _Partitions.ToArray();
            }
        }

        /// <summary>
        /// The Byte Array containing the bootable code of the MBR
        /// </summary>
        public readonly Byte[] Bootable = new Byte[440];

        /// <summary>
        /// The Disk signature
        /// </summary>
        public readonly UInt32 Signature = 0;

        /// <summary>
        /// Initializes a new MBR object getting the content of the first block of the disk
        /// </summary>
        /// <param name="aMBR">Byte rapresentation of the first disk block</param>
        public MBR(byte[] aMBR)
        {
            DataUtils.CopyByteToByte(aMBR, 0, Bootable, 0, 440);
            Signature = BitConverter.ToUInt32(aMBR, 440);
            ParsePartition(aMBR, 446);
            ParsePartition(aMBR, 462);
            ParsePartition(aMBR, 478);
            ParsePartition(aMBR, 494);
        }

        private void ParsePartition(byte[] aMBR, UInt32 aLoc)
        {
            byte xSystemID = aMBR[aLoc + 4];
            // SystemID = 0 means no partition
            if (xSystemID != 0)
            {
                UInt32 xStartSector = BitConverter.ToUInt32(aMBR, (int)aLoc + 8);
                UInt32 xSectorCount = BitConverter.ToUInt32(aMBR, (int)aLoc + 12);

                var xPartInfo = new PartInfo(xSystemID, xStartSector, xSectorCount);
                _Partitions.Add(xPartInfo);
            }
        }
    }
}
