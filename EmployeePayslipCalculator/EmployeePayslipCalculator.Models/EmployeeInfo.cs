using System;

namespace EmployeePayslipCalculator.Models
{
    public class EmployeeInfo
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AnnualSalary { get; set; }
        public double SuperRate { get; set; }
    }
}
