import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { OrderService } from 'src/app/services/order.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent {
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
    this.orderService.listByCompany().subscribe((data) =>
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
      console.log(error);
    });
  }

  decline(id:number){
    console.log(id + " declined");
    this.orderService.declineOrder(id, "Declined by Company").subscribe((data) =>
    {
      console.log('Order declined successfully');
      window.location.reload();
    },  (error) =>
    {
      console.log(error);
    });
  }
  approve(id:number){
    console.log(id + " approved");
    this.orderService.approveOrder(id).subscribe((data) =>
    {
      console.log('Order approved successfully');
      window.location.reload();
    },  (error) =>
    {
      console.log(error);
    });
  }
}
