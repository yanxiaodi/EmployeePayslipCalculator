using EmployeePayslipCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayslipCalculator.Service.Impl
{
    public class TaxCalculatorLevel3 : TaxCalculatorBase
    {
        public TaxCalculatorLevel3() { }
        public TaxCalculatorLevel3(int annualSalary) : base(annualSalary)
        {

        }

        public override int CalculateTax()
        {
            int result = 0;
            var tax = Utils.ConvertToInt((3572 + (this.AnnualSalary - 37000) * 0.325) / 12);
            int.TryParse(tax.ToString(), out result);
            return result;
        }
    }
}
