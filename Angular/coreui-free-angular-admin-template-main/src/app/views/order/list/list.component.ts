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
    this.orderService.listByCompanyDealer().subscribe((data) =>
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
}
