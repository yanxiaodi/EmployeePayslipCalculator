using EmployeePayslipCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayslipCalculator.Service.Impl
{
    public class TaxCalculatorLevel4 : TaxCalculatorBase
    {
        public TaxCalculatorLevel4() { }
        public TaxCalculatorLevel4(int annualSalary) : base(annualSalary)
        {

        }

        public override int CalculateTax()
        {
            int result = 0;
            var tax = Utils.ConvertToInt((17547 + (this.AnnualSalary - 80000) * 0.37) / 12);
            int.TryParse(tax.ToString(), out result);
            return result;
        }
    }
}
