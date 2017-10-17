using System;
using EmployeePayslipCalculator.Models;
using System.Globalization;
using System.Collections.Generic;

namespace EmployeePayslipCalculator.Service
{
    public class PayslipCalculatorService
    {
        public PayslipInfo Calculate(EmployeeInfo employee, int month)
        {
            PayslipInfo result = new PayslipInfo();
            result.Employee = employee;
            result.PayPeriod = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            result.GrossIncome = Utils.ConvertToInt(employee.AnnualSalary / 12);
            result.IncomeTax = TaxCalculatorFactory.Instance.CreateTaxCalculator(employee).CalculateTax();
            result.NetIncome = result.GrossIncome - result.IncomeTax;
            result.Super = Utils.ConvertToInt(result.GrossIncome * employee.SuperRate);
            return result;
        }

        public List<PayslipInfo> Calculate(List<EmployeeInfo> employees, int month)
        {
            List<PayslipInfo> result = new List<PayslipInfo>();
            employees.ForEach(x =>
            {
                var y = this.Calculate(x, month);
                result.Add(y);
            });
            return result;
        }





    }
}
