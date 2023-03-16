import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainComponent } from './main/main.component';
import { DocumentTypeRoutingModule } from './document-type-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ClarityModule } from '@clr/angular';
import { ListOfWorkflowsComponent } from './components/list-of-workflows/list-of-workflows.component';

@NgModule({
  declarations: [
    MainComponent,
    ListOfWorkflowsComponent
  ],
  imports: [
    CommonModule,
    DocumentTypeRoutingModule,
    SharedModule,
    ClarityModule
  ]
})
export class DocumentTypesModule { }
