using System;
using Dates.Recurring.Type;

namespace Dates.Recurring.Builders
{
    public class DaysBuilder
    {
        private readonly int _days;
        private readonly DateTime _starting;
        private DateTime? _endingAfterDate;
        private int? _endingAfterNumOfOccurrences;

        public DaysBuilder(int days, DateTime starting)
        {
            _days = days;
            _starting = starting;
        }

        public DaysBuilder Ending(DateTime afterDate)
        {
            _endingAfterDate = afterDate;
            return this;
        }

        public DaysBuilder Ending(int afterNumOfRecurrences)
        {
            _endingAfterNumOfOccurrences = afterNumOfRecurrences;
            return this;
        }

        public Daily Build()
        {
            return new Daily(_days, _starting, _endingAfterDate, _endingAfterNumOfOccurrences);
        }
    }
}