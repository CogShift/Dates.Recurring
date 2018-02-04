using System;
using Dates.Recurring.Type;

namespace Dates.Recurring.Builders
{
    public class WeeksBuilder
    {
        private readonly int _weeks;
        private readonly DateTime _starting;
        private DateTime? _endingAfterDate;
        private Day _days;
        private int? _endingAfterNumOfOccurrences;

        public WeeksBuilder(int weeks, DateTime starting)
        {
            _weeks = weeks;
            _starting = starting;
            OnDay(starting.DayOfWeek);
        }

        public WeeksBuilder Ending(DateTime afterDate)
        {
            _endingAfterDate = afterDate;
            return this;
        }

        public WeeksBuilder Ending(int afterNumOfRecurrences)
        {
            _endingAfterNumOfOccurrences = afterNumOfRecurrences;
            return this;
        }

        public WeeksBuilder OnDays(Day days)
        {
            _days = days;
            return this;
        }

        public WeeksBuilder OnDay(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Sunday:
                    _days = Day.SUNDAY;
                    break;
                case DayOfWeek.Monday:
                    _days = Day.MONDAY;
                    break;
                case DayOfWeek.Tuesday:
                    _days = Day.TUESDAY;
                    break;
                case DayOfWeek.Wednesday:
                    _days = Day.WEDNESDAY;
                    break;
                case DayOfWeek.Thursday:
                    _days = Day.THURSDAY;
                    break;
                case DayOfWeek.Friday:
                    _days = Day.FRIDAY;
                    break;
                case DayOfWeek.Saturday:
                    _days = Day.SATURDAY;
                    break;
            }
            return this;
        }

        public Weekly Build()
        {
            return new Weekly(_weeks, _starting, _endingAfterDate, _endingAfterNumOfOccurrences, _days);
        }
    }
}