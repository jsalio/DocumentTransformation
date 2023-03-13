import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { QueueRoutingModule } from './queue-routing.module';
import { MainComponent } from './main/main.component';
import '@cds/core/input/register.js'
import { ClarityModule } from "@clr/angular";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '../shared/shared.module';
import { DocumentService } from '../services/documwent.service';

@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    CommonModule,
    QueueRoutingModule,
    ClarityModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    TranslateModule.forChild()
  ],
  providers: [DocumentService]
})
export class QueueModule { }
