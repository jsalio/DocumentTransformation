import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainComponent } from './main/main.component';
import { DocumentTypeRoutingModule } from './document-type-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ClarityModule } from '@clr/angular';
import { ListOfWorkflowsComponent } from './components/list-of-workflows/list-of-workflows.component';
import { WorkflowService } from '../services/workflow.service';
import { NotificationService } from '../shared/notification/notification.service';
import { FormsModule,ReactiveFormsModule } from '@angular/forms'

@NgModule({
  declarations: [
    MainComponent,
    ListOfWorkflowsComponent
  ],
  imports: [
    CommonModule,
    DocumentTypeRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    ClarityModule
  ],
  providers:[WorkflowService, NotificationService]
})
export class DocumentTypesModule { }
