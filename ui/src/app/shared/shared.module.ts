import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { MenuComponent } from './menu/menu.component';

import { ClarityModule } from "@clr/angular";
import { ClarityIcons, userIcon, vmwAppIcon, cogIcon, toolsIcon, searchIcon } from '@cds/core/icon'
import '@cds/core/icon/register.js';
import '@cds/core/input/register.js';
import { LayoutComponent } from './layout/layout.component'
import { TranslateModule } from '@ngx-translate/core';
import { RouterModule } from '@angular/router';
import { SelectorComponent } from './selector/selector.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RadioListComponent } from './radio-list/radio-list.component';
import { LoadingWrapperComponent } from './loading-wrapper/loading-wrapper.component';
import { LoaderService } from './loading-wrapper/loader.service';
ClarityIcons.addIcons(userIcon, vmwAppIcon, cogIcon, toolsIcon, searchIcon)

@NgModule({
  declarations: [
    HeaderComponent,
    MenuComponent,
    LayoutComponent,
    SelectorComponent,
    RadioListComponent,
    LoadingWrapperComponent,
  ],
  imports: [
    CommonModule,
    ClarityModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule.forChild()
  ],
  exports: [
    HeaderComponent,
    MenuComponent,
    LayoutComponent,
    TranslateModule,
    SelectorComponent,
    RadioListComponent,
    LoadingWrapperComponent
  ],
  providers: [LoaderService]
})
export class SharedModule { }
