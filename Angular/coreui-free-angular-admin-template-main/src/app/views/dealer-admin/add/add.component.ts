import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router'
import { StorageService } from 'src/app/services/storage.service';
import { ProductService } from 'src/app/services/product.service';


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
    private storage:StorageService
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
          this.storage.saveUser(data);
          if(data.response.role == 'admin')
            this.router.navigate(['/dashboard']);
          else
            this.router.navigate(['/dealer']);
        }
      },
      error: err => {
        console.log(err.error.errors);
        console.log(typeof err.error.errors);
      }
    })
  }
}


