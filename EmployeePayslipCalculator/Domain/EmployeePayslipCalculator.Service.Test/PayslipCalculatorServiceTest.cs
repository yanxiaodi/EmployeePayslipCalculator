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
            PayslipCalculatorService service = new PayslipCalculatorService();

            EmployeeInfo employee = new EmployeeInfo();
            employee.AnnualSalary = 60050;
            employee.FirstName = "David";
            employee.LastName = "Rudd";
            employee.SuperRate = 0.09;
            var result = service.Calculate(employee, 3);
            Assert.Equal("David Rudd", result.Employee.FullName);
            Assert.Equal(5004, result.GrossIncome);
            Assert.Equal(922, result.IncomeTax);
            Assert.Equal(4082, result.NetIncome);
            Assert.Equal(450, result.Super);

            EmployeeInfo employee2 = new EmployeeInfo();
            employee2.AnnualSalary = 120000;
            employee2.FirstName = "Ryan";
            employee2.LastName = "Chen";
            employee2.SuperRate = 0.10;
            var result2 = service.Calculate(employee2, 3);
            Assert.Equal("Ryan Chen", result2.Employee.FullName);
            Assert.Equal(10000, result2.GrossIncome);
            Assert.Equal(2696, result2.IncomeTax);
            Assert.Equal(7304, result2.NetIncome);
            Assert.Equal(1000, result2.Super);
        }

    }
}
