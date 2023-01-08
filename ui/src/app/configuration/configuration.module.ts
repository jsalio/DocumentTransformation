import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ConfigurationRoutingModule } from './configuration-routing.module';
import { MainComponent } from './main/main.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ClarityModule } from '@clr/angular';
import { TranslateModule } from '@ngx-translate/core';
import { ScheduleComponent } from './components/schedule/schedule.component';
import { WorkflowsComponent } from './components/workflows/workflows.component';
import { RulesComponent } from './components/rules/rules.component';
import { DocumentTypesComponent } from './components/document-types/document-types.component';
import { SharedModule } from '../shared/shared.module';
import { QueueComponent } from './components/queue/queue.component';
import { QueueService } from '../services/queue.service';
import { LoaderService } from '../shared/loading-wrapper/loader.service';
import { AppConfigService } from '../services/app-config.service';
import { NotificationService } from '../shared/notification/notification.service';

@NgModule({
  declarations: [
    MainComponent,
    ScheduleComponent,
    WorkflowsComponent,
    RulesComponent,
    DocumentTypesComponent,
    QueueComponent
  ],
  imports: [
    CommonModule,
    ConfigurationRoutingModule,
    ClarityModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule.forChild(),
    SharedModule
  ],
  providers: [QueueService, LoaderService, AppConfigService, NotificationService]
})
export class ConfigurationModule { }
