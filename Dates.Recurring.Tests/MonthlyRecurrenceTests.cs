﻿using Dates.Recurring.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dates.Recurring.Tests
{
    public class MonthlyRecurrenceTests
    {

        [Fact]
        public void Monthly_EveryMonth()
        {
            // Arrange.
            IRecurring monthly = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(1)
                .Months()
                .OnDay(24)
                .Ending(new DateTime(2016, 2, 4))
                .Build();

            // Act.

            // Assert.
            Assert.Equal(new DateTime(2015, 1, 24), monthly.Next(new DateTime(2014, 4, 8)));
            Assert.Equal(new DateTime(2015, 2, 24), monthly.Next(new DateTime(2015, 1, 24)));
            Assert.Equal(new DateTime(2015, 2, 24), monthly.Next(new DateTime(2015, 2, 23)));
            Assert.Equal(new DateTime(2015, 3, 24), monthly.Next(new DateTime(2015, 2, 24)));
            Assert.Equal(new DateTime(2015, 3, 24), monthly.Next(new DateTime(2015, 2, 25)));
            Assert.Equal(new DateTime(2015, 6, 24), monthly.Next(new DateTime(2015, 6, 3)));
            Assert.Null(monthly.Next(new DateTime(2016, 6, 3)));
            Assert.Equal(new DateTime(2016, 1, 24), monthly.Prev(new DateTime(2017, 4, 8)));
            Assert.Equal(new DateTime(2015, 12, 24), monthly.Prev(new DateTime(2016, 1, 24)));
            Assert.Equal(new DateTime(2015, 12, 24), monthly.Prev(new DateTime(2015, 12, 25)));
            Assert.Equal(new DateTime(2015, 11, 24), monthly.Prev(new DateTime(2015, 12, 24)));
            Assert.Equal(new DateTime(2015, 11, 24), monthly.Prev(new DateTime(2015, 12, 23)));
            Assert.Equal(new DateTime(2015, 6, 24), monthly.Prev(new DateTime(2015, 7, 3)));
            Assert.Null(monthly.Prev(new DateTime(2015, 1, 1)));
        }

        [Fact]
        public void Monthly_EveryThirdMonth()
        {
            // Arrange.
            IRecurring monthly = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(3)
                .Months()
                .OnDay(24)
                .Ending(new DateTime(2016, 2, 4))
                .Build();

            // Act.

            // Assert.
            Assert.Equal(new DateTime(2015, 1, 24), monthly.Next(new DateTime(2014, 2, 1)));
            Assert.Equal(new DateTime(2015, 4, 24), monthly.Next(new DateTime(2015, 1, 24)));
            Assert.Equal(new DateTime(2015, 4, 24), monthly.Next(new DateTime(2015, 1, 25)));
            Assert.Equal(new DateTime(2015, 4, 24), monthly.Next(new DateTime(2015, 2, 1)));
            Assert.Equal(new DateTime(2015, 4, 24), monthly.Next(new DateTime(2015, 2, 17)));
            Assert.Equal(new DateTime(2015, 4, 24), monthly.Next(new DateTime(2015, 4, 23)));
            Assert.Equal(new DateTime(2015, 7, 24), monthly.Next(new DateTime(2015, 4, 24)));
            Assert.Null(monthly.Next(new DateTime(2016, 6, 3)));
            Assert.Equal(new DateTime(2016, 1, 24), monthly.Prev(new DateTime(2017, 2, 1)));
            Assert.Equal(new DateTime(2015, 4, 24), monthly.Prev(new DateTime(2015, 6, 24)));
            Assert.Equal(new DateTime(2015, 4, 24), monthly.Prev(new DateTime(2015, 6, 23)));
            Assert.Equal(new DateTime(2015, 4, 24), monthly.Prev(new DateTime(2015, 6, 1)));
            Assert.Equal(new DateTime(2015, 4, 24), monthly.Prev(new DateTime(2015, 5, 5)));
            Assert.Equal(new DateTime(2015, 4, 24), monthly.Prev(new DateTime(2015, 4, 25)));
            Assert.Equal(new DateTime(2015, 1, 24), monthly.Prev(new DateTime(2015, 4, 24)));
            Assert.Null(monthly.Prev(new DateTime(2015, 1, 24)));
        }

        [Fact]
        public void Monthly_EveryMonth_DifferentDaysInMonths()
        {
            // Arrange.
            IRecurring monthly = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(1)
                .Months()
                .OnDay(31)
                .Ending(new DateTime(2016, 2, 4))
                .Build();

            // Act.

            // Assert.
            Assert.Equal(new DateTime(2015, 1, 31), monthly.Next(new DateTime(2014, 2, 1)));
            Assert.Equal(new DateTime(2015, 1, 31), monthly.Next(new DateTime(2015, 1, 30)));
            Assert.Equal(new DateTime(2015, 2, 28), monthly.Next(new DateTime(2015, 1, 31)));
            Assert.Equal(new DateTime(2015, 2, 28), monthly.Next(new DateTime(2015, 2, 27)));
            Assert.Equal(new DateTime(2015, 3, 31), monthly.Next(new DateTime(2015, 2, 28)));
            Assert.Equal(new DateTime(2015, 4, 30), monthly.Next(new DateTime(2015, 3, 31)));
            Assert.Null(monthly.Next(new DateTime(2016, 6, 3)));
            Assert.Equal(new DateTime(2016, 1, 31), monthly.Prev(new DateTime(2017, 2, 1)));
            Assert.Equal(new DateTime(2015, 1, 31), monthly.Prev(new DateTime(2015, 2, 1)));
            Assert.Equal(new DateTime(2015, 2, 28), monthly.Prev(new DateTime(2015, 3, 1)));
            Assert.Equal(new DateTime(2015, 1, 31), monthly.Prev(new DateTime(2015, 2, 28)));
            Assert.Equal(new DateTime(2015, 3, 31), monthly.Prev(new DateTime(2015, 4, 1)));
            Assert.Equal(new DateTime(2015, 4, 30), monthly.Prev(new DateTime(2015, 5, 1)));
            Assert.Null(monthly.Prev(new DateTime(2015, 1, 31)));
        }

        [Fact]
        public void Monthly_ComparePastAndFutureSeries()
        {
            // Arrange.
            IRecurring recur = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(3)
                .Months()
                .OnDay(24)
                .Ending(new DateTime(2016, 2, 4))
                .Build();

            // Act.
            var futureSeries = recur.Future(new DateTime(2014, 1, 1)).Take(5).ToList();
            var last = futureSeries.Last();
            futureSeries.RemoveAt(futureSeries.Count - 1);
            var pastSeries = recur.Past(last).Reverse().ToList();

            Assert.Equal(futureSeries, pastSeries);
        }
    }
}

