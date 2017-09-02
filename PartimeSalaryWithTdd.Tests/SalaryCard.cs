using PartimeSalaryWithTdd.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PartimeSalaryWithTdd.Tests
{
    class SalaryCard
    {
        public int HourlySalary { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        internal object CalculateSalary()
        {
            var workingHour = this.GetWorkingHour();
            var result = workingHour * this.HourlySalary;

            return result;
        }

        private double GetWorkingHour()
        {
            var moringEnd = new DateTime(this.StartTime.Year, this.StartTime.Month, this.StartTime.Day, 12, 0, 0);
            var afternoonStart = new DateTime(this.StartTime.Year, this.StartTime.Month, this.StartTime.Day, 13, 0, 0);

            var morningWorkhour = DateTimeHelper.TotalHours(this.StartTime, moringEnd);
            var afternoonWorkhour = DateTimeHelper.TotalHours(afternoonStart, this.EndTime);

            var workingHour = morningWorkhour + afternoonWorkhour;

            return workingHour;
        }

        public double FirstOverTimeRatio { get; set; }
    }
}
