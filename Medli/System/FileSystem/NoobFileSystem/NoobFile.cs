/* NoobFile.cs - defines the class for a file in NoobOS
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
using Medli.System;

namespace Medli.FileSystem.NoobFileSystem
{
    class NoobFile : NoobEntry
    {
        /// <summary>
        /// The FullName (Path+Name) of the Current NoobFile
        /// </summary>
        public String FullName
        {
            get
            {
                return NoobFileSystem.CombineDir(_Path, Name);
            }
        }

        /// <summary>
        /// Writes all the specified Bytes into the file
        /// </summary>
        /// <param name="Data">The array Byte to write into file</param>
        public void WriteAllBytes(Byte[] Data)
        {
            if (_StartBlock.NextBlock != 0)
            {
                NoobFileSystem.ClearBlocks(_StartBlock);
                _StartBlock.NextBlock = 0;
                NoobFSBlock.Write(NoobFileSystem.mFS.Partition, _StartBlock);
            }
            int index = 0;
            NoobFSBlock curb = NoobFSBlock.GetFreeBlock(NoobFileSystem.mFS.Partition);
            _StartBlock.NextBlock = curb.BlockNumber;
            NoobFSBlock.Write(part, _StartBlock);
            do
            {
                Byte[] arr = new Byte[NoobFSBlock.MaxBlockContentSize];
                index = DataUtils.CopyByteToByte(Data, index, arr, 0, arr.Length);
                curb.Used = true;
                curb.Content = arr;
                if (index != Data.Length)
                {
                    NoobFSBlock b = NoobFSBlock.GetFreeBlock(NoobFileSystem.mFS.Partition);
                    curb.NextBlock = b.BlockNumber;
                    curb.ContentSize = (uint)arr.Length;
                    NoobFSBlock.Write(NoobFileSystem.mFS.Partition, curb);
                    curb = b;
                }
                else
                {
                    curb.ContentSize = (uint)(Data.Length % arr.Length);
                    NoobFSBlock.Write(NoobFileSystem.mFS.Partition, curb);
                }
            }
            while (index != Data.Length);
            EditEntryInfo(EntryInfoPosition.DateTimeModified, System.Environment.DateTime.Now.TimeStamp);
            EditEntryInfo(EntryInfoPosition.DateTimeLastAccess, System.Environment.DateTime.Now.TimeStamp);
        }

        /// <summary>
        /// Writes all the specified text into the file
        /// </summary>
        /// <param name="text">The string to write into file</param>
        public void WriteAllText(String text)
        {
            Byte[] b = new Byte[text.Length];
            DataUtils.CopyCharToByte(text.ToCharArray(), 0, b, 0, text.Length);
            WriteAllBytes(b);
        }

        /// <summary>
        /// Return's all the bytes contained in the file
        /// </summary>
        public Byte[] ReadAllBytes()
        {
            if (_StartBlock.NextBlock == 0)
            {
                return new Byte[0];
            }
            NoobFSBlock b = _StartBlock;
            List<Byte> lret = new List<Byte>();
            while (b.NextBlock != 0)
            {
                b = NoobFSBlock.Read(b.Partition, b.NextBlock);
                for (int i = 0; i < b.ContentSize; i++)
                {
                    lret.Add(b.Content[i]);
                }
            }
            EditEntryInfo(EntryInfoPosition.DateTimeLastAccess, System.Environment.DateTime.Now.TimeStamp);
            return lret.ToArray();
        }

        /// <summary>
        /// Return's all the text contained in the file
        /// </summary>
        public string ReadAllText()
        {
            Byte[] b = ReadAllBytes();
            Char[] text = new Char[b.Length];
            DataUtils.CopyByteToChar(b, 0, text, 0, b.Length);
            return DataUtils.CharToString(text);
        }

        /// <summary>
        /// Creates a new NoobFile Object
        /// </summary>
        /// <param name="p">The partition to use</param>
        /// <param name="bn">The block number we want to use</param>
        /// <param name="pa">The path of the new directory</param>
        public NoobFile(Partition p, ulong bn,String pa) 
        {
            _Path = pa;
            part = p;
            _StartBlock = NoobFSBlock.Read(p, bn);
            if (!_StartBlock.Used)
            {
                _StartBlock.Used = true;
                String n = "New File";
                CreateEntry(part, _StartBlock, n);
            }
        }

        /// <summary>
        /// Overrides the ToString Method.
        /// </summary>
        public override String ToString()
        {
            return this.Name;
        }
    }
}
