using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    [TestClass]
    public class WorkDayCalculatorTests
    {

        [TestMethod]
        public void TestNoWeekEnd()
        {
            DateTime startDate = new DateTime(2021, 12, 1);
            int count = 10;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.AreEqual(startDate.AddDays(count - 1), result);
        }

        [TestMethod]
        public void TestNormalPath()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }

        [TestMethod]
        public void TestWeekendAfterEnd()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25)),
                new WeekEnd(new DateTime(2021, 4, 29), new DateTime(2021, 4, 29))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }

        [TestMethod]
        public void TestMultipleWeekndsBeforeAndAfterEnd()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 10;
            WeekEnd[] weekends = new WeekEnd[5]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25)),
                new WeekEnd(new DateTime(2021, 4, 29), new DateTime(2021, 4, 30)),
                new WeekEnd(new DateTime(2021, 5, 3), new DateTime(2021, 5, 3)),
                new WeekEnd(new DateTime(2021, 5, 9), new DateTime(2021, 5, 10)),
                new WeekEnd(new DateTime(2021, 5, 15), new DateTime(2021, 5, 17))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 5, 6)));
        }

        [TestMethod]
        public void TestIfStartDateIsWeekend()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 21), new DateTime(2021, 4, 21)),
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 29)));
        }

        [TestMethod]
        public void TestWeekendsBeforeStartDate()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 16), new DateTime(2021, 4, 17)),
                new WeekEnd(new DateTime(2021, 4, 19), new DateTime(2021, 4, 18))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 25)));
        }

        [TestMethod]
        public void AllPossibleCases()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 10;
            WeekEnd[] weekends = new WeekEnd[]
            {
                //Before start
                new WeekEnd(new DateTime(2021, 4, 13), new DateTime(2021, 4, 15)),
                new WeekEnd(new DateTime(2021, 4, 17), new DateTime(2021, 4, 18)),
                //At start
                new WeekEnd(new DateTime(2021, 4, 20), new DateTime(2021, 4, 21)),
                //In the interval
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25)),
                new WeekEnd(new DateTime(2021, 4, 29), new DateTime(2021, 4, 30)),
                new WeekEnd(new DateTime(2021, 5, 3), new DateTime(2021, 5, 3)),
                //After end
                new WeekEnd(new DateTime(2021, 5, 9), new DateTime(2021, 5, 10)),
                new WeekEnd(new DateTime(2021, 5, 15), new DateTime(2021, 5, 17))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 5, 7)));
        }
    }
}
