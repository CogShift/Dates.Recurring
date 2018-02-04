using System;
using System.Collections.Generic;
using Humanizer;

namespace Dates.Recurring.Type
{
    public class Monthly : RecurrenceType
    {
        public int DayOfMonth { get; set; }

        public Monthly(int skipMonths, int dayOfMonth, DateTime starting, DateTime? endingAfterDate, int? endingAfterNumOfOccurrences)
            : base(skipMonths, starting, endingAfterDate, endingAfterNumOfOccurrences)
        {
            DayOfMonth = dayOfMonth;
        }

        public override IEnumerable<DateTime> GetSchedule(DateTime? forecastLimit = null)
        {
            var occurrenceCount = 0;
            var next = Starting;
            var after = Starting - 1.Days();

            while (true)
            {
                if (DayOfMonthMatched(next))
                {
                    occurrenceCount++;

                    if (EndingAfterDate.HasValue && next > EndingAfterDate.Value ||
                        EndingAfterNumOfOccurrences.HasValue && occurrenceCount > EndingAfterNumOfOccurrences ||
                        next > forecastLimit ||
                        (DateTime.MaxValue.AddMonths(-X) - next).Days <= 0)
                        yield break;
                    if (next > after)
                        yield return next;
                }

                var dayOfMonth = Math.Min(DayOfMonth, DateTime.DaysInMonth(next.Year, next.Month));

                if (next.Day < dayOfMonth)
                {
                    next = next + 1.Days();
                }
                else
                {
                    // Rewind to the first of the month.
                    next = next + (-1 * next.Day + 1).Days();

                    // Skip ahead by the required number of months.
                    next = next.AddMonths(X);
                }
            }
        }

        private bool DayOfMonthMatched(DateTime date)
        {
            var dayOfMonth = Math.Min(DayOfMonth, DateTime.DaysInMonth(date.Year, date.Month));
            return date.Day == dayOfMonth;
        }
    }
}