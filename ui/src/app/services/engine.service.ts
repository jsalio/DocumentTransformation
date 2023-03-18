import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { EngineLicense, PDFEngine } from "../configuration/components/engine-register/engine-register.component";
import { environment } from "src/environments/environment";
import BaseApiService from "./base.service";

@Injectable({
  providedIn: 'root'
})

export default class EngineService extends BaseApiService {
  /**
   *
   */
  constructor(private client: HttpClient) {
    super(client, 'Engine');
  }

  public getEngineList(): Promise<Array<PDFEngine>> {
    return this.http.get<Array<PDFEngine>>(`${this.controllerUri}/list`).toPromise();
  }

  public saveChanges(engine: PDFEngine, engineId: number): Promise<PDFEngine> {
    return this.http.put<PDFEngine>(`${this.controllerUri}/update/${engineId}`, engine).toPromise();
  }

  public addEngine(engine: PDFEngine): Promise<PDFEngine> {
    return this.http.post<PDFEngine>(`${this.controllerUri}/register`, engine).toPromise();
  }

  public deleteEngine(engineId: number): Promise<number> {
    return this.http.delete<number>(`${this.controllerUri}/remove/${engineId}`).toPromise();
  }

  public getLicenseByEngineId(engineId: number): Promise<EngineLicense> {
    return this.http.get<EngineLicense>(`${this.controllerUri}/${engineId}/license`).toPromise();
  }

  public addLicense(license: EngineLicense, engineId: number): Promise<number> {
    return this.http.post<number>(`${this.controllerUri}/${engineId}/license-update}`, license).toPromise();
  }

  public updateLicense(license: EngineLicense, engineId: number): Promise<number> {
    return this.http.put<number>(`${this.controllerUri}/${engineId}/license-update`, license).toPromise();
  }
}
