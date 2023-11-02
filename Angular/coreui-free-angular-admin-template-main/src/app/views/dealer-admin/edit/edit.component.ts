import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router'
import { StorageService } from 'src/app/services/storage.service';
import { DealerService } from 'src/app/services/dealer.service';


@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit{


  dealerId:number = Number(this.router.url.split('/')[3]);
  dealerForm = new FormGroup({
    dividend: new FormControl(''),
    openaccountlimit: new FormControl('')
  });
  
  constructor(
    private dealerService:DealerService,
    private router:Router,
    private storage:StorageService,
    private route:ActivatedRoute
  ) {}


  ngOnInit(): void {
    this.load();
  }

  load(){
    this.dealerService.getById(this.dealerId).subscribe((data) =>
    {
      this.dealerForm.controls['dividend'].setValue(data.response.dividend);
      this.dealerForm.controls['openaccountlimit'].setValue(data.response.openAccountLimit)
    }, (error) =>
    {
      console.log(error);
    })
  }


  onSubmit(){
    const { dividend, openaccountlimit } = this.dealerForm.value
    this.dealerService.updateShort(Number(dividend), Number(openaccountlimit)).subscribe({
      next: data =>{
        if(data.success == false)
        {
          console.log('error');
        }
        else{
          this.router.navigate(['/adminDealer/list']);
        }
      },
      error: err => {
        console.log(err.error.errors);
        console.log(typeof err.error.errors);
      }
    })
  }
}


