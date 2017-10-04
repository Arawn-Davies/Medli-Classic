using System;
using System.Collections.Generic;
using System.Text;
using Medli.System;

namespace Medli.SysInternal
{
    public class MedliTime
    {
        public static string GetDay()
        {
            if (SysClock.DayOfTheWeek() == 1)
            {
                return "Thursday";
            }
            else if (SysClock.DayOfTheWeek() == 2)
            {
                return "Friday";
            }
            else if (SysClock.DayOfTheWeek() == 3)
            {
                return "Saturday";
            }
            else if (SysClock.DayOfTheWeek() == 4)
            {
                
                return "Sunday";
            }
            else if (SysClock.DayOfTheWeek() == 5)
            {
                return "Monday";
            }
            else if (SysClock.DayOfTheWeek() == 6)
            {
                return "Tuesday";
            }
            else if (SysClock.DayOfTheWeek() == 7)
            {
                return "Wednesday";
            }
            else
            {
                return "Invalid DayOfTheWeek";
            }
        }
        public static void printTime()
        {
            Console.WriteLine("The current time is " + SysClock.Hour().ToString() + " :" + SysClock.Minute().ToString() + " :" + SysClock.Second().ToString());
        }
        public static void printDate()
        {
            Console.WriteLine("The current date is " + GetDay() + " " + SysClock.DayOfTheMonth().ToString() + ", of " + SysClock.Month().ToString() + ", " + SysClock.Year().ToString());
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
