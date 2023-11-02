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
  getById(id:number):Observable<any>{
    return this.http.get(AUTH_API + 'Product/GetProdutsById?Id=' + id, httpOptions);
  }
  getByCompanyFilter(id:number):Observable<any>{
    return this.http.get(AUTH_API + 'Product/filter?Id=' + id, httpOptions);
  }
  add(name:any, description:any, type:any, stockQuantity:number, price:number, taxRate:number):Observable<any>{
    return this.http.post(AUTH_API + 'Product', {
      name, description, type, stockQuantity, price, taxRate
    }, httpOptions);
  }
  delete(id:number):Observable<any>{
    console.log(id);
    return this.http.delete(AUTH_API + 'Product?Id='+ id, httpOptions);
  }
  update(id:number, name:any, description:any, type:any, stockQuantity:number, price:number, taxRate:number):Observable<any>{
    return this.http.put(AUTH_API + 'Product?Id=' + id, {
      name, description, type, stockQuantity, price, taxRate
    }, httpOptions);
  }
}
