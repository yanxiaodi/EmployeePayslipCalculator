using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayslipCalculator.Models
{
    public class PayslipInfo
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public virtual EmployeeInfo Employee { get; set; }
        public DateTime PayPeriod { get; set; }
        public int GrossIncome { get; set; }
        public int IncomeTax { get; set; }
        public int NetIncome { get; set; }
        public int Super { get; set; }
    }
}
