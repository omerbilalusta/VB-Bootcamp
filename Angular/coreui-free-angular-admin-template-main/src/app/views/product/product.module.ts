import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list/list.component';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
import { ProductRoutingModule } from './product-routing-module';
import { ButtonModule, CardModule, FormModule, GridModule, TableModule } from '@coreui/angular';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { ProductService } from '../../services/product.service';
import { IconModule } from '@coreui/icons-angular';


@NgModule({
  declarations: [
    ListComponent,
    AddComponent,
    EditComponent
  ],
  imports: [
    CommonModule,
    ProductRoutingModule,
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
    ProductService
  ]
})
export class ProductModule { }