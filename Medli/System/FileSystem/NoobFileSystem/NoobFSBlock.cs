/* NoobFSBlock.cs - defines a block in NoobFS
 * Copyright (C) 2012 NoobOS
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.HAL.BlockDevice;

namespace Medli.FileSystem.NoobFileSystem
{
    class NoobFSBlock
    {
        public static uint MaxBlockContentSize = 491;

        private Partition _Partition;

        private ulong _BlockNumber = 0;

        private bool _Used = false;
        private uint _ContentSize = 0;
        private ulong _TotalSize = 0;
        private ulong _NextBlock = 0;

        /// <summary>
        /// The partition that the block belong to
        /// </summary>
        public Partition Partition
        {
            get
            {
                return _Partition;
            }
        }

        /// <summary>
        /// The BlockNumber of the current block
        /// </summary>
        public ulong BlockNumber
        {
            get
            {
                return _BlockNumber;
            }
        }

        /// <summary>
        /// Block used or not used
        /// </summary>
        public bool Used
        {
            get
            {
                return _Used;
            }
            set
            {
                _Used = value;
            }
        }

        /// <summary>
        /// The current Content Size
        /// </summary>
        public uint ContentSize
        {
            get
            {
                return _ContentSize;
            }
            set
            {
                _ContentSize = value;
            }
        }

        /// <summary>
        /// Total size of the Entry (Not Used)
        /// </summary>
        public ulong TotalSize
        {
            get
            {
                return _TotalSize;
            }
            set
            {
                _TotalSize = value;
            }
        }

        /// <summary>
        /// The next block number to read al the content
        /// </summary>
        public ulong NextBlock
        {
            get
            {
                return _NextBlock;
            }
            set
            {
                _NextBlock = value;
            }
        }

        /// <summary>
        /// The Content Byte Array. TO-DO: code this better
        /// </summary>
        public Byte[] Content;

        /// <summary>
        /// Creates a new virtual Block.
        /// </summary>
        /// <param name="Data">The Byte data</param>
        /// <param name="p">The partition to use</param>
        /// <param name="bn">The block number</param>
        public NoobFSBlock(Byte[] Data, Partition p, ulong bn)
        {
            _BlockNumber = bn;
            _Partition = p;
            Content = new Byte[Data.Length - 21];
            if (Data[0] == 0x00)
            {
                _Used = false;
                for (int i = 0; i < Content.Length; i++)
                {
                    Content[i] = 0;
                }
            }
            else
            {
                _Used = true;
                _ContentSize = BitConverter.ToUInt32(Data, 1);
                _TotalSize = BitConverter.ToUInt64(Data, 5);
                _NextBlock = BitConverter.ToUInt64(Data, 13);
                for (int i = 21; i < Data.Length; i++)
                {
                    Content[i - 21] = Data[i];
                }
            }
        }

        /// <summary>
        /// The
        /// </summary>
        /// <param name="p">The partition to read</param>
        /// <param name="bn">The blocknumber to read</param>
        public static NoobFSBlock Read(Partition p, ulong bn)
        {
            Byte[] data = p.NewBlockArray(1);
            p.ReadBlock(bn, 1, data);
            return new NoobFSBlock(data, p, bn);
        }

        /// <summary>
        /// Writes a block into a specific partition
        /// </summary>
        /// <param name="p">The partition to write to</param>
        /// <param name="b">The block to write</param>
        public static void Write(Partition p, NoobFSBlock b)
        {
            Byte[] data = new Byte[p.BlockSize];
            int index = 0;
            if (b.Used)
            {
                data[index++] = 0x01;
            }
            else
            {
                data[index++] = 0x00;
            }
            Byte[] x = BitConverter.GetBytes(b.ContentSize);
            for (int i = 0; i < x.Length; i++)
            {
                data[index++] = x[i];
            }
            x = BitConverter.GetBytes(b.TotalSize);
            for (int i = 0; i < x.Length; i++)
            {
                data[index++] = x[i];
            }
            x = BitConverter.GetBytes(b.NextBlock);
            for (int i = 0; i < x.Length; i++)
            {
                data[index++] = x[i];
            }
            x = b.Content;
            for (int i = 0; i < x.Length; i++)
            {
                data[index++] = x[i];
            }
            p.WriteBlock(b.BlockNumber, 1, data);
        }

        /// <summary>
        /// Get the free block from the selected partition (TO-DO: Implement something that runs faster)
        /// </summary>
        /// <param name="p">The partition to get the block from</param>
        public static NoobFSBlock GetFreeBlock(Partition p)
        {
            for (ulong i = 1; i < p.BlockCount; i++)
            {
                NoobFSBlock b = Read(p, i);
                if (!b.Used)
                {
                    return b;
                }
            }
            return null;
        }
    }
}
