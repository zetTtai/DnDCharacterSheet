import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslationModule } from 'src/app/modules/translation.module';
import { SharedModule } from 'src/app/modules/shared.module';
import { LayoutsModule } from 'src/app/modules/layouts.module';
import { AppRoutingModule } from 'src/app/modules/app-routing.module';


import { SaveButtonComponent } from 'src/app/components/save-button/save-button.component';
import { HomeComponent } from 'src/app/components/home/home.component';
import { PcSliderComponent } from 'src/app/components/pc-slider/pc-slider.component';
import { MobileSliderComponent } from 'src/app/components/mobile-slider/mobile-slider.component';
import { LoreComponent } from 'src/app/components/lore/lore.component';
import { SpellsComponent } from 'src/app/components/spells/spells.component';
import { AccountComponent } from 'src/app/components/account/account.component';
import { LandscapeWarningComponent } from 'src/app/components/landscape-warning/landscape-warning.component';
import { LayoutComponentsModule } from 'src/app/modules/layout-components.module';
import { MainViewComponent } from 'src/app/components/main-view/main-view.component';
import { AboutUsComponent } from 'src/app/components/about-us/about-us.component';


@NgModule({
  declarations: [
    SaveButtonComponent,
    HomeComponent,
    LoreComponent,
    SpellsComponent,
    AccountComponent,
    PcSliderComponent,
    MobileSliderComponent,
    LandscapeWarningComponent,
    MainViewComponent,
    AboutUsComponent
  ],
  imports: [
    CommonModule,
    TranslationModule,
    SharedModule,
    LayoutsModule,
    AppRoutingModule,
    LayoutComponentsModule
  ],
  exports: [
    SaveButtonComponent,
    HomeComponent,
    LoreComponent,
    SpellsComponent,
    AccountComponent,
    PcSliderComponent,
    MobileSliderComponent,
    LandscapeWarningComponent,
    MainViewComponent,
    AboutUsComponent
  ]
})
export class ComponentsModule { }
