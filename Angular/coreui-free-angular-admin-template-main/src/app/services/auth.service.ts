import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StorageService } from './storage.service';
const AUTH_API = 'http://localhost:5082/api/';
const httpOptions = {
  headers : new HttpHeaders({'Content-Type':'application/json'})
}

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  constructor(
    private http: HttpClient,
    private storage:StorageService
  ) { }

  register(name:any,email:any,password:any,address:any,invoiceaddress:any):Observable<any>{
    return this.http.post(AUTH_API + 'DealerService',{
      name,
      email,
      password,
      address,
      invoiceaddress
    },httpOptions);
  }

  login(email:any,password:any):Observable<any>{
    return this.http.post(AUTH_API + 'Token',{
      email,
      password
    },httpOptions);
  }
  
  logOut(){
    this.storage.clean();
    window.location.href = '/#/login'
  }

  isLoggin(){
    let user = this.storage.getUser();
    return user;
  }

  getRole(){
    let role = this.storage.getUser().response.role;
    return role;
  }

  fetchExample():Observable<any>{
    return this.http.get(AUTH_API + 'Token/tokenTest',httpOptions);
  }
}
