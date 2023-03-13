import { Component } from '@angular/core';
import { DocumentService } from 'src/app/services/documwent.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {

  columns = [
    {
      header: 'BatchName',
      field: 'batchName'
    },
    {
      header: 'BatchId',
      field: 'batchId'
    },
    {
      header: 'DocumentId',
      field: 'documentHandler'
    },
    {
      header: 'DocumentType',
      field: 'documentType'
    },
    {
      header: 'Pages',
      field: 'pages'
    },
    {
      header: 'DocumentStatus',
      field: 'documentStatus'
    },
    {
      header: 'Date',
      field: 'lastUpdate'
    },
  ]

  documentQueue: Array<DocumentInQueue> = []

  /**
   *
   */
  constructor(private docService: DocumentService) {
    docService.getDocumentInQueue().then((data: any) => {
      this.documentQueue = data;
    })
  }
}

export interface DocumentInQueue {
  batchId: number;
  batchName: string;
  documentType: string;
  documentHandler: string;
  pages: number;
  lastUpdate: Date;
  documentStatus: string;
}
