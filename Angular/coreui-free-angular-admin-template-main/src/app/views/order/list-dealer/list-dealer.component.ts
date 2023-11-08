import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from 'src/app/services/order.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-list-dealer',
  templateUrl: './list-dealer.component.html',
  styleUrls: ['./list-dealer.component.scss']
})
export class ListDealerComponent {
  orders: any[] = [];
  declinedOrders: any[] = [];
  invoiceDetails: any[] = [];
  user:any = this.storageService.getUser().response;

  constructor(private orderService:OrderService, private router:Router,private storageService:StorageService, private toastr:ToastrService) {}

  ngOnInit(): void {
    this.load();
  }
  
  load(){
    this.orderService.listByDealer().subscribe((data) =>
    {
      this.orders = data.response.filter((order:any) => order.isActive == true);
      console.log(this.orders);
    }, (error) =>
    {
      console.log(error);
      this.toastr.error('Error');
    });

    this.orderService.listDeclined().subscribe((data) =>
    {
      this.declinedOrders = data.response;
      console.log(this.declinedOrders);
    }, (error) =>
    {
      console.log(error);
      this.toastr.error('Error');
    });

    this.orderService.getInvoiceDetails().subscribe((data) =>
    {
      this.invoiceDetails = data.response;
      console.log(this.invoiceDetails);
    },  (error) =>
    {
      console.log(error);
      this.toastr.error('Error');
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

  changePaymentMethod(orderNumber:number){
    this.router.navigate(['/payment/editmethod/', orderNumber]);
  }

  delete(orderNumber:number){
    this.orderService.deleteOrder(orderNumber).subscribe((data) =>
    {
      this.load();
      this.toastr.success('Order Deleted');
    }, (error) =>
    {
      console.log(error);
      this.toastr.error('Error');
    });
  }
}
