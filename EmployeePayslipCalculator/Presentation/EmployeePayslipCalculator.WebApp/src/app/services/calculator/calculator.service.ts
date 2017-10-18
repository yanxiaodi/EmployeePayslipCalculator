import { Injectable } from '@angular/core';
import { GlobalContextService, HttpClientService } from '../index';

@Injectable()
export class CalculatorService {

  constructor(public globalContext: GlobalContextService, public httpClient: HttpClientService) { }

  public async getGateList() {
    const url = `${this.globalContext.server}GateMonitorApi/GetGateList`;
    return await this.httpClient.get<Array<TGateInfo>>(url);
  }

}
