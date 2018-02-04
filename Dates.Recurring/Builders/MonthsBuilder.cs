using System;
using Dates.Recurring.Type;

namespace Dates.Recurring.Builders
{
    public class MonthsBuilder
    {
        private readonly int _skipMonths;
        private int _dayOfMonth = 1;
        private readonly DateTime _starting;
        private DateTime? _endingAfterDate;
        private int? _endingAfterNumOfOccurrences;

        public MonthsBuilder(int skipMonths, DateTime starting)
        {
            _skipMonths = skipMonths;
            _starting = starting;
        }

        public MonthsBuilder Ending(DateTime afterDate)
        {
            _endingAfterDate = afterDate;
            return this;
        }

        public MonthsBuilder Ending(int afterNumOfRecurrences)
        {
            _endingAfterNumOfOccurrences = afterNumOfRecurrences;
            return this;
        }

        public MonthsBuilder OnDay(int dayOfMonth)
        {
            _dayOfMonth = dayOfMonth;
            return this;
        }

        public Monthly Build()
        {
            return new Monthly(_skipMonths, _dayOfMonth, _starting, _endingAfterDate, _endingAfterNumOfOccurrences);
        }
    }
}