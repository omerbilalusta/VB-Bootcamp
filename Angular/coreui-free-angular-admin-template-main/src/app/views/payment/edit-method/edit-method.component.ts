import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-edit-method',
  templateUrl: './edit-method.component.html',
  styleUrls: ['./edit-method.component.scss']
})
export class EditMethodComponent {
  orderNumber:number = Number(this.router.url.split('/')[3]);
  selectedValuePayment: string = "0";
  paymentForm = new FormGroup({
    select: new FormControl(''),
    paymentMethod: new FormControl(''),
  });
  
  constructor(
    private orderService:OrderService,
    private router:Router,
    private toastr:ToastrService
  ) {}


  ngOnInit(): void {
    this.load();
    
  }

  load(){
    this.orderService.getByOrderNumberService(this.orderNumber).subscribe((data) =>
    {
      this.paymentForm.controls['select'].setValue(data.response.paymentMethod);
    }, (error) =>
    {
      console.log(error);
    })
  }

  onSubmit(){
    this.orderService.updatePaymentMethodService(this.orderNumber, this.selectedValuePayment).subscribe({
      next: (data:any) =>{
        if(data.success == false)
        {
          this.toastr.error(data.message  , 'Error');
        }
        else{
          this.router.navigate(['/order/list-dealer']);
          this.toastr.success("Payment Method updated succesfully"  , 'Success');
        }
      },
      error: (err:any) => {
        if (err.error.errors) {
          this.toastr.error("Payment couldn't update"  , 'Error');
        }
      }
    });
  }
}
