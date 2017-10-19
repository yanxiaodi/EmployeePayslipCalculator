# EmployeePayslipCalculator

## Intruduction

Employee Payslip Calculator demo. It shows how to implement a Web API to calculate the employee payslip. The solution contains these projects:

* Domain
    * EmployeePayslipCalculator.Models
    * EmployeePayslipCalculator.Service
    * EmployeePayslipCalculator.Service.Test
* Service
    * EmployeePayslipCalculator.WebApi
    * EmployeePayslipCalculator.WebApi.Test
* Presentation
    * EmployeePayslipCalculator.WebApp
    * EmployeePayslipCalculator.MobileApp

According to the requirements, I need to calculate the employee salary by different rates. The core function for this goal is not very difficult, and I should use a simple factory to create different `TaxCalculator` class instances. But I would like to simulate the reality so I develope a web api based on the `Service` project. So I can use different clients, including the web app, the mobile app, and the WPF app, to call the api. In the same time, I must make sure all the codes to be robust, and have the ability to deal with any wrong inputs. I need test projects, and validate all the input value from the user. From my perspective, this demo should demonstrate some main coding principles, such as:

* Abstraction
* Inheritance
* Encapsulation
* Design Patterns
* Single Responsibility
* Separation of Concerns
* Dependency Injection
* Do not Repeat Yourself
* Testing
...

## Domain

The `Domain` folder contains the core models and business logic. 

### Models

`EmployeePayslipCalculator.Models` project is a very simple project. According the requirement, I use the `EmployeeInfo` class to indicate the Employee model, and the `PayslipInfo` to represent the Payslip model. I also create the `ResponseResult` class to encapsulate the response of the web api.

### Service

`EmployeePayslipCalculator.Service` project is the logic layer.

First, I creat a abstract class named `TaxCalculatorBase`, which receives a int value param to set the `AnnualSalary` property. The `CalculateTax` method is a virtual method so the derived classed must override it.

Then I create several different classes that inherit from the abstract base class in the `Impl` folder. For this purpose, I need to use a simple factory named `TaxCalculatorFactory` to create the instance, not create the specific instance in the caller class. So in the `PayslipCalculatorService` class, I can call the method in the instance class, as shown below:

`TaxCalculatorFactory.Instance.CreateTaxCalculator(employee.AnnualSalary).CalculateTax();`

If we need to add some other tax rate algorithm, I just add the new derived classes and modify the factory, and the code in the `PayslipCalculatorService` doesn't need to change. This is the benefit of the factory, which is the simplest design pattern, but very useful.

There is another thing to note. When I convert a double value to the int value, I need to use `Math.Round()` method. But be careful, we must pass the `MidpointRounding` enum as a param to ensure the accuracy, as shown below:

`int.TryParse(Math.Round(d, 0, MidpointRounding.AwayFromZero).ToString(), out result);
`

I use `int.TryParse()` method because it is more safe than `int.Parse()`.

For robustly, I also add some code for checking the value. If the param value is illegal, the service will throw a exception.

In the service code, I implement 2 method to calculate a single employee and a employees list. So it makes the service more flexible.


