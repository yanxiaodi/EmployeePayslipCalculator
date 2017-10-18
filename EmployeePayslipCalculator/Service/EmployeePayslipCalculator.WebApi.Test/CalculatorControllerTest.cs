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
            EmployeeInfo employee = new EmployeeInfo();
            employee.AnnualSalary = 60050;
            employee.FirstName = "Jack";
            employee.LastName = "Smith";
            employee.SuperRate = 0.09;
            CalculatorController controller = new CalculatorController(new Service.PayslipCalculatorService());
            var response = controller.Calculate(3, employee) as OkObjectResult;
            var result = response.Value as ResponseResult<PayslipInfo>;
            Assert.AreEqual(5004, result.Result.GrossIncome);
            Assert.AreEqual(922, result.Result.IncomeTax);
            Assert.AreEqual(4082, result.Result.NetIncome);
            Assert.AreEqual(450, result.Result.Super);
        }
    }
}
