import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RuleService {

  server: string = "https://localhost:44353/api/Rules";
  constructor(private http: HttpClient) { }

  getRules = () => {
    return this.http.get(`${this.server}/current`).toPromise()
  }

  saveChanges = (rule: ApplicationRule): Promise<ApplicationRule> => {
    return this.http.post(`${this.server}/save`, rule).toPromise() as Promise<ApplicationRule>
  }
}


export interface ApplicationRule {
  enableLog: boolean
  lockFailsElements: boolean;
  tryLimits: number,
  deleteDocumentAfterSync: boolean;
  validateBatchConverttion: boolean;
  enableLocalConfig: boolean;
  enableConsole: boolean;
}
