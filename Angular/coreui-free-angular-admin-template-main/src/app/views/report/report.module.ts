import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportRoutingModule } from './report-routing-module';
import { ButtonModule, CardModule, FormModule, GridModule, TableModule} from '@coreui/angular';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IconModule } from '@coreui/icons-angular';
import { BydateComponent } from './bydate/bydate.component';



@NgModule({
  declarations: [
    BydateComponent
  ],
  imports: [
    CommonModule,
    ReportRoutingModule,
    CardModule,
    TableModule,
    HttpClientModule,
    ButtonModule,
    FormModule,
    FormsModule,
    ReactiveFormsModule,
    GridModule,
    IconModule
  ]
})
export class ReportModule { }
