import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list/list.component';
import { ButtonModule, CardModule, FormModule, GridModule, PopoverModule, TableModule, TooltipModule } from '@coreui/angular';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IconModule } from '@coreui/icons-angular';
import { InvoiceRoutingModule } from './invoice-routing-module';



@NgModule({
  declarations: [
    ListComponent
  ],
  imports: [
    CommonModule,
    InvoiceRoutingModule,
    CardModule,
    TableModule,
    HttpClientModule,
    ButtonModule,
    FormModule,
    FormsModule,
    ReactiveFormsModule,
    GridModule,
    IconModule,
    PopoverModule,
    TooltipModule
  ]
})
export class InvoiceModule { }
