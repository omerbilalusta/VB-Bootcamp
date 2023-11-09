import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
const AUTH_API = 'http://localhost:5082/api/';
const httpOptions = {
  headers : new HttpHeaders({'Content-Type':'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(private http:HttpClient) { }

  getByDate(dateFrom:any, dateTo:any):Observable<any>{
    return this.http.get(AUTH_API + 'Report/GetReportByDate?dateFrom=' + dateFrom + '&dateTo=' + dateTo, httpOptions);
  }
}
