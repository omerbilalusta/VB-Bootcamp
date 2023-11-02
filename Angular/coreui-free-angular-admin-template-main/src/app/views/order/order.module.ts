import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddComponent } from './add/add.component';
import { ListComponent } from './list/list.component';
import { OrderRoutingModule } from './order-routing-module';
import { ButtonModule, CardModule, FormModule, GridModule, TableModule } from '@coreui/angular';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { IconModule } from '@coreui/icons-angular';
import { StorageService } from 'src/app/services/storage.service';
import { OrderService } from 'src/app/services/order.service';



@NgModule({
  declarations: [
    AddComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    OrderRoutingModule,
    CardModule,
    TableModule,
    HttpClientModule,
    ButtonModule,
    FormModule,
    ReactiveFormsModule,
    GridModule,
    IconModule
  ],
  providers:[
    StorageService,
    OrderService
  ]
})
export class OrderModule { }
