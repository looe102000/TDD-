using PartimeSalaryWithTdd.Utility;
using System;

namespace PartimeSalaryWithTdd.Tests
{
    internal class SalaryCard
    {
        public int HourlySalary { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int NormalWorkingHourLimit { get => 8; }

        internal object CalculateSalary()
        {
            var workingHour = this.GetWorkingHour();

            if (workingHour <= NormalWorkingHourLimit)
            {
                return workingHour * this.HourlySalary;
            }
            else
            {
                var normalPay = NormalWorkingHourLimit * this.HourlySalary;

                var overTimeHour = workingHour - NormalWorkingHourLimit;
                double overTimePay = GetOverTimePay(workingHour);

                return normalPay + overTimePay;
            }
        }

        private double GetOverTimePay(double workingHour)
        {
            double overTimeHour = GetoverTimeHour(workingHour);

            // separate two phase of overtime hour
            var firstOverTime = overTimeHour <= 2 ? overTimeHour : 2;
            var secondOverTime = overTimeHour > 2 ? overTimeHour - firstOverTime : 0;

            //var overTimePay = overTimeHour * this.FirstOverTimeRatio * this.HourlySalary;
            var firstOverTimePay = firstOverTime * this.FirstOverTimeRatio * this.HourlySalary;
            var secondOverTimePay = secondOverTime * this.SecondOverTimeRatio * this.HourlySalary;

            return firstOverTimePay + secondOverTimePay;
        }

        private double GetoverTimeHour(double workingHour)
        {
            var overTimeHour = workingHour - NormalWorkingHourLimit;

            // 加班最多只能報4hr
            var result = overTimeHour > 4 ? 4 : overTimeHour;
            return result;
        }

        private double GetWorkingHour()
        {
            var moringEnd = new DateTime(this.StartTime.Year, this.StartTime.Month, this.StartTime.Day, 12, 0, 0);
            var afternoonStart = new DateTime(this.StartTime.Year, this.StartTime.Month, this.StartTime.Day, 13, 0, 0);

            var morningWorkhour = DateTimeHelper.TotalHours(this.StartTime, moringEnd);
            var afternoonWorkhour = DateTimeHelper.TotalHours(afternoonStart, this.EndTime);

            return morningWorkhour + afternoonWorkhour;
        }

        public double FirstOverTimeRatio { get; set; }
        public int SecondOverTimeRatio { get; internal set; }
    }
}