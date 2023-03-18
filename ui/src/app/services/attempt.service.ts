import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CaseDetails, DocumentLock } from '../lock/main/main.component';
import BaseApiService from './base.service';

@Injectable({
  providedIn: 'root'
})
export class AttemptService extends BaseApiService {

  constructor(private client: HttpClient) {
    super(client,'attempt');
  }

  public getActiveCasesList(): Promise<Array<DocumentLock>> {
    return this.http.get<Array<DocumentLock>>(`${this.controllerUri}/list`).toPromise();
  }

  public unlockDocuments(documentIds: Array<number>): Promise<Array<number>> {
    console.log(documentIds);
    return this.http.put<Array<number>>(`${this.controllerUri}/unlock`, documentIds).toPromise();
  }

  public getCaseDetails(documentId: number): Promise<Array<CaseDetails>> {
    return this.http.get<Array<CaseDetails>>(`${this.controllerUri}/${documentId}/details`).toPromise();
  }
}
