﻿using System;
using System.Collections.Generic;
using System.Text;
using Medli.System;

namespace Medli.SysInternal
{
    public class MedliTime
    {
        public static void printTime()
        {
            Console.WriteLine("The current time is HH:{0} MM:{1} SS:{2}", SysClock.Hour(), SysClock.Minute(), SysClock.Second());
        }
        public static void printDate()
        {
            Console.WriteLine("The current date is {0}/{1}/{2}", SysClock.DayOfTheMonth(), SysClock.Month(), SysClock.Year());
        }
        public static int Second()
        {
            return SysClock.Second();
        }
        public static int Minute()
        {
            return SysClock.Minute();
        }
        public static int Hour()
        {
            return SysClock.Hour();
        }
        public static int DayOfTheWeek()
        {
            return SysClock.DayOfTheWeek();
        }
        public static int Month()
        {
            return SysClock.Month();
        }
        public static int Year()
        {
            return SysClock.Year();
        }
        public static int DayOfTheMonth()
        {
            return SysClock.DayOfTheMonth();
        }
    }
}
