import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { ResponseResult } from '../../models/index';
import { GlobalContextService } from '../index';

/*
  Generated class for the HttpClientProvider provider.

  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
@Injectable()
export class HttpClientProvider {

  constructor(public http: Http, public globalContextService: GlobalContextService) {
    console.log('Hello HttpClientProvider Provider');
  }

  
  
    public get<T>(url): Promise<ResponseResult<T>> {
      return this.http.get(url).toPromise()
        .then(response => response.json() as ResponseResult<T>)
        .catch(this.handleError);
    }
  
    public post<T>(url, data: any): Promise<ResponseResult<T>> {
      return this.http.post(url, data).toPromise()
        .then(response => response.json() as ResponseResult<T>)
        .catch(this.handleError);
    }
  
    private handleError(error: any): Promise<any> {
      console.error('An error occurred', error);
      return Promise.reject(error.message || error);
    }

}
