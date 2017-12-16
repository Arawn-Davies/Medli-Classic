using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Core;

namespace Medli.FileSystem.Physical.Drivers
{
    class ATABus
    {
        public static ATABus[] Busses
        {
            get
            {
                List<ATABus> l = new List<ATABus>();
                l.Add(new ATABus(0x1F0, 0x3F6));
                l.Add(new ATABus(0x170, 0x376));
                return l.ToArray();
            }
        }

        private UInt16 _BusPort;
        private UInt16 _ControllerPort;

        //Private Registers
        private IOPort _Data;
        private IOPortRead _Error;
        private IOPortWrite _Feature;
        private IOPortWrite _SectorCount;
        private IOPortWrite _SectorNumber;
        private IOPort _LBA1;
        private IOPort _LBA2;
        private IOPort _LBA3;
        private IOPort _LBA4;
        private IOPort _LBA5;
        private IOPort _LBA6;
        private IOPortWrite _DeviceSelect;
        private IOPortWrite _Command;
        private IOPortRead _Status;

        //Private Controller
        private IOPortWrite _Control;

        //Public Registers properties
        public IOPort Data
        {
            get
            {
                return _Data;
            }
        }
        public IOPortRead Error
        {
            get
            {
                return _Error;
            }
        }
        public IOPortWrite Feature
        {
            get
            {
                return _Feature;
            }
        }
        public IOPortWrite SectorCount
        {
            get
            {
                return _SectorCount;
            }
        }
        public IOPortWrite SectorNumber
        {
            get
            {
                return _SectorNumber;
            }
        }
        public IOPort LBA1
        {
            get
            {
                return _LBA1;
            }
        }
        public IOPort LBA2
        {
            get
            {
                return _LBA2;
            }
        }
        public IOPort LBA3
        {
            get
            {
                return _LBA3;
            }
        }
        public IOPort LBA4
        {
            get
            {
                return _LBA4;
            }
        }
        public IOPort LBA5
        {
            get
            {
                return _LBA5;
            }
        }
        public IOPort LBA6
        {
            get
            {
                return _LBA6;
            }
        }

        public IOPortWrite DeviceSelect
        {
            get
            {
                return _DeviceSelect;
            }
        }
        public IOPortWrite Command
        {
            get
            {
                return _Command;
            }
        }
        public IOPortRead Status
        {
            get
            {
                return _Status;
            }
        }

        //Public Controller register
        public IOPortWrite Control
        {
            get
            {
                return _Control;
            }
        }

        public int BusPort
        {
            get
            {
                return _BusPort;
            }
        }

        public int ControllerPort
        {
            get
            {
                return _ControllerPort;
            }
        }

        public ATABus(UInt16 bp, UInt16 cp)
        {
            _BusPort = bp;
            _ControllerPort = cp;
            Init();
        }

        private void Init()
        {
            //Init Registers
            _Data = new IOPort(_BusPort);
            _Error = new IOPortRead(_BusPort, 1);
            _Feature = new IOPortWrite(_BusPort, 1);
            _SectorCount = new IOPortWrite(_BusPort, 2);
            _SectorNumber = new IOPortWrite(_BusPort, 3);
            _LBA1 = new IOPort(_BusPort, 3);
            _LBA2 = new IOPort(_BusPort, 4);
            _LBA3 = new IOPort(_BusPort, 5);
            _LBA4 = new IOPort(_BusPort, 3);
            _LBA5 = new IOPort(_BusPort, 4);
            _LBA6 = new IOPort(_BusPort, 5);
            _Command = new IOPortWrite(_BusPort, 7);
            _Status = new IOPortRead(_BusPort, 7);
            _DeviceSelect = new IOPortWrite(_BusPort, 6);
            _Control = new IOPortWrite(_ControllerPort, 2);
        }
    }
}
