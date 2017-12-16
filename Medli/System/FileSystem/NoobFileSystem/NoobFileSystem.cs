/* NoobFileSystem.cs - the main file for defining NoobFS
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
using Fs = Cosmos.System.FileSystem;
using Cosmos.HAL.BlockDevice;
using Medli.System;

namespace Medli.FileSystem.NoobFileSystem
{
    class NoobFileSystem : Fs.FileSystem
    {
        private Byte[] recHash = new Byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e };

        private Partition part;

        #region static Methods And Variables
        /// <summary>
        /// The current used filesystem. TO-DO: Move this thing to GlobalEnvironment class
        /// </summary>
        public static NoobFileSystem mFS = null;

        /// <summary>
        /// The separator for the path
        /// </summary>
        public static String separator = "/";

        /// <summary>
        /// Clears every block starting from this one
        /// </summary>
        /// <param name="StartBlock">The block to start from</param>
        public static void ClearBlocks(NoobFSBlock StartBlock)
        {
            NoobFSBlock b = StartBlock;
            while (b.NextBlock != 0)
            {
                b = NoobFSBlock.Read(b.Partition, b.NextBlock);
                b.Used = false;
                NoobFSBlock.Write(mFS.Partition, b);
            }
        }

#warning TODO!!!
        /// <summary>
        /// Simply sets mFS
        /// </summary>
        /// <param name="aPath">The path to assign (Still not working)</param>
        /// <param name="aFileSystem">The Filesystem to assign</param>
        public static void AddMapping(string aPath, NoobFileSystem aFileSystem)
        {
            mFS = aFileSystem;
        }

        /// <summary>
        /// Combines two strings into one by getting a path and a filename
        /// </summary>
        /// <param name="_Path">The path</param>
        /// <param name="name">The filename</param>
        public static string CombineFile(string _Path, string name)
        {
            String ret = "";
            ret = _Path.TrimEnd(separator.ToCharArray());
            if (ret == null)
            {
                ret = "";
            } 
            if (name != separator)
            {
                ret += ret += separator + name; 
            }
            else
            {
                ret = "/";
            }
            return ret;
        }

        /// <summary>
        /// Combines two strings into one by getting a path and a directory name
        /// </summary>
        /// <param name="_Path">The path</param>
        /// <param name="name">The directory name</param>
        public static string CombineDir(string _Path, string name)
        {
            String ret = "";
            ret = _Path.TrimEnd(separator.ToCharArray());
            if (ret == null)
            {
                ret = "";
            }
            if (name != separator)
            {
                ret += separator + name + separator;
            }
            else
            {
                ret = "/";
            }
            return ret;
        }
        #endregion

        #region public Properties
        /// <summary>
        /// The partition of the current Filesystem
        /// </summary>
        public Partition Partition
        {
            get
            {
                return part;
            }
        }

        /// <summary>
        /// The Root directory of the current filesystem
        /// </summary>
        public NoobDirectory Root
        {
            get
            {
                return new NoobDirectory(part, 1, separator);
            }
        }

        /// <summary>
        /// The blocksize of the current partition
        /// </summary>
        public ulong BlockSize
        {
            get
            {
                return part.BlockSize;
            }
        }

        /// <summary>
        /// The number of blocks of the current partition
        /// </summary>
        public ulong BlockCount
        {
            get
            {
                return part.BlockCount;
            }
        }
        #endregion

        /// <summary>
        /// Creates a new filesystem basing it on a Partition
        /// </summary>
        /// <param name="p">The partition of the filesystem</param>
        public NoobFileSystem(Partition p)
        {
            part = p;
            if (!ISNoobFS())
            {
                Console.WriteLine("");
                Console.Write("Must clear and rewrite a new fileSystem...");
                if (!CreateNewNoobFS())
                {
                    ExConsole.Error("Cannot create New FileSystem!");
                }
            }
        }

        /// <summary>
        /// Formats the partition to use NoobFS
        /// </summary>
        /// <param name="p">Partition to use</param>
        private bool CreateNewNoobFS()
        {
            CleanFS(30000); //Using 30000 Blocks because memory management is not good and when cleaning all blocks, it throws a exception "Too large memory block allocated"
            Byte[] data = part.NewBlockArray(1);
            recHash.CopyTo(data, 0);
            //Write Definition of the Root Directory
            for (int i = recHash.Length; i < data.Length; i++)
            {
                data[i] = 0x00;
            }
            part.WriteBlock(0, 1, data);
            return true;
        }

        /// <summary>
        /// Gets if this is a NoobFilesystem or not
        /// </summary>
        /// <param name="p">Partition to use</param>
        private bool ISNoobFS()
        {
            Byte[] data = part.NewBlockArray(1);
            part.ReadBlock(0, 1, data);
            for (int i = 0; i < recHash.Length; i++)
            {
                if (recHash[i] != data[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Cleans the current Filesystem stopping the cleaning at the block setted. if 0 clears all
        /// </summary>
        /// <param name="stop">The stop block</param>
        public void CleanFS(ulong stop)
        {
            Console.WriteLine("Cleaning FS...");
            Byte[] data = part.NewBlockArray(1);
            for (int j = 0; j < data.Length; j++)
            {
                data[j] = 0;
            }
            Console.WriteLine("Starting...");
            uint percent = 0;
            ulong max = part.BlockCount;
            if (stop != 0)
            {
                max = stop;
            }
            ulong rate = max / 100;
            Console.WriteLine(percent + "% Done. " + (uint)max + " Blocks Left. ");
            for (ulong i = 0; i < max; i++)
            {
                part.WriteBlock(i, 1, data);
                if (i % rate == 0)
                {
                    percent++;
                }
                if (i % 32 == 0)
                {
                    ExConsole.WriteOnLastLine(percent + "% Done. " + ((uint) (max - i)) + " Blocks Left ");
                }
            }
            ExConsole.WriteOnLastLine("100 % Done");
        }
    }
}
