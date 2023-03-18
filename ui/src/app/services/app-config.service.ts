import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppSetting } from '../shared/models/AppSetting';
import BaseApiService from './base.service';

@Injectable({
  providedIn: 'root'
})
export class AppConfigService extends BaseApiService {

  constructor(private client: HttpClient) {
    super(client, 'ServiceSetting');
   }

  getConfig = () => {
    return this.http.get(`${this.controllerUri}/service-settings`).toPromise() as Promise<AppSetting>
  }

  saveChange = (updatedSettings: AppSetting) => {
    return this.http.post(`${this.controllerUri}/save`, updatedSettings).toPromise()
  }
}
