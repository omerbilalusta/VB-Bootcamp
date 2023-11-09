import { NgModule, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DealerRoutingModule } from './dealer-routing-module';
import { ButtonModule, CardModule, FormModule, GridModule, TableModule } from '@coreui/angular';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from 'src/app/services/auth.service';
import { DealerService } from 'src/app/services/dealer.service';
import { IconModule } from '@coreui/icons-angular';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    DealerRoutingModule,
    CardModule,
    TableModule,
    HttpClientModule,
    CardModule,
    TableModule,
    HttpClientModule,
    ButtonModule,
    GridModule,
    IconModule,
    FormModule,
    ReactiveFormsModule
  ],
  providers :[
    AuthService,
    DealerService
  ]
})
export class DealerModule{
  constructor() {}
}
