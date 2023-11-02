import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from '../product/list/list.component';

const routes: Routes = [
  {
    path: 'product',
    component: ListComponent,
    data: {
      title: 'Products'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DealerRoutingModule {
    

}
