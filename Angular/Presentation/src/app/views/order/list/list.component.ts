import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
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

  constructor(private orderService:OrderService,private storageService:StorageService, private toastr:ToastrService) {}

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
    console.log(id + " order declined");
    this.orderService.declineOrder(id, "Declined by Company").subscribe((data) =>
    {
      this.load();
      this.toastr.success('Order declined successfully');
    },  (error) =>
    {
      console.log(error);
    });
  }
  approve(id:number){
    console.log(id + " order approved");
    this.orderService.approveOrder(id).subscribe((data) =>
    {
      this.load();
      this.toastr.success('Order approved successfully');
    },  (error) =>
    {
      console.log(error);
    });
  }

  confirmPayment(id:number){
    console.log(id + " payment confirmed");
    this.orderService.confirmPayment(id).subscribe((data) =>
    {
      this.load();
      this.toastr.success('Payment confirmed successfully');
    },  (error) =>
    {
      console.log(error);
    });
  }
}
