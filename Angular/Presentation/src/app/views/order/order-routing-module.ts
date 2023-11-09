import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './list/list.component';
import { AddComponent } from './add/add.component';
import { ListDealerComponent } from './list-dealer/list-dealer.component';
import { AuthGuardService } from 'src/app/services/auth-guard.service';
import { AuthGuardDealerService } from 'src/app/services/auth-guard-dealer.service';

const routes: Routes = [
  {
    path: 'list',
    component: ListComponent,
    canActivate: [AuthGuardService],
    data: {
      title: 'Order List Company'
    }
  },
  {
    path: 'list-dealer',
    component: ListDealerComponent,
    canActivate: [AuthGuardDealerService],
    data: {
      title: 'Order List Dealer'
    }
  },
  {
    path: 'add',
    component: AddComponent,
    canActivate: [AuthGuardDealerService],
    data: {
      title: 'Order Create'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrderRoutingModule {
    

}
