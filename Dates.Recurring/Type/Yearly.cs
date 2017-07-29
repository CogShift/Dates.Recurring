﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;

namespace Dates.Recurring.Type
{
    public class Yearly : RecurrenceType
    {
        public int DayOfMonth { get; set; }
        public Month Month { get; set; }

        public Yearly(int skipYears, int dayOfMonth, Month month, DateTime starting, DateTime? ending) : base(skipYears, starting, ending)
        {
            DayOfMonth = dayOfMonth;
            Month = month;
        }

        public override DateTime? Next(DateTime after)
        {
            return Next(after, false);
        }

        private DateTime? Next(DateTime after, bool ignoreEndLimit)
        {
            var next = Starting;

            if (after.Date < Starting.Date)
            {
                after = Starting - 1.Days();
            }

            while (next.Date <= after.Date || !MonthMatched(next) || !DayOfMonthMatched(next))
            {
                if (!MonthMatched(next))
                {
                    if (next.Month == 12)
                    {
                        // Rewind to the first of the month.
                        next = next + ((-1 * next.Day) + 1).Days();

                        // Rewind to the first month
                        next = next.AddMonths((-1 * next.Month) + 1);

                        // Skip ahead by the required number of years.
                        next = next.AddYears(X);
                    }
                    else
                    {
                        // Rewind to the first of the month.
                        next = next + ((-1 * next.Day) + 1).Days();

                        // Skip to the next month.
                        next = next.AddMonths(1);
                    }
                }
                else
                {
                    int dayOfMonth = Math.Min(DayOfMonth, DateTime.DaysInMonth(next.Year, next.Month));

                    if (next.Day < dayOfMonth)
                    {
                        next = next + 1.Days();
                    }
                    else
                    {
                        // Rewind to the first of the month.
                        next = next + ((-1 * next.Day) + 1).Days();

                        // Skip to the next month.
                        next = next.AddMonths(1);
                    }
                }
            }

            if (!ignoreEndLimit && Ending.HasValue && next.Date >= Ending.Value.Date)
            {
                return null;
            }

            return next;
        }

        public override DateTime? Prev(DateTime before)
        {
            // ReSharper disable once PossibleInvalidOperationException
            var next = Next(before, true).Value;

            if (before.Date > Ending.Value.Date)
            {
                before = Ending.Value + 1.Days();
            }

            while (next.Date >= before.Date || !MonthMatched(next) || !DayOfMonthMatched(next))
            {
                if (!MonthMatched(next))
                {
                    next = next.AddMonths(-1);

                    next = next + (DateTime.DaysInMonth(next.Year, next.Month) - next.Day).Days();
                }
                else
                {
                    int dayOfMonth = Math.Min(DayOfMonth, DateTime.DaysInMonth(next.Year, next.Month));

                    if (next.Day > dayOfMonth)
                    {
                        next = next - 1.Days();
                    }
                    else if (next.Month == 1)
                    {
                        next = next.AddMonths(11);

                        // Skip back the the required number of years.
                        next = next.AddYears(-X);

                        // Rewind to the last day in the month
                        next = next + (DateTime.DaysInMonth(next.Year, next.Month) - next.Day).Days();
                    }
                    else
                    {
                        // Skip to the next month.
                        next = next.AddMonths(-1);

                        next = next + (DateTime.DaysInMonth(next.Year, next.Month) - next.Day).Days();
                    }
                }
            }

            if (next.Date < Starting.Date)
            {
                return null;
            }

            return next;
        }

        private bool DayOfMonthMatched(DateTime date)
        {
            int dayOfMonth = Math.Min(DayOfMonth, DateTime.DaysInMonth(date.Year, date.Month));
            return (date.Day == dayOfMonth);
        }

        private bool MonthMatched(DateTime date)
        {
            if (date.Month == 1 && (Month & Recurring.Month.JANUARY) != 0)
                return true;

            if (date.Month == 2 && (Month & Recurring.Month.FEBRUARY) != 0)
                return true;

            if (date.Month == 3 && (Month & Recurring.Month.MARCH) != 0)
                return true;

            if (date.Month == 4 && (Month & Recurring.Month.APRIL) != 0)
                return true;

            if (date.Month == 5 && (Month & Recurring.Month.MAY) != 0)
                return true;

            if (date.Month == 6 && (Month & Recurring.Month.JUNE) != 0)
                return true;

            if (date.Month == 7 && (Month & Recurring.Month.JULY) != 0)
                return true;

            if (date.Month == 8 && (Month & Recurring.Month.AUGUST) != 0)
                return true;

            if (date.Month == 9 && (Month & Recurring.Month.SEPTEMBER) != 0)
                return true;

            if (date.Month == 10 && (Month & Recurring.Month.OCTOBER) != 0)
                return true;

            if (date.Month == 11 && (Month & Recurring.Month.NOVEMBER) != 0)
                return true;

            if (date.Month == 12 && (Month & Recurring.Month.DECEMBER) != 0)
                return true;

            return false;
        }
    }
}
