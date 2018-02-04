using System;
using System.Collections.Generic;
using Humanizer;

namespace Dates.Recurring.Type
{
    public class Daily : RecurrenceType
    {
        public Daily(int days, DateTime starting, DateTime? endingAfterDate, int? endingAfterNumOfOccurrences)
            : base(days, starting, endingAfterDate, endingAfterNumOfOccurrences) { }

        public override IEnumerable<DateTime> GetSchedule(DateTime? forecastLimit = null)
        {
            var occurrenceCount = 0;
            var next = Starting;
            var after = Starting - 1.Days();

            while (true)
            {
                occurrenceCount++;


                if (EndingAfterDate.HasValue && next > EndingAfterDate.Value ||
                    EndingAfterNumOfOccurrences.HasValue && occurrenceCount > EndingAfterNumOfOccurrences ||
                    next > forecastLimit ||
                    (DateTime.MaxValue.AddDays(-X) - next).Days == 0)
                    yield break;

                if (next > after)
                    yield return next;

                next = next.AddDays(X);
            }
        }
    }
}