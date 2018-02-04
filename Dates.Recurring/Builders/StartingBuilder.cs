using System;

namespace Dates.Recurring.Builders
{
    public class StartingBuilder
    {
        private readonly DateTime _starting;

        public StartingBuilder(DateTime starting)
        {
            _starting = starting;
        }

        public EveryBuilder Every(int x)
        {
            return new EveryBuilder(_starting, x);
        }
    }
}