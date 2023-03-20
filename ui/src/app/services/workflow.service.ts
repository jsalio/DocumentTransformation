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

  getDocumentTypes = (): Promise<Array<WorkflowDocumentSettings>> => {
    return this.http.get<Array<WorkflowDocumentSettings>>(`${this.controllerUri}/settings`).toPromise()
  }

  updateSettings = (settings: Array<DocumentType>, workflowId:number) => {
    return this.http.put(`${this.controllerUri}/${workflowId}/update-settings`, settings).toPromise();
  }
}

export interface DocumentType {
  documentTypeId: number;
  documentTypeName: string;
  convertPdf: boolean;
  supportOcr: boolean;
  touched?: boolean;
}

export interface WorkflowDocumentSettings {
  workflowName: string;
  workflowId: number;
  workflowActive: boolean;
  documentConvertSettings: DocumentType[];
}

