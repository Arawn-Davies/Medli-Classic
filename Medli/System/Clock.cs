using System;
using System.Collections.Generic;
using System.Text;
using Medli.Hardware;

namespace Medli.SysInternal
{
    public class MedliTime
    {
        public static void printTime()
        {
            Console.WriteLine("The current time is HH:{0} MM:{1} SS:{2}", Clock.Hour(), Clock.Minute(), Clock.Second());
        }
        public static void printDate()
        {
            Console.WriteLine("The current date is {0}/{1}/{2}", Clock.DayOfTheMonth(), Clock.Month(), Clock.Year());
        }
        public static int Second()
        {
            return Clock.Second();
        }
        public static int Minute()
        {
            return Clock.Minute();
        }
        public static int Hour()
        {
            return Clock.Hour();
        }
        public static int DayOfTheWeek()
        {
            return Clock.DayOfTheWeek();
        }
        public static int Month()
        {
            return Clock.Month();
        }
        public static int Year()
        {
            return Clock.Year();
        }
        public static int DayOfTheMonth()
        {
            return Clock.DayOfTheMonth();
        }
    }
}
