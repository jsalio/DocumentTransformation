import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import LogHubService from 'src/app/services/log.hub.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  loggerDataSet:LogEvent[] = [];
  columns= [
    { field: 'agentId', header: 'Agent Id' },
    { field: 'message', header: 'Message' },
    { field: 'date', header: 'Date' }
  ]

  constructor(private onlineLogService:LogHubService) {
    this.onlineLogService.startHubConnection();
  }

  ngOnInit() {
    this.onlineLogService.subscribeToLog().on('RecievedLogEvent', (message) => {
      this.loggerDataSet.push(message);
      this.loggerDataSet = this.loggerDataSet.sort((a, b) => {
        return new Date(b.date).getTime() - new Date(a.date).getTime();
      });
    });
   }

   clean = () => {
      this.loggerDataSet = [];
   }
}

export interface LogEvent {
  agentId: string
  date: Date
  message: string
}
