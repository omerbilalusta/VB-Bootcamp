import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { OrderService } from 'src/app/services/order.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-payment-transfer',
  templateUrl: './payment-transfer.component.html',
  styleUrls: ['./payment-transfer.component.scss']
})
export class PaymentTransferComponent {
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
