using System;
using System.Collections.Generic;
using System.Linq;

namespace Dates.Recurring.Type
{
    public abstract class RecurrenceType : IRecurring
    {
        protected int X { get; set; }
        protected DateTime Starting { get; set; }
        protected DateTime? EndingAfterDate { get; set; }
        protected int? EndingAfterNumOfOccurrences { get; set; }

        protected RecurrenceType(int x, DateTime starting, DateTime? endingAfterDate = null, int? endingAfterNumOfOccurrences = null)
        {
            X = x;
            Starting = starting;
            EndingAfterDate = endingAfterDate;
            EndingAfterNumOfOccurrences = endingAfterNumOfOccurrences;
        }

        public abstract IEnumerable<DateTime> GetSchedule(DateTime? forecastLimit = null);

        public DateTime? Next(DateTime after)
        {
            return GetSchedule(DateTime.MaxValue)
                .Cast<DateTime?>()
                .FirstOrDefault(date => date > after);
        }

        public virtual IEnumerable<DateTime> Future(DateTime after)
        {
            for (var next = Next(after); next != null; next = Next(next.Value))
                yield return next.Value;
        }
    }
}