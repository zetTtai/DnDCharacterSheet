import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslationModule } from 'src/app/modules/translation.module';
import { SharedModule } from 'src/app/modules/shared.module';
import { AppRoutingModule } from './app-routing.module';

import { PcSlideLayoutComponent } from 'src/app/layouts/pc-slide-layout/pc-slide-layout.component';
import { NavbarComponent } from 'src/app/layouts/navbar/navbar.component';
import { MobileHeaderComponent } from 'src/app/layouts/mobile-header/mobile-header.component';
import { PcFixedLayoutComponent } from 'src/app/layouts/pc-fixed-layout/pc-fixed-layout.component';
import { FixedToggleButtonsComponent } from 'src/app/layouts/fixed-toggle-buttons/fixed-toggle-buttons.component';

@NgModule({
  declarations: [
    PcSlideLayoutComponent,
    NavbarComponent,
    MobileHeaderComponent,
    PcFixedLayoutComponent,
    FixedToggleButtonsComponent
  ],
  imports: [
    CommonModule,
    TranslationModule,
    SharedModule,
    AppRoutingModule
  ],
  exports: [
    PcSlideLayoutComponent,
    NavbarComponent,
    MobileHeaderComponent,
    PcFixedLayoutComponent,
    FixedToggleButtonsComponent
  ]
})
export class LayoutsModule { }
