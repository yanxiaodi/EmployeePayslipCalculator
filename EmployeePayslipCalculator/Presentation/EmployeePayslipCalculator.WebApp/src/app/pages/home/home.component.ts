import { PayslipInfo } from './../../models/payslipInfo';
import { async } from '@angular/core/testing';
import { CalculatorService, GlobalContextService } from './../../services';
import { EmployeeInfo } from './../../models';
import { Component, OnInit } from '@angular/core';
import { SpinnerModule } from 'primeng/primeng';
import { SelectItem, Message } from 'primeng/primeng';
import { NgForm, Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  userform: FormGroup;
  submitted: boolean;
  public messages: Message[] = [];
  public employee: EmployeeInfo;
  public superRate: number;
  public months: SelectItem[];
  public selectedMonth: number;
  public payslipInfo: PayslipInfo;
  constructor(private fb: FormBuilder, public calculatorService: CalculatorService, public globalContextService: GlobalContextService) {
    this.messages = this.globalContextService.messages;
    this.employee = new EmployeeInfo();
    this.months = [];
    this.initMonths();
  }

  ngOnInit() {
    this.userform = this.fb.group({
      'firstNameControl': new FormControl('', Validators.required),
      'lastNameControl': new FormControl('', Validators.required),
      'annualSalaryControl': new FormControl('', Validators.compose([Validators.required, Validators.min(0)])),
      'superRateControl': new FormControl('', Validators.compose([Validators.required, Validators.min(0), Validators.max(50)])),
      'selectedMonthControl': new FormControl('', Validators.required)
    });
  }

  initMonths() {
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
    this.selectedMonth = 1;
  }

  async onSubmit() {
    this.submitted = true;
    const isValid = this.checkInput();
    if (isValid) {
      try {
        const result = await this.calculatorService.Calculate(this.selectedMonth, this.employee);
        if (result.isSuccess) {
          this.payslipInfo = result.result;
          console.log(this.payslipInfo);
        } else {
          this.globalContextService.showErrorMessage('Errors!' + result.message);
        }
      } catch (ex) {
        this.globalContextService.showErrorMessage('Errors!' + ex);
      }
    }
  }

  checkInput(): boolean {
    const isValid = true;
    if (this.employee.firstName === '' || this.employee.firstName === null || this.employee.firstName === undefined) {
      this.globalContextService.showWarnMessage('Please input the first name!');
      return false;
    } else if (this.employee.lastName === '' || this.employee.lastName === null || this.employee.lastName === undefined) {
      this.globalContextService.showWarnMessage('Please input the last name!');
      return false;
    }

    if (Number.isInteger(Number.parseInt(this.employee.annualSalary.toString()))) {
      this.employee.annualSalary = Number.parseInt(this.employee.annualSalary.toString());
      if (this.employee.annualSalary <= 0) {
        this.globalContextService.showWarnMessage('The annual salary must be more than 0!');
        return false;
      }
    } else {
      this.globalContextService.showWarnMessage('Please input a valid number for annual salary!');
      return false;
    }
    if (!Number.isNaN(Number.parseFloat(this.superRate.toString()))) {
      this.employee.superRate = Number.parseFloat(this.superRate.toString()) / 100;
      if (this.employee.superRate < 0 || this.employee.superRate > 0.5) {
        this.globalContextService.showWarnMessage('The super rate must be between 0 and 0.5!');
        return false;
      }
    } else {
      this.globalContextService.showWarnMessage('Please input a valid number for super rate!');
      return false;
    }
    return isValid;
  }

}
