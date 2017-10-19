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
  constructor(public navCtrl: NavController) {
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

  calculate(form: NgForm) {
    this.submitted = true;
    if (form.valid) {
    }
  }

}
