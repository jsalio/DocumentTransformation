import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EngineFormComponent } from './components/engine-form/engine-form.component';
import { LicenseViewComponent } from './components/license-view/license-view.component';
import { MainComponent } from './main/main.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: MainComponent
  },
  {
    path: 'engine/:id',
    component: EngineFormComponent
  },
  {
    path: 'engine',
    component: EngineFormComponent
  },
  {
    path: 'engine/license/:id',
    component: LicenseViewComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConfigurationRoutingModule { }
