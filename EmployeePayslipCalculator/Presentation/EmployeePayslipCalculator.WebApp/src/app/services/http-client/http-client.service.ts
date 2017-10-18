import { GlobalContextService } from './../global-context/global-context.service';
import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { ResponseResult } from '../../models';

@Injectable()
export class HttpClientService {

  constructor(public http: Http, public globalContextService: GlobalContextService) { }


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
