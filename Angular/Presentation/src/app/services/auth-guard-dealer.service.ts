import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardDealerService {

  constructor(private auth:AuthService,private router:Router, private toastr:ToastrService) { }

  canActivate(): boolean{
    return this.checkUserLogin();
  }

  checkUserLogin(): boolean {
    if (this.auth.isLoggin()) {
      const userRole = this.auth.getRole();
      if (userRole && userRole === "admin") {
        this.toastr.error('You are not authorized to access this page');
        return false;
      }
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }
}
