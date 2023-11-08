import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from 'src/app/services/auth-guard.service';
import { BydateComponent } from './bydate/bydate.component';

const routes: Routes = [
  {
    path: 'bydate',
    canActivate: [AuthGuardService],
    component: BydateComponent,
    data: {
      title: 'Report By Date'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRoutingModule {
    

}
