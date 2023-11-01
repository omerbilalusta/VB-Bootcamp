import { NgModule, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DealerAdminRoutingModule } from './dealer-admin-routing-module';
import { ButtonModule, CardModule, FormModule, GridModule, TableModule } from '@coreui/angular';
import { HttpClientModule } from '@angular/common/http';
import { ListComponent } from './list/list.component';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ProductService } from 'src/app/services/product.service';
import { DealerService } from 'src/app/services/dealer.service';
import { AuthService } from 'src/app/services/auth.service';
import { IconModule } from '@coreui/icons-angular';


@NgModule({
  declarations: [
    ListComponent,
    AddComponent,
    EditComponent,
  ],
  imports: [
    CommonModule,
    DealerAdminRoutingModule,
    CardModule,
    TableModule,
    HttpClientModule,
    ButtonModule,
    FormModule,
    ReactiveFormsModule,
    GridModule,
    IconModule
  ],
  providers :[
    AuthService,
    ProductService,
    DealerService
  ]
})
export class DealerAdminModule{
  constructor() {}
}
