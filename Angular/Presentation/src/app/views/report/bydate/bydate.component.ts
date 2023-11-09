import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-bydate',
  templateUrl: './bydate.component.html',
  styleUrls: ['./bydate.component.scss']
})
export class BydateComponent {

  report: any;

  reportForm = new FormGroup({
    dateFrom: new FormControl(''),
    dateTo: new FormControl('')
  });
  
  constructor(
    private reportService:ReportService,
    private toastr:ToastrService
  ) {}



  onSubmit(){
    const { dateFrom, dateTo } = this.reportForm.value;
    console.log(dateFrom);
    console.log(dateTo);
    if (dateFrom == '' || dateTo == '') {
      this.toastr.error('Pick Date'  , 'Error');
    }
    else{
      this.reportService.getByDate(dateFrom, dateTo).subscribe({
        next: data =>{
          if(data.success == false)
            this.toastr.error(data.message  , 'Error');
          else
          {
            this.report = data.response;
            this.toastr.success("Report generated succesfully"  , 'Success');
          }
            
        },
        error: err => {
          this.toastr.error(err.error.errors  , 'Error');
        }
      });
    }
  }
}
