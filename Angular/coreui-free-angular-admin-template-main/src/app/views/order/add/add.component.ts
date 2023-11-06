import { Component, OnInit } from '@angular/core';
import { OrderService } from 'src/app/services/order.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})

// interface IDictionary {
//   [key: string]: number;
// }

// const dictionary = new Map<string, string>();

// dictionary.set('key1', 'value1');
export class AddComponent implements OnInit{

  products: any[] = [];
  companies: any[] = [];
  selectedValue: string = "0";
  selectedValuePayment: string = "0";
  loading = true;
  cart: number[] = [];
  // dictionary: IDictionary = {};
  dictionaryy = new Map<string, number>();
  
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
  addToCart(id:number){
    // if(this.cart[0][0] != undefined){
    //   this.cart[id][1] += 1;
    //   console.log(this.cart);
    //   return;
    // }
    this.cart.push(this.products.find(x => x.id == id).id);
    console.log(this.cart);
  }

  confirmCart(){
    this.orderService.createOrder(this.selectedValuePayment).subscribe({
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

  asd(){
    this.cart.forEach(item => {
      
    });
  }
}
