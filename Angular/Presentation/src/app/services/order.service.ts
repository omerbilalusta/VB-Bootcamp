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
  getByOrderNumberAdmin(orderNumber:number):Observable<any>{
    return this.http.get(AUTH_API + 'Order/GetOrderByOrderNumber?orderNumber=' + orderNumber, httpOptions);
  }

  getByOrderNumberService(orderNumber:number):Observable<any>{
    return this.http.get(AUTH_API + 'OrderService/GetOrderByOrderNumber?orderNumber=' + orderNumber, httpOptions);
  }

  listByCompany():Observable<any>{
    return this.http.get(AUTH_API + 'Order/GetOrdersByCompany', httpOptions);
  }

  listByDealerService():Observable<any>{
    return this.http.get(AUTH_API + 'OrderService/GetOrdersByDealer', httpOptions);
  }

  listDeclinedAdmin():Observable<any>{
    return this.http.get(AUTH_API + 'Order/GetDeclinedOrders', httpOptions);
  }

  listDeclinedService():Observable<any>{
    return this.http.get(AUTH_API + 'OrderService/GetDeclinedOrders', httpOptions);
  }

  //Create
  createOrderService(paymentMethod:any, productList:{ [key: string]: number; }):Observable<any>{
    console.log(paymentMethod);
    console.log(productList);
    return this.http.post(AUTH_API + 'OrderService', {paymentMethod, productList} ,httpOptions);
  }

  //Delete
  deleteOrderAdmin(ordernumber:number){
    return this.http.delete(AUTH_API + 'Order?orderNumber='+ ordernumber, httpOptions);
  }
  deleteOrderService(ordernumber:number){
    return this.http.delete(AUTH_API + 'OrderService?orderNumber='+ ordernumber, httpOptions);
  }

  //Update Payment Method
  updatePaymentMethodService(orderNumber:number, paymentMethod:string):Observable<any>{
    return this.http.put(AUTH_API + 'OrderService/UpdatePaymentMethod?orderNumber=' + orderNumber + '&paymentMethod=' + paymentMethod, httpOptions);
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
    return this.http.get(AUTH_API + 'OrderDetail', httpOptions);
  }

  //Pay
  payService(orderNumber:number){
    return this.http.put(AUTH_API + 'OrderService/Pay?orderNumber='+ orderNumber, httpOptions);
  }
  payWithOpenAccountService(orderNumber:number){
    return this.http.put(AUTH_API + 'OrderService/PayOpenAccount?orderNumber='+ orderNumber, httpOptions);
  }
}
