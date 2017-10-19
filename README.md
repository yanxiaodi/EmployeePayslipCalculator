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

In the service code, I implement 2 methods to calculate a single employee and a employees list. So it makes the service more flexible.

The `EmployeePayslipCalculator.Service.Test` project is the unit test. I use xUnit framework, which is a good test framework for testing. I can create more test codes but for demonstration, it's enough.

In fact, I found a mistake by testing in my development process, since I typed a wrong number. So testing is very important for software development.

## Service

This folder contain 2 projects. 

`EmployeePayslipCalculator.WebApi` is a ASP.NET Core project. It expose a web api to the callers. Any clients can use this api through the HTTP protocal. This architecture is more and more popular in modern software development.

First, I use this code shown below to inject an instance of `PayslipCalculatorService` in the `ConfigureServices` method of `Startup` class:

`services.AddSingleton(typeof(PayslipCalculatorService), new PayslipCalculatorService());
`

So I'm able to use this service in the controller like this:

```
public CalculatorController(PayslipCalculatorService service)
        {
            this.service = service;
        }
```

This is a kind of Dependency Injection, we can call it `Constructor Injection`.

I create a Action named `Calculate` to receive the params, like this:

```
        [HttpPost]
        public IActionResult Calculate(int month, [FromBody] EmployeeInfo item)
        {
```

Another Action is for batch calculation:

```
        [HttpPost]
        public IActionResult BatchCalculate(int month, [FromBody] List<EmployeeInfo> items)
        {
```

I use `EmployeePayslipCalculator.WebApi.Test` project to test the api. By comparison to the `EmployeePayslipCalculator.Service.Test` project, I use Microsoft Test Framework to do the test. They have many similarities and all of them are easy to use.

In our real projects, we should use log system for the api. But this is just a demo, so I didn't use log. If the burden of the api become more heavy, we should scale up or scale out the api to face a great many requests. We can also use MQ, like RabbitMQ, to improve the reliability of the api.

## Presentation

I would like to use some different client apps to call the api, including the web app, mobile app, and the WPF app.

### Web App

`EmployeePayslipCalculator.WebApp` is an Angular project. I like Angular and TypeScript since they can improve my work efficiency.

An Angular app must have some resonable layers design. I create a service named `HttpClientService` to encapsulate the HTTP api of Angular. So I can easily get a response model from the api. The main service is the `CalculatorService` class. It's very simple that just contains a few codes.

The challenge for the front-end app is dealing with the inputs from the users. That's a crucial factor to determine whether the app is robust. I create a single method named `checkInput()` to check the values except the validators of ngForm.

I use PrimeNG for the UI presentation. It is a good UI framework for Angular app and easy to use, just like jQuery UI.

### Mobile App

Cross-Platform development is more and more popular now. I use Ionic Framework to create the Cordova app. It's elegant and very easy to use. I reuse many codes of `EmployeePayslipCalculator.WebApp`, such as models, services, and check methods, etc. 

You can compile it for different platforms, including iOS, Android, UWP, or just run it as a web app in the browser.





