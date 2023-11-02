import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { Router } from '@angular/router';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit{
  
  
  products: any[] = [];
  user:any = this.storageService.getUser().response;

  constructor(private productService:ProductService, private router:Router,private storageService:StorageService) {}

  ngOnInit(): void {
    this.load();
  }
  
  load(){
    this.productService.list().subscribe((data) =>
    {
      this.products = data.response;
    }, (error) =>
    {
      console.log(error);
    })
  }

  isDelete(id:number){
    this.productService.delete(id).subscribe({
      next: data =>{
        if(data.success == false)
          console.log('error');
        else
          window.location.reload();
      },
      error: err => {
        console.log(err.error.errors);
      }
    });
  }
}

