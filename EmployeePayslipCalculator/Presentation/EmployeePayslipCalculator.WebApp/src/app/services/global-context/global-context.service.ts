import { Injectable } from '@angular/core';
import { Message } from 'primeng/primeng';

@Injectable()
export class GlobalContextService {
  /**API url */
  public server = 'http://localhost/Application.Client.Web.GateMonitor/api/';

  public CONFIGURATION = {
    baseUrls: {
      server: 'http://localhost/Application.Client.Web.GateMonitor'
    },
  };

  public messages: Message[] = [];


  constructor() {
  }

  public showSuccessMessage(detail: string) {
    // messages = [];
    this.messages.push({ severity: 'success', summary: 'Success', detail: detail });
  }

  public showInfoMessage(detail: string) {
    // messages = [];
    this.messages.push({ severity: 'info', summary: 'Info', detail: detail });
  }

  public showWarnMessage(detail: string) {
    // messages = [];
    this.messages.push({ severity: 'warn', summary: 'Warn', detail: detail });
  }

  public showErrorMessage(detail: string) {
    //  messages = [];
    this.messages.push({ severity: 'error', summary: 'Error', detail: detail });
  }

}
