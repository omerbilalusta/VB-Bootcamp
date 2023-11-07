import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router'
import { DealerService } from 'src/app/services/dealer.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit{


  dealerId:number = Number(this.router.url.split('/')[3]);
  dealerForm = new FormGroup({
    dividend: new FormControl(''),
    openaccountlimit: new FormControl('')
  });
  
  constructor(
    private dealerService:DealerService,
    private router:Router,
    private toastr:ToastrService
  ) {}


  ngOnInit(): void {
    this.load();
  }

  load(){
    this.dealerService.getById(this.dealerId).subscribe((data) =>
    {
      this.dealerForm.controls['dividend'].setValue(data.response.dividend);
      this.dealerForm.controls['openaccountlimit'].setValue(data.response.openAccountLimit)
    }, (error) =>
    {
      console.log(error);
    })
  }


  onSubmit(){
    const { dividend, openaccountlimit } = this.dealerForm.value
    this.dealerService.updateShort(this.dealerId, Number(dividend), Number(openaccountlimit)).subscribe({
      next: data =>{
        if(data.success == false)
        {
          console.log('error');
        }
        else{
          this.router.navigate(['/adminDealer/list']);
          this.toastr.success("Dealer updated succesfully"  , 'Success');
        }
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
        console.log(err.error.errors)
      }
    })
  }
}


