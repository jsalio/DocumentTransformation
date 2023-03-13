import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DocumentLock } from '../lock/main/main.component';

@Injectable({
  providedIn: 'root'
})
export class AttemptService {

  section = 'attempt'

  constructor(private http: HttpClient) { }

  public getActiveCasesList(): Promise<Array<DocumentLock>> {
    return this.http.get<Array<DocumentLock>>('https://localhost:44367/api/Attempt/list').toPromise();
  }

  public unlockDocuments(documentIds: Array<string>): Promise<Array<number>> {
    return this.http.put<Array<number>>(`https://localhost:44367/api/Attempt/unlock`, documentIds).toPromise();
  }
}
