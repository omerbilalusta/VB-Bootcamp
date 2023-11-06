import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './list/list.component';
import { AddComponent } from './add/add.component';
import { ListDealerComponent } from './list-dealer/list-dealer.component';

const routes: Routes = [
  {
    path: 'list',
    component: ListComponent,
    data: {
      title: 'Order List Company'
    }
  },
  {
    path: 'list-dealer',
    component: ListDealerComponent,
    data: {
      title: 'Order List Dealer'
    }
  },
  {
    path: 'add',
    component: AddComponent,
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
