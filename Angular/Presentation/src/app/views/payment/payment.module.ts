import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateComponent } from './create/create.component';
import { PaymentRoutingModule } from './payment-routing.module';
import { ButtonModule, CardModule, FormModule, GridModule, TableModule, TooltipModule } from '@coreui/angular';
import { PaymentCardComponent } from './payment-card/payment-card.component';
import { PaymentEFTComponent } from './payment-eft/payment-eft.component';
import { PaymentOpenaccountComponent } from './payment-openaccount/payment-openaccount.component';
import { PaymentTransferComponent } from './payment-transfer/payment-transfer.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { IconModule } from '@coreui/icons-angular';
import { EditMethodComponent } from './edit-method/edit-method.component';



@NgModule({
  declarations: [
    CreateComponent,
    PaymentCardComponent,
    PaymentEFTComponent,
    PaymentOpenaccountComponent,
    PaymentTransferComponent,
    EditMethodComponent
  ],
  imports: [
    CommonModule,
    PaymentRoutingModule,
    CardModule,
    ButtonModule,
    GridModule,
    TableModule,
    TooltipModule,
    FormModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    IconModule
  ]
})
export class PaymentModule { }
