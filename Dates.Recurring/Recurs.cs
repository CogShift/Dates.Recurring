using System;
using Dates.Recurring.Builders;

namespace Dates.Recurring
{
    public static class Recurs
    {
        public static StartingBuilder Starting(DateTime start)
        {
            return new StartingBuilder(start);
        }
    }
}