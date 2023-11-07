import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './list/list.component';
import { EditComponent } from './edit/edit.component';
import { AddComponent } from './add/add.component';
import { AuthGuardService } from 'src/app/services/auth-guard.service';

const routes: Routes = [
  {
    path: 'list',
    component: ListComponent,
    data: {
      title: 'Product List'
    }
  },
  {
    path: 'edit/:id',
    component: EditComponent,
    canActivate: [AuthGuardService],
    data: {
      title: 'Product Edit'
    }
  },
  {
    path: 'add',
    component: AddComponent,
    canActivate: [AuthGuardService],
    data: {
      title: 'Product Add'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductRoutingModule {
    

}
