using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medli.FileSystem.Physical.Drivers
{
    class AtapioHandler
    {
        protected UInt64 mBlockCount = 0;
        public UInt64 BlockCount
        {
            get { return mBlockCount; }
        }

        protected UInt64 mBlockSize = 512;
        public UInt64 BlockSize
        {
            get { return mBlockSize; }
        }

        protected string mSerialNo;
        public string SerialNo
        {
            get { return mSerialNo; }
        }

        protected string mFirmwareRev;
        public string FirmwareRev
        {
            get { return mFirmwareRev; }
        }

        protected string mModelNo;
        public string ModelNo
        {
            get { return mModelNo; }
        }


        private ATABus UsedBus = null;

        private AtapioMode Mode;

        private bool currentDevice = false; // false = Master, true = slave

        public AtapioHandler(int BusToUse)
        {
            UsedBus = ATABus.Busses[BusToUse];
            if (GetStatus() == (Statuses)0xFF)
            {
                UsedBus = null;
                return;
            }
            UsedBus.Control.Byte = (byte)Controlles.nIEN;
            Mode = AtapioMode.LBA48;
            Identify();
        }

        private void SelectDrive(bool slave, byte aLbaHigh4)
        {
            //Switch for modes
            if (Mode == AtapioMode.CHS)
            {
                UsedBus.DeviceSelect.Byte = (byte)(((slave) ? 0x0A : 0x0B) | ((slave) ? (byte)(DeviceSelectionValues.Slave) : 0) | aLbaHigh4);
                currentDevice = slave;
            }
            else 
            if(Mode == AtapioMode.LBA28)
            {
                UsedBus.DeviceSelect.Byte = (byte)(((slave) ? 0x0E : 0x0F) | ((slave) ? (byte)(DeviceSelectionValues.Slave) : 0) | ((aLbaHigh4 >> 24) & 0x0F));
                currentDevice = slave;
            }
            else 
            {
                UsedBus.DeviceSelect.Byte = (byte)(((slave) ? 0x40 : 0x50) | ((slave) ? (byte)(DeviceSelectionValues.Slave) : 0));
                currentDevice = slave;
            }
            //UsedBus.DeviceSelect.Byte = (byte)((byte)(currentDevice == 0x0A ? 0x0B : 0x0A) | (byte)DeviceSelectionValues.LBA | ((slave) ? (byte)(DeviceSelectionValues.Slave) : 0) | aLbaHigh4);
        }

        private Statuses GetStatus()
        {
            Byte x = UsedBus.Status.Byte;
            x = UsedBus.Status.Byte;
            x = UsedBus.Status.Byte;
            x = UsedBus.Status.Byte;
            return (Statuses)UsedBus.Status.Byte;
        }

        private string GetString(UInt16[] aBuffer, int aIndexStart, int aStringLength)
        {
            Char[] xChars = new char[aStringLength];
            for (int i = 0; i < aStringLength / 2; i++)
            {
                UInt16 xChar = aBuffer[aIndexStart + i];
                xChars[i * 2] = (char)(xChar >> 8);
                xChars[i * 2 + 1] = (char)xChar;
            }
            return new string(xChars);
        }

        private void Identify()
        {
            SelectDrive(false,0);
            UsedBus.LBA3.Byte = 0x00;
            UsedBus.LBA1.Byte = 0x00;
            UsedBus.LBA2.Byte = 0x00;
            SendCommand(Commands.Identify);
            Statuses status;
            if (UsedBus != null)
            {
                if (UsedBus.LBA2.Byte != 0x00 || UsedBus.LBA3.Byte != 0x00)
                {
                    //Device is not ATA
                    if (UsedBus.LBA2.Byte == 0x14 && UsedBus.LBA3.Byte == 0xEB)
                    {
                    }
                    UsedBus = null;
                }
            }
            if(UsedBus != null) 
            {
                do
                {
                    status = GetStatus();
                    if ((status & Statuses.ERR) != 0)
                    {
                        UsedBus = null;
                        break;
                    }
                } while ((status & Statuses.DRQ) == 0);
            }
            if (UsedBus != null)
            {
                UInt16[] Buffer = new UInt16[256];
                UsedBus.Data.Read16(Buffer);
                mSerialNo = GetString(Buffer, 10, 20);
                Console.WriteLine(mSerialNo);
                mFirmwareRev = GetString(Buffer, 23, 8);
                mModelNo = GetString(Buffer, 27, 40);

                mBlockCount = ((UInt32)Buffer[61] << 16 | Buffer[60]) - 1;

                bool xLba48Capable = (Buffer[83] & 0x400) != 0;
                if (xLba48Capable)
                {
                    mBlockCount = ((UInt64)Buffer[102] << 32 | (UInt64)Buffer[101] << 16 | (UInt64)Buffer[100]) - 1;
                    Mode = AtapioMode.LBA48;
                }
            }
        }

        protected void SelectSector(UInt64 aSectorNo, UInt32 aSectorCount)
        {
            SelectDrive(currentDevice,(byte)(aSectorNo >> 24));

            if (Mode == AtapioMode.LBA28)
            {
                UsedBus.SectorCount.Byte = (byte)aSectorCount;
                UsedBus.LBA1.Byte = (byte)(aSectorNo & 0xFF);
                UsedBus.LBA2.Byte = (byte)((aSectorNo & 0xFF00) >> 8);
                UsedBus.LBA3.Byte = (byte)((aSectorNo & 0xFF0000) >> 16);
            }
            else
            {
                UsedBus.SectorCount.Byte = (byte)(aSectorCount>>8);
                UsedBus.LBA4.Byte = (byte)((aSectorNo & 0xFF000000) >> 24);
                UsedBus.LBA5.Byte = (byte)((aSectorNo & 0xFF00000000) >> 32);
                UsedBus.LBA6.Byte = (byte)((aSectorNo & 0xFF0000000000) >> 40);
                //
                UsedBus.SectorCount.Byte = (byte)aSectorCount;
                UsedBus.LBA1.Byte = (byte)(aSectorNo & 0xFF);
                UsedBus.LBA2.Byte = (byte)((aSectorNo & 0xFF00) >> 8);
                UsedBus.LBA3.Byte = (byte)((aSectorNo & 0xFF0000) >> 16);
            }
        }

        public void ReadBlock(UInt64 aBlockNo, UInt32 aBlockCount, byte[] aData)
        {
            //CheckDataSize(aData, aBlockCount);
            SelectSector(aBlockNo, aBlockCount);
            if (Mode == AtapioMode.LBA28)
            {
                SendCommand(Commands.ReadSec);
            }
            else
            {
                SendCommand(Commands.ReadSecExt);
            }
            UsedBus.Data.Read8(aData);
        }

        public void WriteBlock(UInt64 aBlockNo, UInt32 aBlockCount, byte[] aData)
        {
            SelectSector(aBlockNo, 1);
            if (Mode == AtapioMode.LBA28)
            {
                SendCommand(Commands.WriteSec);
            }
            else
            {
                SendCommand(Commands.WriteSecExt);
            }

            UInt16 xValue;
            for (int i = 0; i < aData.Length / 2; i++)
            {
                xValue = (UInt16)((aData[i * 2 + 1] << 8) | aData[i * 2]);
                UsedBus.Data.Word = xValue;
            }

            SendCommand(Commands.CacheFlush);
        }

        private void SendCommand(Commands command)
        {
            UsedBus.Command.Byte = (byte)command;
            Statuses status;
            do
            {
                status = GetStatus();
                if (status == 0x00)
                {
                    UsedBus = null;
                    return;
                }
            } while ((status & Statuses.BSY) != 0);
            if ((status & Statuses.ERR) != 0)
            {
                throw new Exception("Ata error");
            }
        }
    }
}
