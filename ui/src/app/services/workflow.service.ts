import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Workflow } from '../shared/models/Workflow';

@Injectable({
  providedIn: 'root'
})
export class WorkflowService {
  server: string = "https://localhost:44353/api/workflow";
  constructor(private http: HttpClient) { }

  loadAvailable = (): Promise<Array<Workflow>> => {
    return this.http.get(`${this.server}/capture-available`).toPromise() as Promise<Array<Workflow>>;
  }
  loadActiveElements = (): Promise<Array<Workflow>> => {
    return this.http.get(`${this.server}/white-list`).toPromise() as Promise<Array<Workflow>>;
  }
  saveChanges = (workflows: Array<Workflow>) => {
    return this.http.post(`${this.server}/save`, workflows).toPromise();
  }
}