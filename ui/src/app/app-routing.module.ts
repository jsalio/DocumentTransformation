import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'queue',
        loadChildren: () => import('./queue/queue.module').then(m => m.QueueModule)
      },
      {
        path: 'config',
        loadChildren: () => import('./configuration/configuration.module').then(m => m.ConfigurationModule)
      },
      {
        path: 'dashboard',
        loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)
      },
      {
        path: 'lock-items',
        loadChildren: () => import('./lock/lock.module').then(m => m.LockModule)
      },
      {
        path: 'document-types',
        loadChildren: () => import('./document-types/document-types.module').then(m => m.DocumentTypesModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
