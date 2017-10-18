import { EmployeeInfo } from './index';

export class PayslipInfo {
    Employee: EmployeeInfo;
    PayPeriod: string;
    GrossIncome: number;
    IncomeTax: number;
    NetIncome: number;
    Super: number;
}
