import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApplicationRule } from '../shared/models/ApplicationRule';
import BaseApiService from './base.service';

@Injectable({
  providedIn: 'root'
})
export class RuleService extends BaseApiService {

  constructor(private client: HttpClient) {
    super(client, 'Rules');
  }

  getRules = () => {
    return this.http.get(`${this.controllerUri}/current`).toPromise()
  }

  saveChanges = (rule: ApplicationRule): Promise<ApplicationRule> => {
    return this.http.post(`${this.controllerUri}/save`, rule).toPromise() as Promise<ApplicationRule>
  }
}



