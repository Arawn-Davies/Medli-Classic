/* DateTime.cs - provides date/time information on NoobOS
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
using Cosmos.HAL;

namespace Medli.System.Environment
{
    class DateTime
    {
        private int[] MonthToDays
        {
            get
            {
                List<int> l = new List<int>();
                l.Add(31);
                l.Add(28);
                l.Add(31);
                l.Add(30);
                l.Add(31);
                l.Add(30);
                l.Add(31);
                l.Add(31);
                l.Add(30);
                l.Add(31);
                l.Add(30);
                l.Add(31);
                return l.ToArray();
            }
        }

        #region private time
        private int _Hour;

        private int _Minute;

        private int _Second;
        #endregion

        #region private date
        private int _Year;

        private int _Month;

        private int _Day;

        private int _DayOfWeek;
        #endregion

        #region time
        /// <summary>
        /// Represents the DateTime's Hour
        /// </summary>
        public int Hour
        {
            get
            {
                return _Hour;
            }
        }

        /// <summary>
        /// Represents the DateTime's Minute
        /// </summary>
        public int Minute
        {
            get
            {
                return _Minute;
            }
        }

        /// <summary>
        /// Represents the DateTime's Second
        /// </summary>
        public int Second
        {
            get
            {
                return _Second;
            }
        }
        #endregion

        #region date
        /// <summary>
        /// Represents the DateTime's Year
        /// </summary>
        public int Year
        {
            get
            {
                return _Year;
            }
        }

        /// <summary>
        /// Represents the DateTime's Month
        /// </summary>
        public int Month
        {
            get
            {
                return _Month;
            }
        }

        /// <summary>
        /// Represents the DateTime's Day
        /// </summary>
        public int Day
        {
            get
            {
                return _Day;
            }
        }

        /// <summary>
        /// Represents the DateTime's DayOfWeek as an integer
        /// </summary>
        public int DayOfWeek
        {
            get
            {
                return _DayOfWeek;
            }
        }
        #endregion

        /// <summary>
        /// Rapresents the UNIX TimeStamp of the current DateTime
        /// </summary>
        public long TimeStamp
        {
            get
            {
                long ret = 0;
                long secondsinyear = 31536000;
                long secondsinday = 86400;
                for (int i = 1970; i < this.Year - 1; i++)
                {
                    ret += secondsinyear;
                    if ((i % 400 == 0 || (i % 4 == 0 && i % 100 != 0)))
                    {
                        ret += secondsinday;
                    }
                }
                for (int i = 1; i < this.Month - 1; i++)
                {
                    ret += MonthToDays[i] * secondsinday;
                    if (i == 2 && (this.Year % 400 == 0 || (this.Year % 4 == 0 && this.Year % 100 != 0)))
                    {
                        ret += secondsinday;
                    }
                }
                ret += this.Day * secondsinday;
                ret += this.Hour * 3600;
                ret += this.Minute * 60;
                ret += this.Second;
                return ret;
            }
        }

        /// <summary>
        /// The DateTime of this moment
        /// </summary>
        public static DateTime Now
        {
            get
            {
                return new DateTime((RTC.Century * 100) + RTC.Year, RTC.Month, RTC.DayOfTheMonth, RTC.DayOfTheWeek, RTC.Hour, RTC.Minute, RTC.Second);
            }
        }

        /// <summary>
        /// Creates a new DateTime object by getting the required informations
        /// </summary>
        /// <param name="Year">The Year of the new DateTime</param>
        /// <param name="Month">The Month of the new DateTime</param>
        /// <param name="Day">The Day of the new DateTime</param>
        /// <param name="DayOfWeek">The DayOfWeek of the new DateTime (Still not implemented the automatic function to calculate it)</param>
        /// <param name="Hour">The Hour of the new DateTime</param>
        /// <param name="Minute">The Minute of the new DateTime</param>
        /// <param name="Second">The Second of the new DateTime</param>
        /// <remarks>Not fully implemented so don't use invalid dates! E.G. 30/02/2012</remarks>
        public DateTime(int Year, int Month, int Day, int DayOfWeek, int Hour, int Minute, int Second)
        {
            this._Year = Year;
            this._Month = Month;
            this._Day = Day;
            this._DayOfWeek = DayOfWeek;
            this._Hour = Hour;
            this._Minute = Minute;
            this._Second = Second;
        }

        public override string ToString()
        {
            return "Not Implemented";
        }
    }
}
