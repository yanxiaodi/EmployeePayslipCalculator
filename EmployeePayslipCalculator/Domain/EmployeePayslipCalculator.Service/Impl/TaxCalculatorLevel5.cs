using EmployeePayslipCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayslipCalculator.Service.Impl
{
    public class TaxCalculatorLevel5 : TaxCalculatorBase
    {
        public TaxCalculatorLevel5(int annualSalary) : base(annualSalary)
        {

        }

        public override int CalculateTax()
        {
            int result = 0;
            var tax = Utils.ConvertToInt((54547 + (this.AnnualSalary - 180000) * 0.45) / 12);
            int.TryParse(tax.ToString(), out result);
            return result;
        }
    }
}
