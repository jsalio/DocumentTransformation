import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { EngineLicense, PDFEngine } from "../configuration/components/engine-register/engine-register.component";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: 'root'
})

export default class EngineService {
  /**
   *
   */
  constructor(private http: HttpClient) {

  }

  public getEngineList(): Promise<Array<PDFEngine>> {
    return this.http.get<Array<PDFEngine>>(`${environment.apiUrl}/Engine/list`).toPromise();
  }

  public saveChanges(engine: PDFEngine, engineId: number): Promise<PDFEngine> {
    return this.http.put<PDFEngine>(`${environment.apiUrl}/Engine/update/${engineId}`, engine).toPromise();
  }

  public addEngine(engine: PDFEngine): Promise<PDFEngine> {
    return this.http.post<PDFEngine>(`${environment.apiUrl}/Engine/register`, engine).toPromise();
  }

  public deleteEngine(engineId: number): Promise<number> {
    return this.http.delete<number>(`${environment.apiUrl}/Engine/remove/${engineId}`).toPromise();
  }

  public getLicenseByEngineId(engineId: number): Promise<EngineLicense> {
    return this.http.get<EngineLicense>(`${environment.apiUrl}/Engine/${engineId}/license`).toPromise();
  }

  public addLicense(license: EngineLicense, engineId: number): Promise<number> {
    return this.http.post<number>(`${environment.apiUrl}/Engine/${engineId}/license-update}`, license).toPromise();
  }

  public updateLicense(license: EngineLicense, engineId: number): Promise<number> {
    return this.http.put<number>(`${environment.apiUrl}/Engine/${engineId}/license-update`, license).toPromise();
  }
}
