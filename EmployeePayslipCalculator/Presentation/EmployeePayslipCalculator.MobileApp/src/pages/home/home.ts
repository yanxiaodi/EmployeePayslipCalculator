import { ShowResultPage } from './../show-result/show-result';
import { GlobalContextService, CalculatorService } from './../../providers/';
import { EmployeeInfo, PayslipInfo } from '../../models';
import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {
  public submitted = false;
  public employee: EmployeeInfo;
  public superRate: number;
  public months: Array<any>;
  public selectedMonth: any;
  public payslipInfo: PayslipInfo;
  constructor(public navCtrl: NavController, public globalContextService: GlobalContextService, public calculatorService: CalculatorService) {
    this.employee = new EmployeeInfo();
    this.setMonths();
  }

  setMonths() {
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
    this.selectedMonth = 1;
  }

  async calculate(form: NgForm) {
    this.submitted = true;
    if (form.valid) {
      const isValid = this.checkInput();
      if (isValid) {
        try {

          const result = await this.calculatorService.Calculate(this.selectedMonth, this.employee);

          if (result.isSuccess) {
            this.navCtrl.push(ShowResultPage, result.result);
          } else {
            this.globalContextService.showToast(result.message);
          }
        } catch (error) {
          this.globalContextService.showToast(error);
        }
      }
    }
  }

  checkInput(): boolean {
    const isValid = true;
    if (this.employee.firstName === '' || this.employee.firstName === null || this.employee.firstName === undefined) {
      this.globalContextService.showToast('Please input the first name!');
      return false;
    } else if (this.employee.lastName === '' || this.employee.lastName === null || this.employee.lastName === undefined) {
      this.globalContextService.showToast('Please input the last name!');
      return false;
    }

    if (Number.isInteger(Number.parseInt(this.employee.annualSalary.toString()))) {
      this.employee.annualSalary = Number.parseInt(this.employee.annualSalary.toString());
      if (this.employee.annualSalary <= 0) {
        this.globalContextService.showToast('The annual salary must be more than 0!');
        return false;
      }
    } else {
      this.globalContextService.showToast('Please input a valid number for annual salary!');
      return false;
    }
    if (!Number.isNaN(Number.parseFloat(this.superRate.toString()))) {
      this.employee.superRate = Number.parseFloat(this.superRate.toString()) / 100;
      if (this.employee.superRate < 0 || this.employee.superRate > 0.5) {
        this.globalContextService.showToast('The super rate must be between 0 and 0.5!');
        return false;
      }
    } else {
      this.globalContextService.showToast('Please input a valid number for super rate!');
      return false;
    }
    return isValid;
  }

}
