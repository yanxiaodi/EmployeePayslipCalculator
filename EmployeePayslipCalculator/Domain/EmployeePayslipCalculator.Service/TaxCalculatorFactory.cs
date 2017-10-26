using EmployeePayslipCalculator.Models;
using EmployeePayslipCalculator.Service.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayslipCalculator.Service
{
    public static class TaxCalculatorFactory
    {
        
        public static TaxCalculatorBase CreateTaxCalculator(int annualSalary)
        {
            if (annualSalary <= 0)
            {
                throw new Exception("wrong salary range!");
            }
            else if (annualSalary <= 18200)
            {
                return new TaxCalculatorLevel1(annualSalary);
            }
            else if (annualSalary <= 37000)
            {
                return new TaxCalculatorLevel2(annualSalary);
            }
            else if (annualSalary <= 80000)
            {
                return new TaxCalculatorLevel3(annualSalary);
            }
            else if (annualSalary <= 180000)
            {
                return new TaxCalculatorLevel4(annualSalary);
            }
            else
            {
                return new TaxCalculatorLevel5(annualSalary);
            }
        }
    }
}
