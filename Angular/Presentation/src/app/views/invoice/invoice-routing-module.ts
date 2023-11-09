import { NgModule } from '@angular/core';
import { Router, RouterModule, Routes } from '@angular/router';
import { ListComponent } from './list/list.component';
import { AuthGuardDealerService } from 'src/app/services/auth-guard-dealer.service';

const routes: Routes = [
  {
    path: 'list',
    component: ListComponent,
    canActivate: [AuthGuardDealerService],
    data: {
      title: 'Invoices'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InvoiceRoutingModule {
    
  
}
