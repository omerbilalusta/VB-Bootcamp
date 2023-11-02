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
export class OrderService {

  constructor(private http:HttpClient) { }

  //List
  listAll():Observable<any>{
    return this.http.get(AUTH_API + 'Order/GetAllOrder', httpOptions);
  }
  listByOrderNumber(orderNumber:number):Observable<any>{
    return this.http.get(AUTH_API + 'Order/GetOrderByOrderNumber?orderNumber=' + orderNumber, httpOptions);
  }

  listByCompanyDealer():Observable<any>{
    return this.http.get(AUTH_API + 'Order/GetOrdersByCompanyDealer', httpOptions);
  }

  listDeclined():Observable<any>{
    return this.http.get(AUTH_API + 'Order/GetDeclinedOrders', httpOptions);
  }

  //Create
  createOrder(){
    return this.http.post(AUTH_API + 'Order', {} ,httpOptions);
  }

  //Delete
  deleteOrder(ordernumber:number){
    return this.http.delete(AUTH_API + 'Order?orderNumber='+ ordernumber, httpOptions);
  }

  //Approve-Decline
  approveOrder(id:number){
    return this.http.put(AUTH_API + 'Order/companyapprove?Id='+ id, httpOptions);
  }
  declineOrder(id:number, description:any){
    return this.http.put(AUTH_API + 'Order/companyapprove?Id='+ id + '&description=' + description, httpOptions);
  }

  //InvoiceDetails
  // getInvoiceDetails(id:number):Observable<any>{
  //   return this.http.get(AUTH_API + 'Order/GetInvoiceDetails?Id=' + id, httpOptions);
  // }
}
