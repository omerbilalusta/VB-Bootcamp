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
  order: any[] = [];
  user:any = this.storageService.getUser().response;

  constructor(private orderService:OrderService, private router:Router,private storageService:StorageService) {}

  ngOnInit(): void {
    this.load();
  }
  
  load(){
    this.orderService.listByCompanyDealer().subscribe((data) =>
    {
      this.order = data.response;
      console.log(this.order);
    }, (error) =>
    {
      console.log(error);
    })
  }
}
