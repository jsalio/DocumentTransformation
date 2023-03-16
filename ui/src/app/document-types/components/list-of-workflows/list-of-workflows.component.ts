import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PDFEngine } from '../../../configuration/components/engine-register/engine-register.component';

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
  workflowTypes:Array<WorkflowDocumentSettings> = [
    {
      workflowName: 'Workflow Name',
      workflowId: 'Workflow Id',
      workflowActive: false,
      documentTypes: [
        {
          documentTypeId: 0,
          documentTypeName: 'Document Type Name',
          PDF: true,
          OCR: true,
        },
        {
          documentTypeId: 1,
          documentTypeName: 'Document Type Name 2',
          PDF: true,
          OCR: true,
        },
        {
          documentTypeId: 2,
          documentTypeName: 'Document Type Name 3',
          PDF: true,
          OCR: true,
        },
        {
          documentTypeId: 3,
          documentTypeName: 'Document Type Name 4',
          PDF: true,
          OCR: true,
        },
        {
          documentTypeId: 4,
          documentTypeName: 'Document Type Name 5',
          PDF: true,
          OCR: true,
        }
      ]
    },
    {
      workflowName: 'Workflow Name 2',
      workflowId: 'Workflow Id 2',
      workflowActive: false,
      documentTypes: [
        {
          documentTypeId: 0,
          documentTypeName: 'Document Type Name',
          PDF: true,
          OCR: true,
        },
        {
          documentTypeId: 1,
          documentTypeName: 'Document Type Name 2',
          PDF: true,
          OCR: true,
        },
        {
          documentTypeId: 2,
          documentTypeName: 'Document Type Name 3',
          PDF: true,
          OCR: true,
        },
        {
          documentTypeId: 3,
          documentTypeName: 'Document Type Name 4',
          PDF: true,
          OCR: true,
        },
        {
          documentTypeId: 4,
          documentTypeName: 'Document Type Name 5',
          PDF: true,
          OCR: true,
        }
      ]
    }
  ];
  constructor(private router: Router) {
  }

  onRowSelect(event, row: DocumentType, col: string) {
    debugger
    this.workflowTypes.forEach(workflow => {
      workflow.documentTypes.forEach(documentType => {
        if (documentType.documentTypeId === row.documentTypeId) {
          if (col === 'PDF') {
            if (event.target.checked){
              documentType.PDF = event.target.checked;
            } else {
              documentType.PDF = event.target.checked;
              documentType.OCR = event.target.checked;
            }
          } else if (col === 'OCR') {
            if (event.target.checked){
              documentType.OCR = event.target.checked;
              documentType.PDF = event.target.checked;
            } else {
              documentType.OCR = event.target.checked;
            }
          }
        }
      });
    });
    this.dataSetChange = true;
    console.log(this.workflowTypes);
  }



  saveChanges = () => {}
}

export interface DocumentType {
  documentTypeId: number;
  documentTypeName: string;
  PDF: boolean;
  OCR: boolean;
}

export interface WorkflowDocumentSettings {
  workflowName: string;
  workflowId: string;
  workflowActive: boolean;
  documentTypes: DocumentType[];
}
