import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { WorkflowDocumentSettings, WorkflowService, DocumentType } from 'src/app/services/workflow.service';
import { Workflow } from 'src/app/shared/models/Workflow';
import { NotificationService, ToastType } from 'src/app/shared/notification/notification.service';

@Component({
  selector: 'app-list-of-workflows',
  templateUrl: './list-of-workflows.component.html',
  styleUrls: ['./list-of-workflows.component.css']
})
export class ListOfWorkflowsComponent {
  selected: any;
  dataSetChange = false;
  show = false;
  whiteListItem:Workflow[] = [];
  options:any;

  cols = [
    {
      header: 'Document Type id',
      field: 'documentTypeId'
    },
    {
      header: 'Document Type Name',
      field: 'documentTypeName'
    },
    {
      header: 'PDF',
      field: 'PDF'
    },
    {
      header: 'OCR',
      field: 'OCR'
    }
  ];
  workflowTypes:Array<WorkflowDocumentSettings> = [];

  constructor(private router: Router , private workflowService:WorkflowService, private notify: NotificationService) {
    this.workflowService.getDocumentTypes().then(data => {
      this.workflowTypes = data;
    });
  }

  onRowSelect(event, row: DocumentType, col: string) {
    this.workflowTypes.forEach(workflow => {
      workflow.documentConvertSettings.forEach(documentType => {
        if (documentType.documentTypeId === row.documentTypeId) {
          if (col === 'PDF') {
            if (event.target.checked){
              documentType.convertPdf = event.target.checked;
            } else {
              documentType.convertPdf = event.target.checked;
              documentType.supportOcr = event.target.checked;
            }
          } else if (col === 'OCR') {
            if (event.target.checked){
              documentType.supportOcr = event.target.checked;
              documentType.convertPdf = event.target.checked;
            } else {
              documentType.supportOcr = event.target.checked;
            }
          }
          documentType.touched = true;
        }
      });
    });
    this.dataSetChange = true;
  }

  saveUpdateSettings = (workflowId:number) =>{
    const changes = this.workflowTypes.find(x => x.workflowId === workflowId).documentConvertSettings.filter(x => x.touched === true)
    this.workflowService.updateSettings(changes,workflowId).then(data => {
      this.dataSetChange = false;
      this.notify.show({title:'Information', message:'Settings updated successfully'}, ToastType.Success);
    }).catch(error => {
      this.notify.show({title:'Error', message:'Error while updating settings'}, ToastType.Error);
    });
  }

  addSetting = () => {
    this.workflowService.loadActiveElements().then(data => {
      const handlers = this.workflowTypes.map(x => x.workflowId);
      this.whiteListItem = data.filter(x => !handlers.includes(x.handle));
      if (this.whiteListItem.length === 0) {
        this.notify.show({title:'Information', message:'No workflows available to add'}, ToastType.Info);
        return;
      }
      this.show = true
    });
  }

  saveNewConfig = () => {
    this.workflowService.addAddWorkflow(this.options).then(data => {
      this.workflowService.getDocumentTypes().then(data => {
        this.workflowTypes = data;
        this.show = false;
      });
    })
    .catch(error => {
      this.notify.show({title:'Error', message:'Error while adding new workflow'}, ToastType.Error);
    });
  }
}
