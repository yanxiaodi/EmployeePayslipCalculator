import { PayslipInfo } from './../../models/payslipInfo';
import { async } from '@angular/core/testing';
import { CalculatorService, GlobalContextService } from './../../services';
import { EmployeeInfo } from './../../models';
import { Component, OnInit } from '@angular/core';
import { SpinnerModule } from 'primeng/primeng';
import { SelectItem, Message } from 'primeng/primeng';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public messages: Message[] = [];
  public employee: EmployeeInfo;
  public superRate: number;
  public months: SelectItem[];
  public selectedMonth: number;
  public payslipInfo: PayslipInfo;
  constructor(public calculatorService: CalculatorService, public globalContextService: GlobalContextService) {
    this.messages = this.globalContextService.messages;
    this.employee = new EmployeeInfo();
    this.months = [];
    this.months.push({ label: 'January', value: 1 });
    this.months.push({ label: 'February ', value: 2 });
    this.months.push({ label: 'March', value: 3 });
    this.months.push({ label: 'April', value: 4 });
    this.months.push({ label: 'May', value: 5 });
    this.months.push({ label: 'June', value: 6 });
    this.months.push({ label: 'July', value: 7 });
    this.months.push({ label: 'August', value: 8 });
    this.months.push({ label: 'September', value: 9 });
    this.months.push({ label: 'October', value: 10 });
    this.months.push({ label: 'November', value: 11 });
    this.months.push({ label: 'December', value: 12 });
  }

  ngOnInit() {
  }

  async calculate() {
    const isValid = this.checkInput();
    if (isValid) {
      const result = await this.calculatorService.Calculate(this.selectedMonth, this.employee);
      if (result.isSuccess) {
        this.payslipInfo = result.result;
        console.log(this.payslipInfo);
      } else {
        this.globalContextService.showErrorMessage('Errors!' + result.message);
      }
    }
  }

  checkInput(): boolean {
    let isValid = true;
    if (this.employee.firstName === '' || this.employee.firstName === null || this.employee.firstName === undefined) {
      this.globalContextService.showWarnMessage('Please input the first name!');
      return false;
    } else if (this.employee.lastName === '' || this.employee.lastName === null || this.employee.lastName === undefined) {
      this.globalContextService.showWarnMessage('Please input the last name!');
      return false;
    }
    try {
      this.employee.annualSalary = Number.parseInt(this.employee.annualSalary.toString());
      if (this.employee.annualSalary <= 0) {
        this.globalContextService.showWarnMessage('The annual salary must be more than 0!');
        return false;
      }
    } catch (ex) {
      isValid = false;
      this.globalContextService.showWarnMessage('Please input a valid number for annual salary!');
      console.log(ex);
    }
    try {
      this.employee.superRate = Number.parseFloat(this.superRate.toString()) / 100;
      if (this.employee.superRate < 0 || this.employee.superRate > 0.5) {
        this.globalContextService.showWarnMessage('The super rate must be between 0 and 0.5!');
        return false;
      }
    } catch (ex) {
      isValid = false;
      this.globalContextService.showWarnMessage('Please input a valid number for super rate!');
      console.log(ex);
      return;
    }
    console.log(this.employee);
    console.log(this.selectedMonth);
    return isValid;
  }

}
