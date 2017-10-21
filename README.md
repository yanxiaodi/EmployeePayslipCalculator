# EmployeePayslipCalculator

## Introduction

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
    * EmployeePayslipCalculator.WPFApp

According to the requirements, I need to calculate the employee salary by different rates. The core function for this goal is not very difficult, and I should use a simple factory to create different `TaxCalculator` class instances. But I would like to simulate the reality so I developed a web api based on the `Service` project. So I can use different clients, including the web app, the mobile app, and the WPF app, to call the api. In the same time, I must make sure all the codes to be robust, and have the ability to deal with any wrong inputs. I need test projects, and validate all the input value from the user. From my perspective, this demo should demonstrate some main coding principles, such as:

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

## How to run the solution

### Run the Web API

Go into the `Service\EmployeePayslipCalculator.WebApi` folder, and run the command below:

`dotnet restore`

Then input this command to build the web api project:

`dotnet build`

Use this command to run the web api:

`dotnet run`

Now you can open the url: 

`http://localhost:58258/api/values`

If the server is running correctly, you should receive a json string like this:

```
[
"value1",
"value2"
]
```

### Run the web app

First go into the `Presentation\EmployeePayslipCalculator.WebApp` folder and run this command:

`npm install`

Then input this command blow:

`ng serve`

You can open the url:

`http://localhost:4200`

to test the angular app.

### Run the cordova app

Go into the `Presentation\EmployeePayslipCalculator.MobileApp` folder, and run this command:

`npm install`

Now input this command to start the ionic app:

`ionic serve`

You can navigate to this url to test the cordova app:

`http://localhost:8100/`

You can adjust the chrome style through the developer tool to get the best effect.

If you want to compile it to iOS or Android, please reference the ionic documents:

[Ionic CLI](http://ionicframework.com/docs/cli/)

### Run the wpf app

Just compile it and run it.

### Caution

You must config the api address if you change the default web api host.

For `EmployeePayslipCalculator.WebApp` project, please check the `global-context.service.ts` file in the `Presentation\EmployeePayslipCalculator.WebApp\src\app\services\global-context` folder.

For `EmployeePayslipCalculator.MobileApp` project, please check the `global-context.service.ts` file in the `Presentation\EmployeePayslipCalculator.MobileApp\src\providers\global-context` folder.

For `EmployeePayslipCalculator.WPFApp` project, you can modify the server url in the `App.config`.

## Domain

The `Domain` folder contains the core models and business logic. 

### Models

`EmployeePayslipCalculator.Models` project is a very simple project. According to the requirement, I use the `EmployeeInfo` class to indicate the Employee model, and the `PayslipInfo` to represent the Payslip model. I also create the `ResponseResult` class to encapsulate the response of the web api.

### Service

`EmployeePayslipCalculator.Service` project is the logic layer.

First I create an abstract class named `TaxCalculatorBase`, which receives an int value param to set the `AnnualSalary` property. The `CalculateTax` method is a virtual method so the derived classed must override it.

Then I create several different classes that inherit from the abstract base class in the `Impl` folder. For this purpose, I need to use a simple factory named `TaxCalculatorFactory` to create the instance, not create the specific instance in the caller class. So in the `PayslipCalculatorService` class, I can call the method in the instance class, as shown below:

`TaxCalculatorFactory.Instance.CreateTaxCalculator(employee.AnnualSalary).CalculateTax();`

If we need to add some other tax rate algorithm, I just add the new derived classes and modify the factory, and the code in the `PayslipCalculatorService` does not need to change. This is the benefit of the factory, which is the simplest design pattern, but very useful.

There is another thing to note. When I convert a double value to the int value, I need to use `Math.Round()` method. But be careful, we must pass the `MidpointRounding` enum as a param to ensure the accuracy, as shown below:

`int.TryParse(Math.Round(d, 0, MidpointRounding.AwayFromZero).ToString(), out result);
`

I prefer to use `int.TryParse()` method because it is more safe than `int.Parse()`.

To make the programe more robust, I also add some code for checking the value. If the param value is illegal, the service will throw an exception, then the caller will catch the exception and deal with it.

In the service code, I implement two methods to calculate a single employee and an employees list. So it makes the service more flexible.

The `EmployeePayslipCalculator.Service.Test` project is the unit test. I use xUnit framework, which is a good test framework for testing. I can create more test codes but for demonstration, it is enough.

In fact, I found a mistake by testing in my development process, since I typed a wrong number. So testing is very important for software development.

## Service

This folder contains two projects. 

`EmployeePayslipCalculator.WebApi` is an ASP.NET Core project. It exposes a web api to the callers. Any clients can use this api through the HTTP protocol. This architecture is more and more popular in modern software development.

First, I use this code shown below to inject an instance of `PayslipCalculatorService` in the `ConfigureServices` method of `Startup` class:

`services.AddSingleton(typeof(PayslipCalculatorService), new PayslipCalculatorService());
`

So I am able to use this service in the controller like this:

```
public CalculatorController(PayslipCalculatorService service)
        {
            this.service = service;
        }
```

This is a kind of Dependency Injection, we can call it `Constructor Injection`.

I create an Action named `Calculate` to receive the params, like this:

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

In our real projects, we should use the log system for the api. But this is just a demo, so I did not use the log. If the burden of the api become heavier, we should scale up or scale out the api to face a great many requests. We can also use the MQ, such as RabbitMQ, to improve the reliability of the api.

## Presentation

I would like to use some different client apps to call the api, including the web app, mobile app, and the WPF app.

### Web App

`EmployeePayslipCalculator.WebApp` is an Angular project. I preter to use Angular and TypeScript since they are efficient.

An Angular app must have some reasonable layers design. I create a service named `HttpClientService` to encapsulate the HTTP api of Angular. So I can easily get a response model from the api. The main service is the `CalculatorService` class. It is very simple that just contains a few codes.

The challenge for the front-end app is dealing with the inputs from the users. That is a crucial factor to determine whether the app is robust. I create a single method named `checkInput()` to check the values except for the validators of ngForm.

I use PrimeNG for the UI presentation. It is a good UI framework for the Angular apps and easy to use, just like jQuery UI.

### Mobile App

Cross-Platform development is more and more popular now. I use Ionic Framework to create the Cordova app. It is elegant and very easy to use. I reuse many codes of `EmployeePayslipCalculator.WebApp`, such as models, services, and check methods, etc. 

You can compile it for different platforms, including iOS, Android, UWP, or just run it as a web app in the browser.

### WPF App

To demonstrate how to calculate the result from the excel file, I also create a WPF application named `EmployeePayslipCalculator.WPFApp`. I use MVVMSidekick to implement the MVVM pattern. I want to show how to read an excel file to get the source data, and calculate the result from the local codes and the web api.

First, you need to select the path of the excel file, which must follow the format of the template file that is in the Template folder. Do not modify the format of the excel file, otherwise, you will not get the correct result. You are allowed to input a lot of employees in the excel file. But in fact, we should set a limit. I did not do it since it is just a demo.

Then please select the output file path and the salary month.

To deal with the excel file, I use NPOI.NET to read and write the excel files. I encapsulate these methods to handle the excel files:

`private IWorkbook ReadFile(string filePath)`

`private void GenerateResult(List<PayslipInfo> list)`

 You can see two buttons here. The first button is responsible for calculating the result through the `EmployeePayslipCalculator.Service` project. That is similar with the `EmployeePayslipCalculator.WebApi` project.

The second button can get the result through the web api, which must be run first. I write the `WebApiService` class to simplify the use of the HttpClient, which can directly get the result as the correct type. The generic type is necessary for this requirement. I use JSON.NET to parse the json object.

## Summary

This demo is not a difficult problem to solve but I must be careful to make a better application. To be a professional developer, it is not enough to just solve the main problem. We must design the architecture to make it can response the new requirement quickly. For example, to deal with the potential tax rate changes, we must use the factory pattern to separate the core business code from the caller code.

The unit test can confirm our code can run the way as we expect. They also reduce the risk for the changing and refactoring. I just write some simple unit tests for the services project because this is a demo. In fact, TDD is widely used in modern software development. Maybe we need much time at the beginning, but it is worth to do it.

We also need to make our application more beautiful and friendly. You can see that I use several different UI frameworks for different applications. For simplify the operations for the users, I should choose the suitable controls. User experience is more and more important nowadays. Fortunately, we have a lot of libraries and framework to help us.

Another important factor is that make sure our apps can handle all potential wrong inputs from the users. To gain this goal, we must check all params from the users, and throw messages to let the users to know what wrong happen. The explicit toast is neccesary for the client users.

I think this demo is a clean code, and it is easy to understand, and has a reasonable architecture. Anyway, I should do more jobs to make it to be a excellent app to solve the problem, but I am too busy these days. If you have any questions about this demo, let me know please.

Thank you very much.















