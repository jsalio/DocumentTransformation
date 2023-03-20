import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { WorkflowDocumentSettings, WorkflowService, DocumentType } from 'src/app/services/workflow.service';
import { NotificationService, ToastType } from 'src/app/shared/notification/notification.service';

@Component({
  selector: 'app-list-of-workflows',
  templateUrl: './list-of-workflows.component.html',
  styleUrls: ['./list-of-workflows.component.css']
})
export class ListOfWorkflowsComponent {
  selected: any;
  dataSetChange = false;
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
    var changes = this.workflowTypes.find(x => x.workflowId === workflowId).documentConvertSettings.filter(x => x.touched === true)
    this.workflowService.updateSettings(changes,workflowId).then(data => {
      this.dataSetChange = false;
      this.notify.show({title:'Information', message:'Settings updated successfully'}, ToastType.Success);
    });
  }



  saveChanges = () => {}
}
