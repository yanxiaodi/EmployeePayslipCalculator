using EmployeePayslipCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayslipCalculator.Service
{
    public abstract class TaxCalculatorBase
    {
        protected int AnnualSalary { get; set; }

        public TaxCalculatorBase(int annualSalary)
        {
            this.AnnualSalary = annualSalary;
        }
        public abstract int CalculateTax();

    }
}
