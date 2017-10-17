using System;
using EmployeePayslipCalculator.Models;

namespace EmployeePayslipCalculator.Service
{
    public class PayslipCalculator
    {
        public PayslipInfo Calculate(EmployeeInfo employee, DateTime datetime, double superRate)
        {
            PayslipInfo result = new PayslipInfo();
            result.Employee = employee;
            result.EmployeeId = employee.Id;
            result.PayPeriod = DateTime.Parse(datetime.ToString("yyyy-MM"));
            result.Id = Guid.NewGuid().ToString();
            result.GrossIncome = Utils.ConvertToInt(employee.AnnualSalary / 12);
            result.IncomeTax = TaxCalculatorFactory.Instance.CreateTaxCalculator(employee).CalculateTax();
            result.NetIncome = result.GrossIncome - result.IncomeTax;
            result.Super = Utils.ConvertToInt(result.GrossIncome * superRate);
            return result;
        }

        
    }
}
