import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Workflow } from '../shared/models/Workflow';
import BaseApiService from './base.service';

@Injectable({
  providedIn: 'root'
})
export class WorkflowService extends BaseApiService {

  constructor(private client: HttpClient) {
    super(client, 'workflow');
  }

  loadAvailable = (): Promise<Array<Workflow>> => {
    return this.http.get(`${this.controllerUri}/capture-available`).toPromise() as Promise<Array<Workflow>>;
  }
  loadActiveElements = (): Promise<Array<Workflow>> => {
    return this.http.get(`${this.controllerUri}/white-list`).toPromise() as Promise<Array<Workflow>>;
  }
  saveChanges = (workflows: Array<Workflow>) => {
    return this.http.post(`${this.controllerUri}/save`, workflows).toPromise();
  }
}
