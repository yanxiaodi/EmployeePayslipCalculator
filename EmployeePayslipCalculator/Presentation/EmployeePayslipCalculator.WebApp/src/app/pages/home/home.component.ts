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

  calculate() {
    console.log(this.employee);
    if (this.employee.FirstName === '' || this.employee.FirstName === null || this.employee.FirstName === undefined) {
      this.globalContextService.showWarnMessage('Please input the first name!');
      return;
    } else if (this.employee.LastName === '' || this.employee.LastName === null || this.employee.LastName === undefined) {
      this.globalContextService.showWarnMessage('Please input the last name!');
      return;
    }
    try {
      this.employee.AnnualSalary = Number.parseInt(this.employee.AnnualSalary.toString());
      if (this.employee.AnnualSalary <= 0) {
        this.globalContextService.showWarnMessage('The annual salary must be more than 0!');
        return;
      }
    } catch (ex) {
      this.globalContextService.showWarnMessage('Please input a valid number for annual salary!');
      console.log(ex);
      return;
    }
    try {
      this.employee.SuperRate = Number.parseFloat(this.superRate.toString()) / 100;
      if (this.employee.SuperRate < 0 || this.employee.SuperRate > 0.5) {
        this.globalContextService.showWarnMessage('The super rate must be between 0 and 0.5!');
      }
    } catch (ex) {
      this.globalContextService.showWarnMessage('Please input a valid number for super rate!');
      console.log(ex);
      return;
    }
    console.log(this.employee);
  }

}
