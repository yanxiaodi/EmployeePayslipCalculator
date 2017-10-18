import { EmployeeInfo } from './index';

export class PayslipInfo {
    employee: EmployeeInfo;
    payPeriod: string;
    grossIncome: number;
    incomeTax: number;
    netIncome: number;
    super: number;
}
