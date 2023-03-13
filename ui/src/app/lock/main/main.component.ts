import { Component } from '@angular/core';
import { AttemptService } from 'src/app/services/attempt.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {

  basic: any = false;
  currentDocument?: DocumentLock;


  selected = [];
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
      header: 'FailedAttempts',
      field: 'attempt'
    }
  ]

  documentLocks: Array<DocumentLock> = [];

  /**
   *
   */
  constructor(private attemptService: AttemptService) {
    this.attemptService.getActiveCasesList().then((data) => {
      console.log(data);
      this.documentLocks = data;
    });
  }

  unlockDocument = (row: DocumentLock) => {
    this.currentDocument = row;
    this.basic = true;
  }

  unlockSelectedDocument = () => {
    this.basic = true;
  }

  okHandler = () => {
    this.basic = false;
    this.currentDocument = undefined;
  }

  multiUnlockHandle = (items: Array<DocumentLock>) => {
    var elements = items.map((item) => item.id.toString());
    this.attemptService.unlockDocuments(elements).then((data) => {
      console.log(data);
    });
  }
}

export interface DocumentLock {
  id: number;
  batchId: number;
  batchName: string;
  documentType: string;
  documentHandler: string;
  attempt: number;
}
