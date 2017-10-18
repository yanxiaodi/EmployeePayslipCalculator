using EmployeePayslipCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayslipCalculator.Service
{
    public abstract class TaxCalculatorBase
    {
        public int AnnualSalary { get; set; }

        public TaxCalculatorBase() { }
        public TaxCalculatorBase(int annualSalary)
        {
            this.AnnualSalary = annualSalary;
        }
        public virtual int CalculateTax()
        {
            return 0;
        }
    }
}
