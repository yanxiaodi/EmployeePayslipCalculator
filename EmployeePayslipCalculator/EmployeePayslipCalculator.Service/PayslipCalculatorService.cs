using System;
using EmployeePayslipCalculator.Models;
using System.Globalization;

namespace EmployeePayslipCalculator.Service
{
    public class PayslipCalculatorService
    {
        public PayslipInfo Calculate(EmployeeInfo employee, int month)
        {
            PayslipInfo result = new PayslipInfo();
            result.Employee = employee;
            result.EmployeeId = employee.Id;
            result.PayPeriod = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            // Just for mock.
            result.Id = Guid.NewGuid().ToString();
            result.GrossIncome = Utils.ConvertToInt(employee.AnnualSalary / 12);
            result.IncomeTax = TaxCalculatorFactory.Instance.CreateTaxCalculator(employee).CalculateTax();
            result.NetIncome = result.GrossIncome - result.IncomeTax;
            result.Super = Utils.ConvertToInt(result.GrossIncome * employee.SuperRate);
            return result;
        }

        
    }
}
