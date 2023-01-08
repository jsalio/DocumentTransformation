import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TimerOption } from '../shared/models/TimerOption';
import { TimeUnit } from '../shared/models/TimeUnit';
import { WorkMode } from '../shared/models/WorkMode';

@Injectable({
  providedIn: 'root'
})
export class AppConfigService {
  server: string = "https://localhost:44353/api/ServiceSetting";
  constructor(private http: HttpClient) { }

  getConfig = () => {
    return this.http.get(`${this.server}/service-settings`).toPromise() as Promise<AppSetting>
  }

  saveChange = (updatedSettings: AppSetting) => {
    return this.http.post(`${this.server}/save`, updatedSettings).toPromise()
  }
}

export interface AppSetting {
  workMode: WorkMode
  enableSecondQueue: boolean;
  timerWorkMode: TimerOption;
  timeUnit: TimeUnit;
  interval: number;
  startDate: string;
  timeInit: string | '00:00';
  timeEnd: string | '00:00';
}
