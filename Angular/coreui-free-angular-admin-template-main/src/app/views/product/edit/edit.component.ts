import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';
import { ProductService } from 'src/app/services/product.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit{

  productId:number = Number(this.router.url.split('/')[3]);
  user: any = this.authService.isLoggin().response;

  productForm = new FormGroup({
    name: new FormControl(''),
    description: new FormControl(''),
    type: new FormControl(''),
    stockQuantity: new FormControl(''),
    price: new FormControl(''),
    taxRate: new FormControl(''),
  });


  constructor(private storageService:StorageService, private productService:ProductService, private router:Router, private authService:AuthService, private toastr:ToastrService){}
  
  ngOnInit(): void {
    this.load();
  }

  load(){
    this.productService.getById(this.productId).subscribe(
      (data) => {  
        if(data.success == false)
        {
          this.router.navigate(['/product/list']);
          this.toastr.error("Product not found"  , 'Error');
        }
        else{
          if(this.user.id != data.response.company.id && this.user.role == 'admin') // Buradaki amaç, bir kullanıcı başka bir company'e ait ürünü düzenlemeye çalışırsa onu engellemek.
          this.router.navigate(['/product/list']);

          this.productForm.controls['name'].setValue(data.response.name);           //Kontroldeki koşul gerçekleşmediyse formu dolduruluyor ve sayfa yükleniyor.
          this.productForm.controls['description'].setValue(data.response.description);
          this.productForm.controls['type'].setValue(data.response.type);
          this.productForm.controls['stockQuantity'].setValue(data.response.stockQuantity);
          this.productForm.controls['price'].setValue(data.response.price);
          this.productForm.controls['taxRate'].setValue(data.response.taxRate);
        }
      },
      (error) => {
        this.toastr.error("Product not found"  , 'Error');
      }
    );
  }

  onSubmit(){
    const { name, description, type, stockQuantity, price, taxRate } = this.productForm.value;
    this.productService.update(this.productId, name, description, type, Number(stockQuantity), Number(price), Number(taxRate)).subscribe({
      next: data => {
        if(data.success == false)
          console.log('error');
        else
          this.router.navigate(['/product/list']);
          this.toastr.success("Product updated successfully"  , 'Success');
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
