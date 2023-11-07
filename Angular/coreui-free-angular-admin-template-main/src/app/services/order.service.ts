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
  getByOrderNumber(orderNumber:number):Observable<any>{
    return this.http.get(AUTH_API + 'Order/GetOrderByOrderNumber?orderNumber=' + orderNumber, httpOptions);
  }

  listByCompany():Observable<any>{
    return this.http.get(AUTH_API + 'Order/GetOrdersByCompany', httpOptions);
  }

  listByDealer():Observable<any>{
    return this.http.get(AUTH_API + 'Order/GetOrdersByDealer', httpOptions);
  }

  listDeclined():Observable<any>{
    return this.http.get(AUTH_API + 'Order/GetDeclinedOrders', httpOptions);
  }

  //Create
  createOrder(paymentMethod:any, productList:{ [key: string]: number; }):Observable<any>{
    console.log(paymentMethod);
    console.log(productList);
    return this.http.post(AUTH_API + 'Order', {paymentMethod, productList} ,httpOptions);
  }

  //Delete
  deleteOrder(ordernumber:number){
    return this.http.delete(AUTH_API + 'Order?orderNumber='+ ordernumber, httpOptions);
  }

  //Update Payment Method
  updatePaymentMethod(orderNumber:number, paymentMethod:string):Observable<any>{
    return this.http.put(AUTH_API + 'Order/UpdatePaymentMethod?orderNumber=' + orderNumber + '&paymentMethod=' + paymentMethod, httpOptions);
  }

  //Approve-Decline
  approveOrder(orderNumber:number){
    return this.http.put(AUTH_API + 'Order/companyapprove?orderNumber='+ orderNumber, httpOptions);
  }
  declineOrder(orderNumber:number, description:any){
    return this.http.put(AUTH_API + 'Order/companyapprove?orderNumber='+ orderNumber + '&description=' + description, httpOptions);
  }

  //Confirm Payment
  confirmPayment(orderNumber:number){
    return this.http.put(AUTH_API + 'Order/Pay?orderNumber='+ orderNumber, httpOptions);
  }

  //InvoiceDetails
  getInvoiceDetails():Observable<any>{
    return this.http.get(AUTH_API + 'InvoiceDetail', httpOptions);
  }

  //Pay
  pay(orderNumber:number){
    return this.http.put(AUTH_API + 'Order/Pay?orderNumber='+ orderNumber, httpOptions);
  }
  payWithOpenAccount(orderNumber:number){
    return this.http.put(AUTH_API + 'Order/PayOpenAccount?orderNumber='+ orderNumber, httpOptions);
  }
}
