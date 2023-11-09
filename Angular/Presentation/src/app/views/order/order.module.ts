import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddComponent } from './add/add.component';
import { ListComponent } from './list/list.component';
import { OrderRoutingModule } from './order-routing-module';
import { ButtonModule, CardModule, FormModule, GridModule, PopoverModule, TableModule, TooltipModule } from '@coreui/angular';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IconModule } from '@coreui/icons-angular';
import { StorageService } from 'src/app/services/storage.service';
import { OrderService } from 'src/app/services/order.service';
import { ListDealerComponent } from './list-dealer/list-dealer.component';



@NgModule({
  declarations: [
    AddComponent,
    ListComponent,
    ListDealerComponent
  ],
  imports: [
    CommonModule,
    OrderRoutingModule,
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
  ],
  providers:[
    StorageService,
    OrderService
  ]
})
export class OrderModule { }
