import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  constructor(private http: HttpClient) { }

  public getDocumentInQueue(): Promise<any> {
    return this.http.get('https://localhost:44367/api/Document/document-queue').toPromise();
  }
}
