import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router'
import { StorageService } from 'src/app/services/storage.service';
import { ProductService } from 'src/app/services/product.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent {

  productForm = new FormGroup({
    name: new FormControl(''),
    description: new FormControl(''),
    type: new FormControl(''),
    stockQuantity: new FormControl(''),
    price: new FormControl(''),
    taxRate: new FormControl(''),
  });
  
  constructor(
    private prodService:ProductService,
    private router:Router,
    private storage:StorageService,
    private toastr:ToastrService
  ) { 

  }
  onSubmit(){
    const { name, description, type, stockQuantity, price, taxRate } = this.productForm.value
    this.prodService.add(name, description, type, Number(stockQuantity), Number(price), Number(taxRate)).subscribe({
      next: data =>{
        if(data.success == false)
        {
          console.log('error');
        }
        else{
          this.router.navigate(['/product/list']);
          this.toastr.success("Product added successfully"  , 'Success');
        }
      },
      error: err => {
        if (err.error.errors.Description) {
          this.toastr.error(err.error.errors.Description  , 'Error');
        }
        if (err.error.errors.Price) {
          this.toastr.error(err.error.errors.Price  , 'Error');
        }
        if (err.error.errors.StockQuantity) {
          this.toastr.error(err.error.errors.StockQuantity  , 'Error');
        }
        if (err.error.errors.Name) {
          this.toastr.error(err.error.errors.Name  , 'Error');
        }
        if (err.error.errors.TaxRate) {
          this.toastr.error(err.error.errors.TaxRate  , 'Error');
        }
        if (err.error.errors.Type) {
          this.toastr.error(err.error.errors.Type  , 'Error');
        }
        console.log(err.error)
      }
    })
  }
}


