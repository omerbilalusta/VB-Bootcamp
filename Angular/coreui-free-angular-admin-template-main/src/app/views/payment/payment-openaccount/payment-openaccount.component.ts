import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from 'src/app/services/order.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-payment-openaccount',
  templateUrl: './payment-openaccount.component.html',
  styleUrls: ['./payment-openaccount.component.scss']
})
export class PaymentOpenaccountComponent {
  orderNumber:number = Number(this.router.url.split('/')[3]);   //orderNumber'ı öğrenmek için amatörce bir yol oldu, zaman olursa gerekli araştırmayı tekrar 
  order:any;                                                    //yapıp burayı değiştireceğim.
  user:any = this.storageService.getUser();

  constructor(private router:Router, private orderService:OrderService, private storageService:StorageService, private toastr:ToastrService) {  }

  ngOnInit(): void {
    this.load();
  }

  load(){
    this.orderService.getByOrderNumber(this.orderNumber).subscribe({
      next: data => {
        this.order = data.response;
        console.log(this.order);
      },
      error: err => {
        console.log(err);
      }
    })
  }

  pay(orderNumber:number){
    this.orderService.payWithOpenAccount(this.orderNumber).subscribe((data:any) =>
    {
      if(data.success == false){
        console.log(data.message);
        this.router.navigate(['/order/list-dealer']);
        this.toastr.error(data.message  , 'Error');
      }
      else{
        this.router.navigate(['/order/list-dealer']);
        this.toastr.success("Payment succeed"  , 'Success');
      }
      
    },  (error) =>
    {
      console.log(error);
    });
  }
}
