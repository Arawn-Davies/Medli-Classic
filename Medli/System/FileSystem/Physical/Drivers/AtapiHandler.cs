using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Core;

namespace Medli.FileSystem.Physical.Drivers
{
    class AtapiHandler
    {
        protected UInt64 mBlockCount = 0;
        public UInt64 BlockCount
        {
            get { return mBlockCount; }
        }

        protected UInt64 mBlockSize = 2048;
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

        private bool currentDevice = false; // false = Master, true = slave

        public AtapiHandler(int BusToUse)
        {
            UsedBus = ATABus.Busses[BusToUse];
            if (GetStatus() == (Statuses)0xFF)
            {
                UsedBus = null;
                return;
            }
            UsedBus.Control.Byte = (byte)Controlles.nIEN;
            Identify();
        }

        private void SelectDrive(bool slave, byte aLbaHigh4)
        {
            UsedBus.DeviceSelect.Byte = (byte)((slave) ? (byte)(DeviceSelectionValues.Slave) : 0);
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
            //SendCommand(Commands.Packet);
            Statuses s = GetStatus();
            SendCommand(Commands.PacketIdentify);
            Statuses status;
            if (UsedBus != null)
            {
                if (UsedBus.LBA2.Byte != 0x00 || UsedBus.LBA3.Byte != 0x00)
                {
                    //Device is not ATA
                    if (UsedBus.LBA2.Byte == 0x14 && UsedBus.LBA3.Byte == 0xEB)
                    {
                    }
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
                mFirmwareRev = GetString(Buffer, 23, 8);
                mModelNo = GetString(Buffer, 27, 40);

                mBlockCount = ((UInt32)Buffer[61] << 16 | Buffer[60]) - 1;
            }
        }

        public void ReadBlock(UInt64 aBlockNo, UInt32 aBlockCount, byte[] aData)
        {
            //Partially working. When i read data it fails but in the ATA registers the DRQ bit is set to 1. so the device knows that data must be transfered!. 
            //Maybe the problem is in the Interupt management (still don't know ho to do it)
            Statuses s = GetStatus();
            SelectDrive(currentDevice, 0);
            s = GetStatus();
            UsedBus.Feature.Byte = 0x00;
            UsedBus.LBA2.Byte = (byte)((mBlockSize/2) & 0xFF);
            UsedBus.LBA3.Byte = (byte)((mBlockSize/2) >> 8);
            SendCommand(Commands.Packet);
            Byte[] cmd = new Byte[] { (byte)ATAPICommands.Read2, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            cmd[9] = (byte)aBlockCount;
            cmd[2] = (byte)((aBlockNo & 0xFF000000)>> 24);
            cmd[3] = (byte)((aBlockNo & 0xFF0000) >> 16);
            cmd[4] = (byte)((aBlockNo & 0xFF00) >> 8);
            cmd[5] = (byte)(aBlockNo & 0xFF);
            for (int i = 0; i < cmd.Length/2; i+=2)
            {
                UsedBus.Data.Word = (byte)(cmd[i] << 8 | cmd[i+1]);
            }
            //Aurora: WON'T WORK - interrupts are locked by cosmos anyway.
            Cosmos.Core.INTs.IRQContext c = new Cosmos.Core.INTs.IRQContext();
            INTs.HandleInterrupt_2E(ref c);
            INTs.HandleInterrupt_2F(ref c);
            uint xD = c.Interrupt;
            s = GetStatus();
            s = GetStatus();
            s = GetStatus();
            uint size = (uint)(UsedBus.LBA3.Byte << 8 | UsedBus.LBA2.Byte);
            UsedBus.Data.Read8(aData);
            c = new Cosmos.Core.INTs.IRQContext();
            INTs.HandleInterrupt_2E(ref c);
            INTs.HandleInterrupt_2F(ref c);
            xD = c.Interrupt;
        }

        public void WriteBlock(UInt64 aBlockNo, UInt32 aBlockCount, byte[] aData)
        {
            SendCommand(Commands.WriteSec);
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
