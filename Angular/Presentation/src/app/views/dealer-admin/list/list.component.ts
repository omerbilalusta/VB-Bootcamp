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
  }

  load(){
    this.dealerService.list().subscribe((data) =>
    {
      this.dealers = data.response;
    }, (error) =>
    {
      console.log(error);
    })
  }
  isDelete(id:number){
    this.dealerService.delete(id).subscribe((data) =>
    {
      window.location.reload();
    }, (error) =>
    {
      console.log(error);
    })
  }
}
