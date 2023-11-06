import { Component, OnInit } from '@angular/core';
import { OrderService } from 'src/app/services/order.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})

export class AddComponent implements OnInit{

  products: any[] = [];
  companies: any[] = [];
  selectedValue: string = "0";
  selectedValuePayment: string = "0";
  loading = true;
  cart: { [key: string]: number; } = {};
  
  constructor(private productService:ProductService, private orderService:OrderService) {}

  ngOnInit(): void {
    this.load();
    console.log(this.selectedValue);
  }
  
  load(){
    this.productService.list().subscribe((data) =>
    {
      this.products = data.response;
      this.loading = false;
    }, (error) =>
    {
      console.log(error);
    });
    this.productService.getAllCompanies().subscribe((data) =>
    {
      this.companies = data.response;
    }, (error) =>
    {
      console.log(error);
    })
  }
  
  getProducts(){
    return this.products.filter(x => x.companyId == Number(this.selectedValue));
  }
  addToCart(id:number,count:number){
    if(this.cart[id.toString()] != undefined)
      this.cart[id.toString()] += count;
    else
      this.cart[id.toString()] = count;
    console.log(this.cart);
  }

  confirmCart(){
    this.orderService.createOrder(this.selectedValuePayment, this.cart).subscribe({
      next: data =>{
        if(data.response == false)
          console.log('error');
        else
          console.log(data);
          window.location.reload();
      },
      error: err => {
        console.log(err.error.errors);
      }
    })
  }
}
