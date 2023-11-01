import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { StorageService } from 'src/app/services/storage.service';
import { DealerService } from 'src/app/services/dealer.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit{
  
  
  dealers: any[] = [];

  constructor(private dealerService:DealerService, private router:Router,private storage:StorageService) {}

  ngOnInit(): void {
    this.load();
    // this.storage.getUser().response.role == 'admin' ? this.router.navigate(['/dashboard']) : console.log('dealer');
  }

  load(){
    this.dealerService.list().subscribe((data) =>
    {
      this.dealers = data.response;
      console.log(this.dealers);
    }, (error) =>
    {
      console.log(error);
    })
  }
}
