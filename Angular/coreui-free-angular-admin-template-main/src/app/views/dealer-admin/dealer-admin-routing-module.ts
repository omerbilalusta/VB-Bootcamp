import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './list/list.component';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
import { AuthGuardService } from 'src/app/services/auth-guard.service';

const routes: Routes = [
  {
    path: 'list',
    component: ListComponent,
    canActivate: [AuthGuardService],
    data: {
      title: 'Dealers'
    }
  },
  {
    path: 'edit/:id',
    component: EditComponent,
    canActivate: [AuthGuardService],
    data: {
      title: 'Dealer Edit'
    }
  },
  {
    path: 'add',
    component: AddComponent,
    canActivate: [AuthGuardService],
    data: {
      title: 'Dealer Add'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DealerAdminRoutingModule {
    

}
