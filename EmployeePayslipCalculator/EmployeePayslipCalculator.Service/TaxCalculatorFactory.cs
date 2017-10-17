using EmployeePayslipCalculator.Models;
using EmployeePayslipCalculator.Service.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayslipCalculator.Service
{
    public sealed class TaxCalculatorFactory
    {
        static readonly TaxCalculatorFactory instance = new TaxCalculatorFactory();
        static TaxCalculatorFactory() { }
        TaxCalculatorFactory()
        {

        }

        public static TaxCalculatorFactory Instance
        {
            get
            {
                return instance;
            }
        }
        public TaxCalculatorBase CreateTaxCalculator(EmployeeInfo employee)
        {
            if (employee.AnnualSalary <= 0)
            {
                throw new Exception("wrong salary range!");
            }
            else if (employee.AnnualSalary <= 18200)
            {
                return new TaxCalculatorLevel1();
            }
            else if (employee.AnnualSalary <= 37000)
            {
                return new TaxCalculatorLevel2(employee.AnnualSalary);
            }
            else if (employee.AnnualSalary <= 80000)
            {
                return new TaxCalculatorLevel3(employee.AnnualSalary);
            }
            else if (employee.AnnualSalary <= 180000)
            {
                return new TaxCalculatorLevel4(employee.AnnualSalary);
            }
            else
            {
                return new TaxCalculatorLevel5(employee.AnnualSalary);
            }
        }
    }
}
