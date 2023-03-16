import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CaseDetails, DocumentLock } from '../lock/main/main.component';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AttemptService {

  section = 'attempt'

  constructor(private http: HttpClient) { }

  public getActiveCasesList(): Promise<Array<DocumentLock>> {
    return this.http.get<Array<DocumentLock>>(`${environment.apiUrl}/Attempt/list`).toPromise();
  }

  public unlockDocuments(documentIds: Array<number>): Promise<Array<number>> {
    console.log(documentIds);
    return this.http.put<Array<number>>(`${environment.apiUrl}/Attempt/unlock`, documentIds).toPromise();
  }

  public getCaseDetails(documentId: number): Promise<Array<CaseDetails>> {
    return this.http.get<Array<CaseDetails>>(`${environment.apiUrl}/Attempt/${documentId}/details`).toPromise();
  }
}
