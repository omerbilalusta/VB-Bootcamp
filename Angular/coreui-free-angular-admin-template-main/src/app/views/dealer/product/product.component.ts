import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { Router } from '@angular/router';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit{
  
  
  products: any[] = [];

  constructor(private productService:ProductService, private router:Router,private storage:StorageService) {}

  ngOnInit(): void {
    this.load();
    this.storage.getUser().response.role == 'admin' ? this.router.navigate(['/dashboard']) : console.log('dealer');
  }

  load(){
    this.productService.list().subscribe((data) =>
    {
      this.products = data.response;
      console.log(this.products);
    }, (error) =>
    {
      console.log(error);
    })
  }
}
