import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthService } from '../../../services/auth.service'
import { Router } from '@angular/router'
import { StorageService } from 'src/app/services/storage.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  errors:any[] = [];
  

  registerForm = new FormGroup({
    name: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    retrypassword: new FormControl(''),
    address: new FormControl(''),
    invoiceaddress: new FormControl('')
  });
  
  constructor(
    private authService:AuthService,
    private router:Router,
    private toastr: ToastrService
  ) { 

  }
  onSubmit(){
    const { name,email,password,retrypassword,address,invoiceaddress } = this.registerForm.value;
    if (password != retrypassword && password != "" && retrypassword != "") {
      this.toastr.error("Passwords doesn't match ", 'Error');
    }
    else{
      this.authService.registerService(name,email,password,address,invoiceaddress).subscribe({
        next: data =>{
          if(data.success == false)
          {
            this.toastr.error('Check your mail and password.'  , 'Error');
          }
          else{
            this.router.navigate(['/login']);
          }
        },
        error: err => {      //Error'ları if else'ler ile ele almak mantıklı değil ancak response body'de dondüğüm 
                            //hata mesajları object türünden olduğu için bu hata mesajlarını bir foreach ile döndüremedim
          // for (let field of Object.values(err.error.errors)) {
          //   console.log(field)      //Eksik TypeScript bilgimden ötürü field içerisine dönen array'leri TypeScript'e gösteremedim. 
          // }                         //Dolayısıyla hata mesajlarını pratik olan bu yol ile dönemedim
          if (err.error.errors.Address) {
            this.toastr.error(err.error.errors.Address  , 'Error');
          }
          if (err.error.errors.Email) {
            this.toastr.error(err.error.errors.Email  , 'Error');
          }
          if (err.error.errors.InvoiceAddress) {
            this.toastr.error(err.error.errors.InvoiceAddress  , 'Error');
          }
          if (err.error.errors.Name) {
            this.toastr.error(err.error.errors.Name  , 'Error');
          }
          if (err.error.errors.Password) {
            this.toastr.error(err.error.errors.Password  , 'Error');
          }
          console.log(err.error)
        }
      })
    }
  }
}
