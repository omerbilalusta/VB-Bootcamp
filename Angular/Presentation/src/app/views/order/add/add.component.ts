import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
  
  constructor(private productService:ProductService, private orderService:OrderService, private toastr:ToastrService, private router:Router) {}

  ngOnInit(): void {
    this.load();
    console.log(this.selectedValue);
  }
  
  load(){
    this.productService.listService().subscribe((data) =>
    {
      this.products = data.response;
      this.loading = false;
    }, (error) =>
    {
      console.log(error);
      this.toastr.error('Error');
    });
    this.productService.getAllCompaniesService().subscribe((data) =>
    {
      this.companies = data.response;
    }, (error) =>
    {
      console.log(error);
      this.toastr.error('Error');
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
    if(this.selectedValuePayment != "0"){
      this.orderService.createOrderService(this.selectedValuePayment, this.cart).subscribe({
        next: data =>{
          if(data.response == false)
            this.toastr.error('Error');
          else
            console.log(data);
            this.router.navigate(['/order/list-dealer']);
            this.toastr.success("Order succeed"  , 'Success');
        },
        error: err => {
          console.log(err.error.errors);
          this.toastr.error(err.error.title  , 'Error');
        }
      });
    }
    else
    {
      this.toastr.error('Please select payment method', 'Error');
      return;
    }
  }
}
