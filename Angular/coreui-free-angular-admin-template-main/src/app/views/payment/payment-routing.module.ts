import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { PaymentCardComponent } from './payment-card/payment-card.component';
import { PaymentTransferComponent } from './payment-transfer/payment-transfer.component';
import { PaymentOpenaccountComponent } from './payment-openaccount/payment-openaccount.component';
import { PaymentEFTComponent } from './payment-eft/payment-eft.component';
import { EditMethodComponent } from './edit-method/edit-method.component';
import { AuthGuardDealerService } from 'src/app/services/auth-guard-dealer.service';

const routes: Routes = [
  {
    path: '',
    component: CreateComponent,
    canActivate: [AuthGuardDealerService],
    data: {
      title: 'Payment'
    }
  },
  {
    path: 'card/:id',
    component: PaymentCardComponent,
    canActivate: [AuthGuardDealerService],
    data: {
      title: 'Pay with Card'
    },
  },
  {
    path: 'EFT/:id',
    component: PaymentEFTComponent,
    canActivate: [AuthGuardDealerService],
    data: {
      title: 'Pay with EFT'
    },
  },
  {
    path: 'openaccount/:id',
    component: PaymentOpenaccountComponent,
    canActivate: [AuthGuardDealerService],
    data: {
      title: 'Pay with Open Account'
    },
  },
  {
    path: 'transfer/:id',
    component: PaymentTransferComponent,
    canActivate: [AuthGuardDealerService],
    data: {
      title: 'Pay with Transfer'
    }
  },
  {
    path: 'editmethod/:id',
    component: EditMethodComponent,
    canActivate: [AuthGuardDealerService],
    data: {
      title: 'Edit Payment Method'
    },
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PaymentRoutingModule {
}





