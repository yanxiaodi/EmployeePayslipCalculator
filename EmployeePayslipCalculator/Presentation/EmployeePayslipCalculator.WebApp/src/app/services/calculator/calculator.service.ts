import { HttpClientService } from './../http-client/http-client.service';
import { GlobalContextService } from './../global-context/global-context.service';
import { ResponseResult, EmployeeInfo, PayslipInfo } from './../../models';
import { Injectable } from '@angular/core';

@Injectable()
export class CalculatorService {

  constructor(public globalContext: GlobalContextService, public httpClient: HttpClientService) { }

  public async Calculate(month: number, employee: EmployeeInfo) {
    const url = `${this.globalContext.server}Calculator/Calculate?month=${month}`;
    return await this.httpClient.post<PayslipInfo>(url, employee);
  }

}
