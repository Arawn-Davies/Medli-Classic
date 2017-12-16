using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.HAL.BlockDevice;

namespace Medli.FileSystem.Physical
{
    public class PrimaryPartition : Partition
    {
        BlockDevice mHost;

        ulong mStartingSector;

        private PartInfo _infos;

        /// <summary>
        /// Gets the interal PartInfo object
        /// </summary>
        public PartInfo Infos
        {
            get
            {
                return _infos;
            }
        }

        /// <summary>
        /// Creates a new PrimaryPartition
        /// </summary>
        /// <param name="aHost">The blockdevice to use</param>
        /// <param name="aStartingSector">The partion starting sector</param>
        /// <param name="aSectorCount">the number of sectors in the partition</param>
        /// <param name="info">The partition infos</param>
        public PrimaryPartition(BlockDevice aHost, ulong aStartingSector, ulong aSectorCount, PartInfo info)
            : base(aHost, aStartingSector, aSectorCount)
        {
            mHost = aHost;
            mStartingSector = aStartingSector;
            mBlockCount = aSectorCount;
            mBlockSize = aHost.BlockSize;
            _infos = info;
        }

        /// <summary>
        /// Reads a number of blocks from the partition
        /// </summary>
        /// <param name="aBlockNo">Start block number</param>
        /// <param name="aBlockCount">Number of blocks</param>
        /// <param name="aData">Buffer to write to</param>
        public override void ReadBlock(ulong aBlockNo, ulong aBlockCount, byte[] aData)
        {
            UInt64 xHostBlockNo = mStartingSector + aBlockNo;
            CheckBlockNo(xHostBlockNo, aBlockCount);
            mHost.ReadBlock(xHostBlockNo, aBlockCount, aData);
        }

        /// <summary>
        /// Writes a number a buffer down to the current partition
        /// </summary>
        /// <param name="aBlockNo">Start block number</param>
        /// <param name="aBlockCount">Number of blocks</param>
        /// <param name="aData">Buffer to read from</param>
        public override void WriteBlock(ulong aBlockNo, ulong aBlockCount, byte[] aData)
        {
            UInt64 xHostBlockNo = mStartingSector + aBlockNo;
            CheckBlockNo(xHostBlockNo, aBlockCount);
            mHost.WriteBlock(xHostBlockNo, aBlockCount, aData);
        }
    }
}
