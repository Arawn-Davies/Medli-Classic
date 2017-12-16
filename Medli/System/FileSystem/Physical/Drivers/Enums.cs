using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medli.FileSystem.Physical.Drivers
{
    [Flags]
    enum Statuses : byte
    {
        ERR = 0x01,
        DRQ = 0x08,
        SRV = 0x10,
        DF = 0x20,
        RDY = 0x40,
        BSY = 0x80
    }

    enum Controlles : byte
    {
        nIEN = 0x02,
        SRST = 0x04,
        HOB = 0x80
    }

    enum Commands : byte
    {
        Format = 0x50,
        ReadSec = 0x20,
        ReadSecNo = 0x21,
        ReadL = 0x22,
        ReadLNo = 0x23,
        ReadSecExt = 0x24,
        WriteSec = 0x30,
        WriteSecNo = 0x31,
        WriteL = 0x32,
        WriteLNo = 0x33,
        WriteSecExt = 0x34,
        Packet = 0xA0,
        PacketIdentify = 0xA1,
        CacheFlush = 0xE7,
        CacheFlushExt = 0xEA,
        Identify = 0xEC
    }

    enum ATAPICommands : byte
    {
        Test = 0x00,
        RequestSense = 0x03,
        Format = 0x04,
        Inquiry = 0x12,
        StartStopUnit = 0x1B,
        PreventAllowMediumRemoval = 0x1E,
        ReadFormatCapacities = 0x23,
        ReadCapacity = 0x25,
        Read = 0x28,
        Write = 0x2A,
        Seek = 0x2B,
        WriteAndVerify = 0x2F,
        SyncronizeCache = 0x35,
        WriteBuffer = 0x3B,
        ReadBuffer = 0x3C,
        ReadTocPmaAtip = 0x43,
        GetConfig = 0x46,
        GetEventStat = 0x4A,
        ReadDiskInfo = 0x51,
        ReadTrackInfo = 0x52,
        ReserveTrack = 0x53,
        SendOPCInfo = 0x54,
        ModeSel = 0x55,
        RepairTrack = 0x58,
        ModeSense = 0x5A,
        CloseTrackSession = 0x5B,
        ReadBufferCapacity = 0x5C,
        SendCueSheet = 0x5D,
        ReportLuns = 0xA0,
        Blank = 0xA1,
        SecurityControlIn = 0xA2,
        SendKey = 0xA3,
        ReportKey = 0xA4,
        ToggleLoadMedium = 0xA6,
        SetReadAHead = 0xA7,
        Read2 = 0xA8,
        Write2 = 0xAA,
        GetPerformance = 0xAC,
        ReadDiscStruct = 0xAD,
        SecurityProtocolOut = 0xB5,
        SetStreaming = 0xB6,
        ReadCDSpeed = 0xBB,
        MechStatus = 0xBD,
        ReadCD = 0xBE,
        SendDiskStructure = 0xBF
    }

    [Flags]
    enum DeviceSelectionValues : byte
    {
        Slave = 0x10,
        LBA = 0x40,
        Default = 0xA0
    };

    enum AtapioMode
    {
        CHS = 0x00,
        LBA28 = 0x01,
        LBA48 = 0x02
    }
}
