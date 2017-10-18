import { EmployeeInfo } from './../../models';
import { Component, OnInit } from '@angular/core';
import { SpinnerModule } from 'primeng/primeng';
import { SelectItem } from 'primeng/primeng';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public employee: EmployeeInfo;
  public superRate: number;
  public months: SelectItem[];
  public selectedMonth: number;
  constructor() {
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

}
