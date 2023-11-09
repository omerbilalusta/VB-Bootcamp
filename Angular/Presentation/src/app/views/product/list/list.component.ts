import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { Router } from '@angular/router';
import { StorageService } from 'src/app/services/storage.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit{
  
  public liveDemoVisible = false;
  productId_delete:number = 0;

  products: any[] = [];
  user:any = this.storageService.getUser().response;

  constructor(
    private productService:ProductService,
    private authService:AuthService,
    private router:Router,
    private storageService:StorageService,
    private toastr: ToastrService
    ) {}

  ngOnInit(): void {
    this.load();
    this.authService.fetchExample().subscribe({
      next: (data) => {
        console.log(data);
      },
      error: (err:any) => {
        this.router.navigate(['/login']);
        this.toastr.error("Token expired. Login again."  , 'Error');
      }
    });
  }
  
  load(){
    if(this.user.role == 'admin')
    {
      this.productService.listAdmin().subscribe((data) =>
      {
        this.products = data.response;
      }, (error) =>
      {
        console.log(error);
      })
    }
    else
    {
      this.productService.listService().subscribe((data) =>
      {
        this.products = data.response;
      }, (error) =>
      {
        console.log(error);
      })
    }
  }

  isDelete(){
    this.productService.delete(this.productId_delete).subscribe({
      next: data =>{
        if(data.success == false)
          console.log('error');
        else
          this.load();
        this.liveDemoVisible = !this.liveDemoVisible;
      },
      error: err => {
        console.log(err.error.errors);
      }
    });
  }

  toggleModal(id:number) {
    this.productId_delete = id;
    this.liveDemoVisible = !this.liveDemoVisible;
    console.log(this.productId_delete);
  }

  handleLiveDemoChange(event: boolean) {
    this.liveDemoVisible = event;
  }
}

