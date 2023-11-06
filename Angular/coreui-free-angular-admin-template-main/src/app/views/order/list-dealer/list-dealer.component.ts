import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { OrderService } from 'src/app/services/order.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-list-dealer',
  templateUrl: './list-dealer.component.html',
  styleUrls: ['./list-dealer.component.scss']
})
export class ListDealerComponent {
  orders: any[] = [];
  invoiceDetails: any[] = [];
  user:any = this.storageService.getUser().response;

  visible = false;

  constructor(private orderService:OrderService, private router:Router,private storageService:StorageService) {}

  ngOnInit(): void {
    this.load();
    setTimeout(() => {
      this.visible = !this.visible;
    }, 3000);
  }
  
  load(){
    this.orderService.listByDealer().subscribe((data) =>
    {
      this.orders = data.response;
      console.log(this.orders);
    }, (error) =>
    {
      console.log(error);
    });

    this.orderService.getInvoiceDetails().subscribe((data) =>
    {
      this.invoiceDetails = data.response;
      console.log(this.invoiceDetails);
    },  (error) =>
    {
      console.log(error);this.router.navigate(['/dashboard']);
    });
  }

  pay(orderNumber:number, paymentMethod:any){
    if(paymentMethod == "Card")
      this.router.navigate(['/payment/card/', orderNumber]);
    else if(paymentMethod == "EFT")
      this.router.navigate(['/payment/EFT/', orderNumber]);
    else if(paymentMethod == "Open Account")
      this.router.navigate(['/payment/openaccount/', orderNumber]);
    else if(paymentMethod == "Transfer")
      this.router.navigate(['/payment/transfer/', orderNumber]);
  }
}
