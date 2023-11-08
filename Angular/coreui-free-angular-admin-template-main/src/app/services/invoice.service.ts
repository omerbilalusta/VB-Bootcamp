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

export class InvoiceService {

  constructor(private http:HttpClient) { }

  listAllAdmin():Observable<any>{
    return this.http.get(AUTH_API + 'Invoice/GetAllInvoices', httpOptions);
  }
  listAllService():Observable<any>{
    return this.http.get(AUTH_API + 'InvoiceService/GetAllInvoices', httpOptions);
  }
}
