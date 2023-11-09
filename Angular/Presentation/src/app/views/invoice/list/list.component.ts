import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { InvoiceService } from 'src/app/services/invoice.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit{

  invoices: any[] = [];

  constructor(
    private invoiceService: InvoiceService,
    private router: Router,
    private toastr: ToastrService
  ) { }


  ngOnInit(): void {
    this.load();
  }

  load(){
    this.invoiceService.listAllService().subscribe(
      (data) => {
        this.invoices = data.response;
        console.log(data);
      },
      (error) => {
        this.toastr.success('Error Loading Invoices', 'Error');
      }
    )
  }
}
