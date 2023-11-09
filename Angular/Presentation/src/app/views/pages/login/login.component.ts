import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthService } from '../../../services/auth.service'
import { Router } from '@angular/router'
import { StorageService } from 'src/app/services/storage.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  loginForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl('')
  });
  
  constructor(
    private authService:AuthService,
    private router:Router,
    private storage:StorageService,
    private toastr: ToastrService
  ) { 

  }
  onSubmit(){
    const { email,password } = this.loginForm.value
    this.authService.login(email,password).subscribe({
      next: data =>{
        if(data.success == false)
        {
          this.toastr.error('Check your mail and password.'  , 'Error');
        }
        else{
          this.storage.saveUser(data);
          if(data.response.role == 'admin')
            this.router.navigate(['/product/list']);
          else
            this.router.navigate(['/product/list']);
        }
      },
      error: err => {
        console.log(err.error)
        this.toastr.error('Check your mail and password.\n' + err.error.title  , 'Error');
      }
    })
  }
}
