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

        public int normalWorkingHourLimit { get => 8; }

        internal object CalculateSalary()
        {
            var workingHour = this.GetWorkingHour();
            //int normalWorkingHourLimit = 8;

            if (workingHour <= normalWorkingHourLimit)
            {
                var result = workingHour * this.HourlySalary;

                return result;
            }
            else
            {

                var normalPay = normalWorkingHourLimit * this.HourlySalary;

                var overTimeHour = workingHour - normalWorkingHourLimit;
                double overTimePay = GetOverTimePay(workingHour);

                var result = normalPay + overTimePay;

                return result;
            }
        }

        private double GetOverTimePay(double workingHour)
        {
            var overTimeHour = workingHour - normalWorkingHourLimit;

            // separate two phase of overtime hour
            var firstOverTime = overTimeHour <= 2 ? overTimeHour : 2;
            var secondOverTime = overTimeHour > 2 ? overTimeHour - firstOverTime : 0;

            //var overTimePay = overTimeHour * this.FirstOverTimeRatio * this.HourlySalary;
            var firstOverTimePay = firstOverTime * this.FirstOverTimeRatio * this.HourlySalary;
            var secondOverTimePay = secondOverTime * this.SecondOverTimeRatio * this.HourlySalary;

            var overTimePay = firstOverTimePay + secondOverTimePay;
            return overTimePay;
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
        public int SecondOverTimeRatio { get; internal set; }
    }
}
