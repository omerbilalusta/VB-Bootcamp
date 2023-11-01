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
export class ProductService {

  constructor(private http:HttpClient) { }

  list():Observable<any>{
    return this.http.get(AUTH_API + 'Product/GetAllProducts', httpOptions);
  }
  add(name:any, description:any, type:any, stockQuantity:number, price:number, taxRate:number):Observable<any>{
    return this.http.post(AUTH_API + 'Product', {
      name, description, type, stockQuantity, price, taxRate
    }, httpOptions);
  }
  delete(id:number):Observable<any>{
    return this.http.delete(AUTH_API + 'Product?Id'+ id, httpOptions);
  }
}
