import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { MainComponent } from './main/main.component';
import LogHubService from '../services/log.hub.service';
import { SharedModule } from '../shared/shared.module';
import { ClarityModule } from '@clr/angular';


@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    SharedModule,
    ClarityModule
  ],
  providers: [LogHubService]
})
export class DashboardModule { }
