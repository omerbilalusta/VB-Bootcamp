import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthService } from '../../../services/auth.service'
import { Router } from '@angular/router'
import { StorageService } from 'src/app/services/storage.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  registerForm = new FormGroup({
    name: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    address: new FormControl(''),
    invoiceaddress: new FormControl('')
  });
  
  constructor(
    private authService:AuthService,
    private router:Router,
    private storage:StorageService
  ) { 

  }
  onSubmit(){
    const { name,email,password,address,invoiceaddress } = this.registerForm.value
    this.authService.register(name,email,password,address,invoiceaddress).subscribe({
      next: data =>{
        if(data.success == false)
        {
          console.log('error');
        }
        else{
          this.router.navigate(['/login']);
        }
      },
      error: err => {
        console.log('error 500')
      }
    })
  }
}
