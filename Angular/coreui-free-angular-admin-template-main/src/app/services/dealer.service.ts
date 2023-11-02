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
export class DealerService {

  constructor(private http:HttpClient) { }

  list():Observable<any>{
    return this.http.get(AUTH_API + 'Dealer/GetAllDealer', httpOptions);
  }
  getById(id:number):Observable<any>{
    return this.http.get(AUTH_API + 'Dealer/GetDealerById?id=' + id, httpOptions);
  }
  add(name:any, email:any, password:any, address:any, invoiceaddress:any, dividend:number, openaccountlimit:number):Observable<any>{
    return this.http.post(AUTH_API + 'Dealer', {name, email, password, address, invoiceaddress, dividend, openaccountlimit}, httpOptions);
  }
  updateShort(dividend:number, openaccountlimit:number):Observable<any>{
    return this.http.put(AUTH_API + 'Dealer/dealerUpdate?id=', {dividend, openaccountlimit}, httpOptions);
  }
  delete(id:number):Observable<any>{
    return this.http.delete(AUTH_API + 'Dealer/?id=' + id, httpOptions);
  }
}
