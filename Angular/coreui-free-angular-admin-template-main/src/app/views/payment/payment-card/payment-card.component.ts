import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-payment-card',
  templateUrl: './payment-card.component.html',
  styleUrls: ['./payment-card.component.scss']
})
export class PaymentCardComponent {
  
  cardForm = new FormGroup({                //Card bilgilerini form aracılığı ile aldık ama bu işlem gerçekte üçüncü parti bir servis ile yapılmaktadır.
    cardnumber: new FormControl(''),        //Bu yüzden, uygulamamızda ödeme simülasyon olması için bu şekilde bir form oluşturduk.
    cvv: new FormControl(''),               //Yani, bu form işlevsizdir.
    expirationdate: new FormControl(''),
    cardholdername: new FormControl('')
  })

  orderNumber:number = Number(this.router.url.split('/')[3]);   //orderNumber'ı öğrenmek için amatörce bir yol oldu, zaman olursa gerekli araştırmayı tekrar 
  order:any;                                                    //yapıp burayı değiştireceğim.

  constructor(private router:Router, private orderService:OrderService) {  }

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

  pay(){
    this.orderService.pay(this.orderNumber).subscribe((data) =>
    {
      console.log('Order paid successfully');
      this.router.navigate(['/order/list-dealer']);
    },  (error) =>
    {
      console.log(error);
    });
  }
}
