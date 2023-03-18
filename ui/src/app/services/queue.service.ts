import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { QueueConfiguration } from '../shared/models/QueueConfiguration';
import BaseApiService from './base.service';

@Injectable({
  providedIn: 'root'
})
export class QueueService extends BaseApiService {
  constructor(private client: HttpClient) {
    super(client, 'queue');
  }

  getQueue = (): Promise<QueueConfiguration> => {
    return this.http.get(`${this.controllerUri}/queue-settings`).toPromise() as Promise<QueueConfiguration>
  }

  saveChanges = (queue: QueueConfiguration): Promise<QueueConfiguration> => {
    return this.http.post(`${this.controllerUri}/save`, queue).toPromise() as Promise<QueueConfiguration>
  }
}


