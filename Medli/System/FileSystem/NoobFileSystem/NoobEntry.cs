/* NoobEntry.cs - the base for defining file system entries.
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
using System.ComponentModel;
using Medli.System;

namespace Medli.FileSystem.NoobFileSystem
{
    enum EntryInfoPosition
    {
        DateTimeCreated = 0x00, //+8
        DateTimeModified = 0x08, //+8
        DateTimeLastAccess = 0x10, //+8
        Owner = 0x18, //+8
        Group = 0x20, //+8
        Visible = 0x28, //+1
        Writable = 0x29, //+1
        OwnerPermissions = 0x30, //Could be set better
        GroupPermissions = 0x3a, //Could be set better
        EveryonePermissions = 0x3b //Could be set better
        //Space for Something else 
    }

    class NoobEntry
    {
        protected static Char[] InvalidChars = new Char[] { '/', '?', '*' };

        protected NoobFSBlock _StartBlock;
        protected Partition part;
        protected String _Path;

        private static int MaxNameSize = 255;

        private int CustomSize = (int)(NoobFSBlock.MaxBlockContentSize - MaxNameSize);

        /// <summary>
        /// The StartBlock of the current NoobDirectory
        /// </summary>
        public NoobFSBlock StartBlock
        {
            get
            {
                return _StartBlock;
            }
        }

        /// <summary>
        /// The Path of the Current NoobDirectory
        /// </summary>
        public String Path
        {
            get
            {
                return _Path;
            }
        }

        /// <summary>
        /// The Name of the Current NoobDirectory
        /// </summary>
        public String Name
        {
            get
            {
                Byte[] arr = _StartBlock.Content;
                String str = "";
                for (int i = 0; i < MaxNameSize; i++)
                {
                    if (arr[i] == 0)
                        break;
                    str += ((Char)arr[i]).ToString();
                }
                return str;
            }
        }

        /// <summary>
        /// Permits to edit an entryInfo
        /// </summary>
        /// <param name="pos">The EntryInfo to edit</param>
        /// <param name="value">The new value</param>
        public void EditEntryInfo(EntryInfoPosition pos, long value)
        {
            if (pos < EntryInfoPosition.Visible)
            {
                DataUtils.CopyByteToByte(BitConverter.GetBytes(value), 0, _StartBlock.Content, (int)MaxNameSize + (int)pos, 8, false);
            }
            else
            {
                DataUtils.CopyByteToByte(BitConverter.GetBytes(value), 0, _StartBlock.Content, (int)MaxNameSize + (int)pos, 1, false);
            }
        }

        /// <summary>
        /// Creates a new NoobEntry in the current directory. Uses a random Block.
        /// </summary>
        /// <param name="p">The partition where to create the new NoobEntry</param>
        /// <param name="name">The new NoobEntry's name</param>
        protected static NoobFSBlock CreateEntry(Partition p, String name)
        {
            return CreateEntry(p, NoobFSBlock.GetFreeBlock(p), name);
        }

        /// <summary>
        /// Creates a new NoobEntry in the current directory
        /// </summary>
        /// <param name="p">The partition where to create the new NoobEntry</param>
        /// <param name="b">The block to write on</param>
        /// <param name="name">The new NoobEntry's name</param>
        protected static NoobFSBlock CreateEntry(Partition p, NoobFSBlock b, String n)
        {
            if (b != null && ((!DataUtils.StringContains(n, InvalidChars)) || b.BlockNumber == 0))
            {
                b.Used = true;
                b.NextBlock = 0;
                b.TotalSize = 0;
                Char[] nm = n.ToCharArray();
                for (int i = 0; i < nm.Length; i++)
                {
                    b.Content[i] = (byte)nm[i];
                }
                if (b.BlockNumber != 0)
                {
                    DataUtils.CopyByteToByte(BitConverter.GetBytes(System.Environment.DateTime.Now.TimeStamp), 0, b.Content, (int)MaxNameSize + (int)EntryInfoPosition.DateTimeCreated, 8, false);
                }
                b.Content[nm.Length] = 0;
                b.ContentSize = (uint)nm.Length;
                NoobFSBlock.Write(p, b);
                return b;
            }
            return null;
        }
    }
}
