using System;
using System.Collections.Generic;
using System.Text;
using AIC_Framework.IO;
using System.IO;

namespace Medli.System
{
    class Serial
    {
        public const ushort COM1 = 0x3f8;
        public const ushort COM2 = 0x2F8;
        public const ushort COM3 = 0x3E8;
        public const ushort COM4 = 0x2E8;

        public const ushort PORT = COM1; //COM1 address

        public static void InitializeSerial()
        {
            PortIO.outb(PORT + 1, 0x00);
            PortIO.outb(PORT + 3, 0x80);
            PortIO.outb(PORT + 0, 0x03);
            PortIO.outb(PORT + 1, 0x00);
            PortIO.outb(PORT + 3, 0x03);
            PortIO.outb(PORT + 2, 0xC7);
            PortIO.outb(PORT + 4, 0x0B);
        }
        public int Serial_received()
        {
            return PortIO.inb(PORT + 5) & 1;
        }

        public char Read_serial()
        {
            while (Serial_received() == 0) ;

            return (char) PortIO.inb(PORT);
        }

        public static int EmptyTransmitCheck()
        {
            return PortIO.inb(PORT + 5) & 0x20;
        }

        public static void Write_serial(char a)
        {
            while (EmptyTransmitCheck() == 0) ;
            PortIO.outb(PORT, (byte) a);
        }
        public static void Serial_WriteString(string text)
        {
            while (text != null)
            {
                foreach (char a in text)
                {
                    Write_serial(a);
                }
            }
            //while (a != null)
            //{
            //    write_serial(a);
            //    a = a + 1;
            //}
        }
    }
}
