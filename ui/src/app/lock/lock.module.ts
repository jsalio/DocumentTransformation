import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainComponent } from './main/main.component';
import { LockRoutingModule } from './lock-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ClarityModule } from '@clr/angular';
import { AttemptService } from '../services/attempt.service';
import { DetailComponent } from './detail/detail.component';



@NgModule({
  declarations: [
    MainComponent,
    DetailComponent
  ],
  imports: [
    CommonModule,
    LockRoutingModule,
    SharedModule,
    ClarityModule
  ],
  providers: [
    AttemptService
  ]
})
export class LockModule { }
