import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DefaultLayoutComponent } from './containers';
import { Page404Component } from './views/pages/page404/page404.component';
import { Page500Component } from './views/pages/page500/page500.component';
import { LoginComponent } from './views/pages/login/login.component';
import { RegisterComponent } from './views/pages/register/register.component';
import { AuthGuardService} from './services/auth-guard.service'

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'report',
        loadChildren: () =>
          import('./views/report/report.module').then((m) => m.ReportModule)
      },
      {
        path: 'invoice',
        loadChildren: () =>
          import('./views/invoice/invoice.module').then((m) => m.InvoiceModule)
      },
      {
        path: 'adminDealer',
        loadChildren: () =>
          import('./views/dealer-admin/dealer-admin.module').then((m) => m.DealerAdminModule)
      },
      {
        path: 'payment',
        loadChildren: () =>
          import('./views/payment/payment.module').then((m) => m.PaymentModule)
      },
      {
        path: 'order',
        loadChildren: () =>
          import('./views/order/order.module').then((m) => m.OrderModule)
      },
      {
        path: 'product',
        loadChildren: () =>
          import('./views/product/product.module').then((m) => m.ProductModule)
      },
      {
        path: 'dealer',
        loadChildren: () =>
          import('./views/dealer/dealer.module').then((m) => m.DealerModule)
      },
      {
        path: 'theme',
        loadChildren: () =>
          import('./views/theme/theme.module').then((m) => m.ThemeModule)
      },
      {
        path: 'pages',
        loadChildren: () =>
          import('./views/pages/pages.module').then((m) => m.PagesModule)
      },
    ]
  },
  {
    path: '404',
    component: Page404Component,
    data: {
      title: 'Page 404'
    }
  },
  {
    path: '500',
    component: Page500Component,
    data: {
      title: 'Page 500'
    }
  },
  {
    path: 'login',
    component: LoginComponent,
    data: {
      title: 'Login Page'
    }
  },
  {
    path: 'register',
    component: RegisterComponent,
    data: {
      title: 'Register Page'
    }
  },
  {path: '**', redirectTo: 'dashboard'}
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'top',
      anchorScrolling: 'enabled',
      initialNavigation: 'enabledBlocking'
      // relativeLinkResolution: 'legacy'
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
  constructor(private authGuard:AuthGuardService) {}
}
