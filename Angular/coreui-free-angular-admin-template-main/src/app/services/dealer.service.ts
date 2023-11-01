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
  add(params:any):Observable<any>{
    return this.http.post(AUTH_API + 'Dealer', params, httpOptions);
  }
}
