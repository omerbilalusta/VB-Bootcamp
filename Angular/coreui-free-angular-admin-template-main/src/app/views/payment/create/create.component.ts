import { Component, OnInit } from '@angular/core';
import { DealerService } from 'src/app/services/dealer.service';
import { OrderService } from 'src/app/services/order.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent implements OnInit{

  dealer:any

  constructor(private storageService:StorageService, private orderService:OrderService, private dealerService:DealerService) { }

  ngOnInit(): void {
    this.onLoad(); 
  }

  onLoad(){ 
    this.dealerService.getById(this.storageService.getUser().response.id).subscribe({
      next: data => {
        this.dealer = data.response;
      },
      error: err => {
        console.log(err);
      }
    })
    this.orderService.getByOrderNumber
  }
}