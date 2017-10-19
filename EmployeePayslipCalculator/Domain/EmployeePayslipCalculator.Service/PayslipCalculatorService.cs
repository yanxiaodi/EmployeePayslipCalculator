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
            if(employee.AnnualSalary < 0)
            {
                throw new Exception("The annual salary must be more than 0!");
            }
            else if(employee.SuperRate < 0 || employee.SuperRate > 0.5)
            {
                throw new Exception("The super rate must be between 0 and 0.5!");
            }
            else if(month < 1 || month > 12)
            {
                throw new Exception("The month must be between 1 and 12!");
            }
            PayslipInfo result = new PayslipInfo();
            result.Employee = employee;
            result.PayPeriod = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            result.GrossIncome = Utils.ConvertToInt(employee.AnnualSalary / 12);
            result.IncomeTax = TaxCalculatorFactory.Instance.CreateTaxCalculator(employee.AnnualSalary).CalculateTax();
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
