using EmployeePayslipCalculator.Models;
using System;
using Xunit;

namespace EmployeePayslipCalculator.Service.Test
{
    public class PayslipCalculatorServiceTest
    {
        [Fact]
        public void CalculateSingleTest()
        {
            // I can create more tests here but for demonstration, it's enough.
            EmployeeInfo employee = new EmployeeInfo();
            employee.AnnualSalary = 60050;
            employee.FirstName = "Jack";
            employee.LastName = "Smith";
            employee.SuperRate = 0.09;
            PayslipCalculatorService service = new PayslipCalculatorService();
            var result = service.Calculate(employee, 3);
            Assert.Equal(5004, result.GrossIncome);
            Assert.Equal(922, result.IncomeTax);
            Assert.Equal(4082, result.NetIncome);
            Assert.Equal(450, result.Super);
        }

    }
}
