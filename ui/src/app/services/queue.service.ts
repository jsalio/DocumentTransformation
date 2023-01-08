import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class QueueService {
  server: string = "https://localhost:44353/api/queue";
  constructor(private http: HttpClient) { }

  getQueue = (): Promise<QueueConfiguration> => {
    return this.http.get(`${this.server}/queue-settings`).toPromise() as Promise<QueueConfiguration>
  }

  saveChanges = (queue: QueueConfiguration): Promise<QueueConfiguration> => {
    return this.http.post(`${this.server}/save`, queue).toPromise() as Promise<QueueConfiguration>
  }
}

export interface QueueConfiguration {
  serverUrl: string
  queueName: string
  exchange: string
  username: string,
  password: string,
  virtualHost: string,
  port: number,
  prefetchSize: number,
  prefetchCount: number,
  heartbeat: number,
}
