import { Component} from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router'
import { StorageService } from 'src/app/services/storage.service';
import { DealerService } from 'src/app/services/dealer.service';


@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent{

  dealerForm = new FormGroup({
    name: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    address: new FormControl(''),
    invoiceaddress: new FormControl(''),
    dividend: new FormControl(''),
    openaccountlimit: new FormControl('')
  });
  
  constructor(
    private dealerService:DealerService,
    private router:Router,
    private storage:StorageService
  ) {}


  onSubmit(){
    const { name, email, password, address, invoiceaddress, dividend, openaccountlimit } = this.dealerForm.value
    this.dealerService.add(name, email, password, address, invoiceaddress, Number(dividend), Number(openaccountlimit)).subscribe({
      next: data =>{
        if(data.success == false)
          console.log('error');
        else
          this.router.navigate(['/adminDealer/list']);
      },
      error: err => {
        console.log(err.error.errors);
        console.log(typeof err.error.errors);
      }
    })
  }
}


