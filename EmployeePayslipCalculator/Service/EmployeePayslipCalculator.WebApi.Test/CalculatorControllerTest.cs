using EmployeePayslipCalculator.Models;
using EmployeePayslipCalculator.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmployeePayslipCalculator.WebApi.Test
{
    [TestClass]
    public class CalculatorControllerTest
    {
        [TestMethod]
        public void CalculateTest()
        {
            // I can create more tests here but for demonstration, it's enough.
            CalculatorController controller = new CalculatorController(new Service.PayslipCalculatorService());

            EmployeeInfo employee = new EmployeeInfo();
            employee.AnnualSalary = 60050;
            employee.FirstName = "David";
            employee.LastName = "Rudd";
            employee.SuperRate = 0.09;
            var response = controller.Calculate(3, employee) as OkObjectResult;
            var result = response.Value as ResponseResult<PayslipInfo>;
            Assert.AreEqual("David Rudd", result.Result.Employee.FullName);
            Assert.AreEqual(5004, result.Result.GrossIncome);
            Assert.AreEqual(922, result.Result.IncomeTax);
            Assert.AreEqual(4082, result.Result.NetIncome);
            Assert.AreEqual(450, result.Result.Super);


            EmployeeInfo employee2 = new EmployeeInfo();
            employee2.AnnualSalary = 120000;
            employee2.FirstName = "Ryan";
            employee2.LastName = "Chen";
            employee2.SuperRate = 0.10;
            var response2 = controller.Calculate(3, employee2) as OkObjectResult;
            var result2 = response2.Value as ResponseResult<PayslipInfo>;
            Assert.AreEqual("Ryan Chen", result2.Result.Employee.FullName);
            Assert.AreEqual(10000, result2.Result.GrossIncome);
            Assert.AreEqual(2696, result2.Result.IncomeTax);
            Assert.AreEqual(7304, result2.Result.NetIncome);
            Assert.AreEqual(1000, result2.Result.Super);
        }
    }
}
