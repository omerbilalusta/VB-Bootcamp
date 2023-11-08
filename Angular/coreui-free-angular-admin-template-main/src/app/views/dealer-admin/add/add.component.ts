import { Component} from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router'
import { StorageService } from 'src/app/services/storage.service';
import { DealerService } from 'src/app/services/dealer.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent{

  dealerForm = new FormGroup({
    name: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    retrypassword: new FormControl(''),
    address: new FormControl(''),
    invoiceaddress: new FormControl(''),
    dividend: new FormControl(''),
    openaccountlimit: new FormControl('')
  });
  
  constructor(
    private dealerService:DealerService,
    private router:Router,
    private toastr:ToastrService
  ) {}


  onSubmit(){
    const { name, email, password, retrypassword, address, invoiceaddress, dividend, openaccountlimit } = this.dealerForm.value;
    if (password != retrypassword) {
      this.toastr.error("Passwords doesn't match ", 'Error');
    }
    else{
      this.dealerService.addAdmin(name, email, password, address, invoiceaddress, Number(dividend), Number(openaccountlimit)).subscribe({
        next: data =>{
          if(data.success == false)
            console.log('error');
          else
            this.router.navigate(['/adminDealer/list']);
            this.toastr.success("Dealer added succesfully"  , 'Success');
        },
        error: err => {
          if (err.error.errors.Address) {
            this.toastr.error(err.error.errors.Address  , 'Error');
          }
          if (err.error.errors.Dividend) {
            this.toastr.error(err.error.errors.Dividend  , 'Error');
          }
          if (err.error.errors.Email) {
            this.toastr.error(err.error.errors.Email  , 'Error');
          }
          if (err.error.errors.InvoiceAddress) {
            this.toastr.error(err.error.errors.InvoiceAddress  , 'Error');
          }
          if (err.error.errors.OpenAccountLimit) {
            this.toastr.error(err.error.errors.OpenAccountLimit  , 'Error');
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


