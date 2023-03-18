import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import BaseApiService from './base.service';

@Injectable({
  providedIn: 'root'
})
export class DocumentService extends BaseApiService {

  constructor(private client: HttpClient) {
    super(client, 'Document');
   }

  public getDocumentInQueue(): Promise<any> {
    return this.http.get(`${this.controllerUri}/document-queue`).toPromise();
  }
}
