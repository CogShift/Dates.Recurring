using System;
using System.Collections.Generic;
using Humanizer;

namespace Dates.Recurring.Type
{
    public class Weekly : RecurrenceType
    {
        public Day Days { get; set; }

        public Weekly(int weeks, DateTime starting, DateTime? endingAfterDate, int? endingAfterNumOfOccurrences, Day days)
            : base(weeks, starting, endingAfterDate, endingAfterNumOfOccurrences)
        {
            Days = days;
        }

        public override IEnumerable<DateTime> GetSchedule(DateTime? forecastLimit = null)
        {
            var occurrenceCount = 0;
            var next = Starting;
            var after = Starting - 1.Days();

            while (true)
            {
                if (DayOfWeekMatched(next.DayOfWeek))
                {
                    occurrenceCount++;

                    if (EndingAfterDate.HasValue && next > EndingAfterDate.Value ||
                        EndingAfterNumOfOccurrences.HasValue && occurrenceCount > EndingAfterNumOfOccurrences ||
                        next > forecastLimit ||
                        (DateTime.MaxValue.AddDays(-(X * 7)) - next).Days <= 0)
                        yield break;
                    if (next > after)
                        yield return next;
                }

                if (next.DayOfWeek != DayOfWeek.Saturday)
                {
                    next = next + 1.Days();
                }
                else
                {
                    // Skip ahead by x weeks.
                    next = next + X.Weeks();

                    // Rewind to the first day of the week.
                    var delta = DayOfWeek.Sunday - next.DayOfWeek;

                    if (delta > 0)
                        delta -= 7;

                    next = next + delta.Days();
                }
            }
        }

        private bool DayOfWeekMatched(DayOfWeek day)
        {
            if (day == DayOfWeek.Sunday && (Days & Day.SUNDAY) != 0)
                return true;

            if (day == DayOfWeek.Monday && (Days & Day.MONDAY) != 0)
                return true;

            if (day == DayOfWeek.Tuesday && (Days & Day.TUESDAY) != 0)
                return true;

            if (day == DayOfWeek.Wednesday && (Days & Day.WEDNESDAY) != 0)
                return true;

            if (day == DayOfWeek.Thursday && (Days & Day.THURSDAY) != 0)
                return true;

            if (day == DayOfWeek.Friday && (Days & Day.FRIDAY) != 0)
                return true;

            if (day == DayOfWeek.Saturday && (Days & Day.SATURDAY) != 0)
                return true;

            return false;
        }
    }
}