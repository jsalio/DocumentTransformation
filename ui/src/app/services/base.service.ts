import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";

export default class BaseApiService{

  public readonly server :string ;
  public readonly hubServer:string = `${environment.apiHubUrl}`;
  public readonly  controllerUri:string;

  constructor(public http: HttpClient, controllerName:string) {
    this.server = `${environment.apiUrl}`;
    this.controllerUri = `${this.server}/${controllerName}`;
  }
}
