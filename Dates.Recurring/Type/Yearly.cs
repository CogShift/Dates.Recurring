using System;
using System.Collections.Generic;
using Humanizer;

namespace Dates.Recurring.Type
{
    public class Yearly : RecurrenceType
    {
        public int DayOfMonth { get; set; }
        public Month Month { get; set; }

        public Yearly(int skipYears, int dayOfMonth, Month month, DateTime starting, DateTime? endingAfterDate, int? endingAfterNumOfOccurrences)
            : base(skipYears, starting, endingAfterDate, endingAfterNumOfOccurrences)
        {
            DayOfMonth = dayOfMonth;
            Month = month;
        }

        public override IEnumerable<DateTime> GetSchedule(DateTime? forecastLimit = null)
        {
            var occurrenceCount = 0;
            var next = Starting;
            var after = Starting - 1.Days();

            while (true)
            {
                if (DayOfMonthMatched(next) && MonthMatched(next))
                {
                    occurrenceCount++;

                    if (EndingAfterDate.HasValue && next > EndingAfterDate.Value ||
                        EndingAfterNumOfOccurrences.HasValue && occurrenceCount > EndingAfterNumOfOccurrences ||
                        next > forecastLimit ||
                        (DateTime.MaxValue.AddYears(-X) - next).Days <= 0)
                        yield break;
                    if (next > after)
                        yield return next;
                }

                if (!MonthMatched(next))
                {
                    if (next.Month == 12)
                    {
                        // Rewind to the first of the month.
                        next = next + (-1 * next.Day + 1).Days();

                        // Rewind to the first month
                        next = next.AddMonths(-1 * next.Month + 1);

                        // Skip ahead by the required number of years.
                        next = next.AddYears(X);
                    }
                    else
                    {
                        // Rewind to the first of the month.
                        next = next + (-1 * next.Day + 1).Days();

                        // Skip to the next month.
                        next = next.AddMonths(1);
                    }
                }
                else
                {
                    var dayOfMonth = Math.Min(DayOfMonth, DateTime.DaysInMonth(next.Year, next.Month));

                    if (next.Day < dayOfMonth)
                    {
                        next = next + 1.Days();
                    }
                    else
                    {
                        // Rewind to the first of the month.
                        next = next + (-1 * next.Day + 1).Days();

                        // Skip to the next month.
                        next = next.AddMonths(1);
                    }
                }
            }
        }

        private bool DayOfMonthMatched(DateTime date)
        {
            var dayOfMonth = Math.Min(DayOfMonth, DateTime.DaysInMonth(date.Year, date.Month));
            return date.Day == dayOfMonth;
        }

        private bool MonthMatched(DateTime date)
        {
            if (date.Month == 1 && (Month & Month.JANUARY) != 0)
                return true;

            if (date.Month == 2 && (Month & Month.FEBRUARY) != 0)
                return true;

            if (date.Month == 3 && (Month & Month.MARCH) != 0)
                return true;

            if (date.Month == 4 && (Month & Month.APRIL) != 0)
                return true;

            if (date.Month == 5 && (Month & Month.MAY) != 0)
                return true;

            if (date.Month == 6 && (Month & Month.JUNE) != 0)
                return true;

            if (date.Month == 7 && (Month & Month.JULY) != 0)
                return true;

            if (date.Month == 8 && (Month & Month.AUGUST) != 0)
                return true;

            if (date.Month == 9 && (Month & Month.SEPTEMBER) != 0)
                return true;

            if (date.Month == 10 && (Month & Month.OCTOBER) != 0)
                return true;

            if (date.Month == 11 && (Month & Month.NOVEMBER) != 0)
                return true;

            if (date.Month == 12 && (Month & Month.DECEMBER) != 0)
                return true;

            return false;
        }
    }
}