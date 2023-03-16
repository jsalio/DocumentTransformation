import { Component } from '@angular/core';
import { Router } from '@angular/router';
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
  constructor(private attemptService: AttemptService, private router: Router) {
    this.loadDataSet();
  }

  loadDataSet = () => {
    this.attemptService.getActiveCasesList().then((data) => {
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

    this.attemptService.unlockDocuments([this.currentDocument?.id]).then((data) => {
      this.loadDataSet()
      this.basic = false;
      this.currentDocument = undefined;
    });
  }

  goToDetails = (row: DocumentLock) => {
    this.router.navigate(['lock-items/lock', row.id]);
  }

  multiUnlockHandle = (items: Array<DocumentLock>) => {
    var elements = items.map((item) => item.id);
    this.attemptService.unlockDocuments(elements).then((data) => {
      this.loadDataSet()
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

export interface CaseDetails {
  id: number;
  attemptId: number;
  registryDate: Date;
  errorDetails: string;
  status: CaseStatus
}

export enum CaseStatus {
  Inactive = 'Inactive',
  Active = 'Active',
  Closed = 'Closed'
}
