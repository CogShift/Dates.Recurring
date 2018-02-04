using System;
using Dates.Recurring.Type;
using Xunit;

namespace Dates.Recurring.Tests
{
    public class DailyRecurrenceTests
    {
        [Fact]
        public void Daily_EveryDay()
        {
            // Arrange.
            IRecurring daily = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(1)
                .Days()
                .Ending(new DateTime(2015, 1, 15))
                .Build();

            // Act.

            // Assert.
            Assert.Equal(new DateTime(2015, 1, 1), daily.Next(new DateTime(2014, 7, 3)));
            Assert.Equal(new DateTime(2015, 1, 2), daily.Next(new DateTime(2015, 1, 1)));
            Assert.Equal(new DateTime(2015, 1, 3), daily.Next(new DateTime(2015, 1, 2)));
            Assert.Null(daily.Next(new DateTime(2015, 1, 15)));
        }

        [Fact]
        public void Daily_EveryThirdDay()
        {
            // Arrange.
            IRecurring daily = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(3)
                .Days()
                .Ending(new DateTime(2015, 1, 15))
                .Build();

            // Act.

            // Assert.
            Assert.Equal(new DateTime(2015, 1, 1), daily.Next(new DateTime(2014, 7, 3)));
            Assert.Equal(new DateTime(2015, 1, 4), daily.Next(new DateTime(2015, 1, 1)));
            Assert.Equal(new DateTime(2015, 1, 7), daily.Next(new DateTime(2015, 1, 5)));
            Assert.Equal(new DateTime(2015, 1, 13), daily.Next(new DateTime(2015, 1, 10)));
        }

        [Fact]
        public void Daily_EndAfterNumOfOccurences()
        {
            IRecurring daily = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(2)
                .Days()
                .Ending(2)
                .Build();

            Assert.Equal(new DateTime(2015, 1, 1), daily.Next(new DateTime(2014, 7, 3)));
            Assert.Equal(new DateTime(2015, 1, 3), daily.Next(new DateTime(2015, 1, 1)));
            Assert.Null(daily.Next(new DateTime(2015, 1, 3)));
        }
    }
}