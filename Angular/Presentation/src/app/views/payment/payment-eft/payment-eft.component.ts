import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { OrderService } from 'src/app/services/order.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-payment-eft',
  templateUrl: './payment-eft.component.html',
  styleUrls: ['./payment-eft.component.scss']
})
export class PaymentEFTComponent {
  orderNumber:number = Number(this.router.url.split('/')[3]);   //orderNumber'ı öğrenmek için amatörce bir yol oldu, 
  order:any;                                                    //zaman olursa gerekli araştırmayı tekrar yapıp burayı değiştireceğim.
  
  constructor(private router:Router, private orderService:OrderService) {  }

  ngOnInit(): void {
    this.load();
  }

  load(){
    this.orderService.getByOrderNumberService(this.orderNumber).subscribe({
      next: data => {
        this.order = data.response;
      },
      error: err => {
        console.log(err);
      }
    })
  }
}
