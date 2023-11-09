import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    title: true,
    name: 'Products'
  },
  {
    name: 'Products',
    url: '/product/list',
    iconComponent: { name: 'cil-speedometer' }
  },
  {
    title: true,
    name: 'Admin'
  },
  {
    name: 'Dealer',
    url: '/admin-dealer',
    iconComponent: { name: 'cil-speedometer' },
    children: [
          {
            name: 'Dealers List',
            url: '/adminDealer/list'
          },
          {
            name: 'Dealer Add',
            url: '/adminDealer/add'
          }
      ]
  },
  {
    name: 'Order List',
    url: '/order/list',
    iconComponent: { name: 'cil-pencil' }
  },
  {
    name: 'Product Add',
    url: '/product/add',
    iconComponent: { name: 'cil-check' }
  },
  {
    name: 'Report',
    url: '/report/bydate',
    iconComponent: { name: 'cil-chart-pie' }
  },
  {
    title: true,
    name: 'Dealer'
  },
  {
    name: 'Order',
    url: '/dealer-order',
    iconComponent: { name: 'cil-basket' },
    children: [
      {
        name: 'Create Order',
        url: '/order/add'
      },
      {
        name: 'Order List',
        url: '/order/list-dealer'
      }
    ]
  },
  {
    name: 'Invoices',
    url: '/invoice/list',
    iconComponent: { name: 'cil-chart' }
  }
];
