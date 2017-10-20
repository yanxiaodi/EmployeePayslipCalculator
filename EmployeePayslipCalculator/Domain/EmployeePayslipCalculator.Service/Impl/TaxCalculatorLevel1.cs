using System;
using System.Collections.Generic;
using System.Text;
using EmployeePayslipCalculator.Models;

namespace EmployeePayslipCalculator.Service.Impl
{
    public class TaxCalculatorLevel1 : TaxCalculatorBase
    {
        public TaxCalculatorLevel1(int annualSalary) : base(annualSalary)
        {

        }
        public override int CalculateTax()
        {
            return 0;
        }
    }
}
