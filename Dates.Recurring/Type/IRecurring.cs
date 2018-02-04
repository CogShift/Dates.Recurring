using System;
using System.Collections.Generic;

namespace Dates.Recurring.Type
{
    public interface IRecurring
    {
        DateTime? Next(DateTime after);
        IEnumerable<DateTime> GetSchedule(DateTime? forecastLimit = null);
    }
}