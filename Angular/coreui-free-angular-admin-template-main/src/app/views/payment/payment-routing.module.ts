import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { PaymentCardComponent } from './payment-card/payment-card.component';
import { PaymentTransferComponent } from './payment-transfer/payment-transfer.component';
import { PaymentOpenaccountComponent } from './payment-openaccount/payment-openaccount.component';
import { PaymentEFTComponent } from './payment-eft/payment-eft.component';

const routes: Routes = [
  {
    path: '',
    component: CreateComponent,
    data: {
      title: 'Payment'
    }
  },
  {
    path: 'card/:id',
    component: PaymentCardComponent,
    data: {
      title: 'Pay with Card'
    },
  },
  {
    path: 'EFT/:id',
    component: PaymentEFTComponent,
    data: {
      title: 'Pay with EFT'
    },
  },
  {
    path: 'openaccount/:id',
    component: PaymentOpenaccountComponent,
    data: {
      title: 'Pay with Open Account'
    },
  },
  {
    path: 'transfer/:id',
    component: PaymentTransferComponent,
    data: {
      title: 'Pay with Transfer'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PaymentRoutingModule {
}





