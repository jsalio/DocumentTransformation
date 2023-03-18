import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { NotificationService, ToastType } from '../shared/notification/notification.service';
import BaseApiService from "./base.service";

@Injectable({
  providedIn: 'root'
})

export default class LogHubService extends BaseApiService {
  private serviceHubConnection: HubConnection;
  connectionIsActive:boolean = false;

  constructor(private notify: NotificationService, private client: HttpClient) {
    super(client,'');
    this.serviceHubConnection = new HubConnectionBuilder()
    .withUrl(`${this.hubServer}/loghub`)
    .build();

  }

  public startHubConnection =(): void =>{
    this.serviceHubConnection.start().then(() => {
      console.log('connected');
      this.connectionIsActive = true;
    }).catch(err => {
      console.log('Error while starting connection: ' + err)
      this.connectionIsActive = false;
      this.notify.show({title: 'Error', message: 'Error while starting connection'}, ToastType.Error);
    });
  }
  /**
      * Gets the HubConnection
      *
      * @returns  A HubConnection
      * @memberof HubService
      */
  public subscribeToLog(): HubConnection {
    return this.serviceHubConnection
  }
}
