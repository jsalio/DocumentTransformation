import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NotificationService, ToastType } from './shared/notification/notification.service';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import LogHubService from './services/log.hub.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'pdf-ui-config';

  private connection: HubConnection;

  constructor(private inlinelogService:LogHubService) {
    this.connection = new HubConnectionBuilder()
      .withUrl('http://localhost:63372/loghub')
      .build();
  }

  ngOnInit() {
    this.connection.start().then(() => {
      console.log('connected');
    }).catch(err => console.log('Error while starting connection: ' + err));
    this.connection.on('RecievedLogEvent', (message) => {

    });
  }
}
