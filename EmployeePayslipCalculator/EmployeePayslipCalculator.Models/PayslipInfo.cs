using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayslipCalculator.Models
{
    public class PayslipInfo
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public virtual EmployeeInfo Employee { get; set; }
        public DateTime PayPeriod { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal NetIncome { get; set; }
        public decimal Super { get; set; }
    }
}
