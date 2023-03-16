import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListOfWorkflowsComponent } from './components/list-of-workflows/list-of-workflows.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full'
  },
  {
    path: 'list',
    component: ListOfWorkflowsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DocumentTypeRoutingModule { }
