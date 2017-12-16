/* NoobDirectory.cs - the class for defining a directory in NoobOS
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
    class NoobDirectory : NoobEntry
    {
        /// <summary>
        /// The FullName (Path+Name) of the Current NoobDirectory
        /// </summary>
        public String FullName
        {
            get
            {
                return NoobFileSystem.CombineDir(_Path, Name);
            }
        }

        /// <summary>
        /// Creates a new NoobDirectory Object
        /// </summary>
        /// <param name="p">The partition to use</param>
        /// <param name="bn">The block number we want to use</param>
        /// <param name="pa">The path of the new directory</param>
        public NoobDirectory(Partition p, ulong bn, String pa)
        {
            _Path = pa;
            part = p;
            _StartBlock = NoobFSBlock.Read(p, bn);
            if (bn == 1 && pa == "/" && _StartBlock.Content[0] != '/')
            {
                Char[] nm = "/".ToCharArray();
                for (int i = 0; i < nm.Length; i++)
                {
                    _StartBlock.Content[i] = (byte)nm[i];
                }
                _StartBlock.Used = true;
                _StartBlock.NextBlock = 0;
                NoobFSBlock.Write(p, _StartBlock);
            }
            if (!_StartBlock.Used)
            {
                _StartBlock.Used = true;
                String n = "New Directory";
                if (pa == NoobFileSystem.separator)
                {
                    _Path = "";
                    n = pa;
                }
                CreateEntry(part, _StartBlock, n);
            }
        }

        /// <summary>
        /// Gets All NoobDirectories Contained in this one
        /// </summary>
        public NoobDirectory[] GetDirs()
        {
            NoobFSBlock curb = _StartBlock;
            List<NoobDirectory> d = new List<NoobDirectory>();
            while (curb.NextBlock != 0)
            {
                int index = 0;
                curb = NoobFSBlock.Read(_StartBlock.Partition, _StartBlock.NextBlock);
                while (index < curb.ContentSize)
                {
                    ulong a = BitConverter.ToUInt64(curb.Content, index);
                    index += 8;
                    uint sep = BitConverter.ToUInt32(curb.Content, index);
                    index += 4;
                    if (sep == 1)
                    {
                        d.Add(new NoobDirectory(part, a, NoobFileSystem.CombineDir(_Path, Name)));
                    }
                }
            }
            return d.ToArray();
        }

        /// <summary>
        /// Gets All NoobFiles contained in this NoobDirectory
        /// </summary>
        public NoobFile[] GetFiles()
        {
            NoobFSBlock curb = _StartBlock;
            List<NoobFile> d = new List<NoobFile>();
            while (curb.NextBlock != 0)
            {
                int index = 0;
                curb = NoobFSBlock.Read(_StartBlock.Partition, _StartBlock.NextBlock);
                while (index < curb.ContentSize)
                {
                    ulong a = BitConverter.ToUInt64(curb.Content, index);
                    index += 8;
                    uint sep = BitConverter.ToUInt32(curb.Content, index);
                    index += 4;
                    if (sep == 2)
                    {
                        d.Add(new NoobFile(part, a, NoobFileSystem.CombineDir(_Path, Name)));
                    }
                }
            }
            return d.ToArray();
        }

        /// <summary>
        /// Gets all NoobEntries contained in this NoobDirectory
        /// </summary>
        public NoobEntry[] GetEntries()
        {
            NoobFSBlock curb = _StartBlock;
            List<NoobEntry> d = new List<NoobEntry>();
            while (curb.NextBlock != 0)
            {
                int index = 0;
                curb = NoobFSBlock.Read(_StartBlock.Partition, _StartBlock.NextBlock);
                while (index < curb.ContentSize)
                {
                    ulong a = BitConverter.ToUInt64(curb.Content, index);
                    index += 8;
                    uint sep = BitConverter.ToUInt32(curb.Content, index);
                    index += 4;
                    if (sep == 1)
                    {
                        d.Add(new NoobDirectory(part, a, NoobFileSystem.CombineDir(_Path, Name)));
                    }
                    else if(sep == 2)
                    {
                        d.Add(new NoobFile(part, a, NoobFileSystem.CombineDir(_Path, Name)));
                    }
                }
            }
            return d.ToArray();
        }

        /// <summary>
        /// Adds a new NoobDirectory to the current directory
        /// </summary>
        /// <param name="Name">The new NoobDirectory's name</param>
        public void AddDirectory(String Name) 
        {
            NoobEntry[] dirs = GetEntries();
            for (int i = 0; i < dirs.Length; i++)
            {
                if (dirs[i].Name == Name)
                {
                    ExConsole.WriteLine("Entry with same Name already exists!");
                    return;
                }
            }
            NoobFSBlock curb = GetBlockToEdit();
            NoobFSBlock newdirb = CreateEntry(part, Name);
            //if (newdirb != null)
            //{
                BitConverter.GetBytes(newdirb.BlockNumber).CopyTo(curb.Content, curb.ContentSize);
                BitConverter.GetBytes((uint)1).CopyTo(curb.Content, curb.ContentSize + 8);
                curb.ContentSize += 12;
                NoobFSBlock.Write(part, curb);
                EditEntryInfo(EntryInfoPosition.DateTimeModified, System.Environment.DateTime.Now.TimeStamp);
                EditEntryInfo(EntryInfoPosition.DateTimeLastAccess, System.Environment.DateTime.Now.TimeStamp);
            //}
        }

        /// <summary>
        /// Creates a new NoobFile to the current directory
        /// </summary>
        /// <param name="Name">The new NoobFile's name</param>
        public void AddFile(String Name)
        {
            NoobEntry[] dirs = GetEntries();
            for (int i = 0; i < dirs.Length; i++)
            {
                if (dirs[i].Name == Name)
                {
                    Console.WriteLine("Entry with same Name already exists!");
                    return;
                }
            }
            NoobFSBlock curb = GetBlockToEdit();
            NoobFSBlock newfileb = CreateEntry(part, Name);
            //if (newfileb != null)
            //{
                BitConverter.GetBytes(newfileb.BlockNumber).CopyTo(curb.Content, curb.ContentSize);
                BitConverter.GetBytes((uint)2).CopyTo(curb.Content, curb.ContentSize + 8);
                curb.ContentSize += 12;
                NoobFSBlock.Write(part, curb);
                EditEntryInfo(EntryInfoPosition.DateTimeModified, System.Environment.DateTime.Now.TimeStamp);
                EditEntryInfo(EntryInfoPosition.DateTimeLastAccess, System.Environment.DateTime.Now.TimeStamp);
            //}
        }

        /// <summary>
        /// Permits to remove a NoobDirectory by passing it's name
        /// </summary>
        /// <param name="Name">The NoobDirectory's name to remove</param>
        public void RemoveDirectory(String Name)
        {
            NoobDirectory[] dirs = GetDirs();
            bool found = false;
            int index = 0;
            for (int i = 0; i < dirs.Length; i++)
            {
                if (dirs[i].Name == Name)
                {
                    index = i;
                    found = true;
                    break;
                }
            }
            if (found)
            {
                RemoveDirectory(dirs[index]);
            }
        }

        /// <summary>
        /// Permits to remove a NoobDirectory by passing it
        /// </summary>
        /// <param name="noobDirectory">The NoobDirectory to remove</param>
        private void RemoveDirectory(NoobDirectory noobDirectory)
        {
            NoobDirectory[] subdirs = noobDirectory.GetDirs();
            for (int i = 0; i < subdirs.Length; i++)
            {
                noobDirectory.RemoveDirectory(subdirs[i]);
            }
            NoobFile[] subfiles = noobDirectory.GetFiles();
            for (int i = 0; i < subdirs.Length; i++)
            {
                noobDirectory.RemoveFile(subfiles[i].Name);
            }
            NoobFileSystem.ClearBlocks(noobDirectory.StartBlock);
            DeleteBlock(noobDirectory.StartBlock);
        }

        /// <summary>
        /// Permits to remove a NoobFile by passing it's name
        /// </summary>
        /// <param name="Name">The NoobFile's name to remove</param>
        public void RemoveFile(String Name) 
        {
            NoobFile[] files = GetFiles();
            bool found = false;
            int index = 0;
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name == Name)
                {
                    index = i;
                    found = true;
                    break;
                }
            }
            if (found)
            {
                NoobFileSystem.ClearBlocks(files[index].StartBlock);
                DeleteBlock(files[index].StartBlock);
            }
        }

        /// <summary>
        /// Permits to remove a NoobFSBlock by passing it
        /// </summary>
        /// <param name="noobDirectory">The NoobFSBlock to remove</param>
        private void DeleteBlock(NoobFSBlock noobFSBlock)
        {
            NoobFSBlock curb = _StartBlock;
            while (curb.NextBlock != 0)
            {
                int index = 0;
                bool found = false;
                List<Byte> cont = new List<Byte>();
                curb = NoobFSBlock.Read(_StartBlock.Partition, _StartBlock.NextBlock);
                while (index < curb.ContentSize)
                {
                    ulong a = BitConverter.ToUInt64(curb.Content, index);
                    Byte[] app = BitConverter.GetBytes(a);
                    for (int i = 0; i < app.Length; i++)
                    {
                        cont.Add(app[i]);
                    }
                    index += 8;
                    uint sep = BitConverter.ToUInt32(curb.Content, index);
                    index += 4;
                    if (a == noobFSBlock.BlockNumber)
                    {
                        app = BitConverter.GetBytes((uint)0);
                        for (int i = 0; i < app.Length; i++)
                        {
                            cont.Add(app[i]);
                        }
                        found = true;
                    }
                    else
                    {
                        app = BitConverter.GetBytes(sep);
                        for (int i = 0; i < app.Length; i++)
                        {
                            cont.Add(app[i]);
                        }
                    }
                }
                if (found)
                {
                    curb.Content = cont.ToArray();
                    curb.ContentSize = (uint)cont.Count;
                    NoobFSBlock.Write(part, curb);
                }
            }
        }

        /// <summary>
        /// Gets the last NoobFSBlock of the directory
        /// </summary>
        private NoobFSBlock GetBlockToEdit()
        {
            NoobFSBlock ret = _StartBlock;
            while (ret.NextBlock != 0)
            {
                ret = NoobFSBlock.Read(_StartBlock.Partition, _StartBlock.NextBlock);
            }
            if (ret.BlockNumber == _StartBlock.BlockNumber)
            {
                ret = NoobFSBlock.GetFreeBlock(part);
                ret.Used = true;
                ret.ContentSize = 0;
                ret.NextBlock = 0;
                _StartBlock.NextBlock = ret.BlockNumber;
                NoobFSBlock.Write(part, _StartBlock);
                NoobFSBlock.Write(part, ret);
            }
            if (part.NewBlockArray(1).Length - ret.ContentSize < 12)
            {
                NoobFSBlock b = NoobFSBlock.GetFreeBlock(part);
                if (b == null)
                {
                    return null;
                }
                ret.NextBlock = b.BlockNumber;
                NoobFSBlock.Write(part, ret);
                b.Used = true;
                ret = b;
            }
            return ret;
        }

        /// <summary>
        /// Get the directory specified by the Fullname passed
        /// </summary>
        /// <param name="fn">The fullname of the directory</param>
        public static NoobDirectory GetDirectoryByFullName(String fn)
        {
            NoobDirectory d = NoobFileSystem.mFS.Root;
            if (fn == d.Name)
            {
                return d;
            }
            if (fn == null || fn == "")
            {
                return null;
            }
            String[] names = fn.Split('/');
            if (names[0] != "")
            {
                return null;
            }
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i] != null && names[i] != "")
                {
                    d = d.GetDirectoryByName(names[i]);
                    if (d == null)
                    {
                        break;
                    }
                }
            }
            return d;
        }

        /// <summary>
        /// Get the directory specified by the Name passed
        /// </summary>
        /// <param name="n">The name of the child directory</param>
        public NoobDirectory GetDirectoryByName(String n)
        {
            NoobDirectory[] dirs = GetDirs();
            for (int i = 0; i < dirs.Length; i++)
            {
                if (dirs[i].Name == n)
                {
                    return dirs[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Overrides the ToString Method.
        /// </summary>
        public override String ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Get the directory specified by the Fullname passed
        /// </summary>
        /// <param name="fn">The fullname of the directory</param>
        public static NoobFile GetFileByFullName(String fn)
        {
            NoobDirectory d = new NoobDirectory(NoobFileSystem.mFS.Partition, 1, NoobFileSystem.separator);
            if (fn == null || fn == "")
            {
                return null;
            }
            String[] names = fn.Split('/');
            for (int i = 0; i < names.Length - 1; i++)
            {
                if (names[i] != "")
                {
                    d = d.GetDirectoryByName(names[i]);
                    if (d == null)
                    {
                        break;
                    }
                }
            }
            return d.GetFileByName(names[names.Length-1]);
        }

        /// <summary>
        /// Get the file specified by the Name passed
        /// </summary>
        /// <param name="n">The name of the child file</param>
        public NoobFile GetFileByName(String n)
        {
            NoobFile[] files = GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name == n)
                {
                    return files[i];
                }
            }
            return null;
        }
    }
}
