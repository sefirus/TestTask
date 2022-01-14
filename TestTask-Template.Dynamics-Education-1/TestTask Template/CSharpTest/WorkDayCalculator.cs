using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        /// <summary>
        /// Returns the date, which is counted as a start date + duration, without weekends
        /// </summary>
        /// <param name="startDate">The first day of the period</param>
        /// <param name="dayCount">Number if days in the period</param>
        /// <param name="weekEnds">Array of the weekends</param>

        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            if(weekEnds == null)
            {
                return startDate.AddDays(dayCount - 1);
            }

            foreach (WeekEnd weekEnd in weekEnds)
            {
                //If startDate is later then weekend`s end
                if (DateTime.Compare(startDate, weekEnd.EndDate) > 0) continue;

                //If startDate is in weekend
                if(DateTime.Compare(startDate, weekEnd.StartDate) > 0 && DateTime.Compare(startDate, weekEnd.EndDate) <= 0)
                {
                    startDate = weekEnd.EndDate.AddDays(1);
                    continue;
                }

                TimeSpan diff = weekEnd.StartDate - startDate;  //Time difference between current date and start date of the next weekend
                if(dayCount > diff.TotalDays)
                {
                    dayCount -= Convert.ToInt32(diff.TotalDays);
                    startDate = weekEnd.EndDate.AddDays(1);
                }
                else break;
            }
            DateTime res = startDate.AddDays(dayCount - 1);
            return res;
        }
    }
}
