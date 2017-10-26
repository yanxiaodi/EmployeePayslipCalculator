using EmployeePayslipCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayslipCalculator.Service.Impl
{
    public class TaxCalculatorLevel2 : TaxCalculatorBase
    {
        public TaxCalculatorLevel2(int annualSalary) : base(annualSalary)
        {

        }
        public override int CalculateTax()
        {
            int result = 0;
            var tax = Utils.ConvertToInt(((this.AnnualSalary - 18200) * 0.19) / 12);
            int.TryParse(tax.ToString(), out result);
            return result;
        }
    }
}
