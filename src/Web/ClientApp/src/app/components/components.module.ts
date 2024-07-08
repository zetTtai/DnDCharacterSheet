import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { LayoutsModule } from '../layouts/layouts.module';
import { SharedModule } from '../shared/shared.module';
import { TranslationModule } from '../translation.module';

import { SaveButtonComponent } from './save-button/save-button.component';
import { HomeComponent } from './home/home.component';
import { PcSliderComponent } from './pc-slider/pc-slider.component';
import { MobileSliderComponent } from './mobile-slider/mobile-slider.component';
import { LoreComponent } from './lore/lore.component';
import { ItemsComponent } from './items/items.component';
import { SpellsComponent } from './spells/spells.component';
import { AccountComponent } from './account/account.component';
import { AbilitiesComponent } from './abilities/abilities.component';
import { DeathSavesComponent } from './death-saves/death-saves.component';
import { SpellcastingComponent } from './spellcasting/spellcasting.component';
import { WalletComponent } from './wallet/wallet.component';
import { FeaturesFeatsComponent } from './features-feats/features-feats.component';
import { NotesComponent } from './notes/notes.component';
import { LandscapeWarningComponent } from './landscape-warning/landscape-warning.component';
import { AppModule } from '../app.module';

@NgModule({
  declarations: [
    SaveButtonComponent,
    HomeComponent,
    PcSliderComponent,
    MobileSliderComponent,
    LoreComponent,
    ItemsComponent,
    SpellsComponent,
    AccountComponent,
    AbilitiesComponent,
    DeathSavesComponent,
    SpellcastingComponent,
    WalletComponent,
    FeaturesFeatsComponent,
    NotesComponent,
    LandscapeWarningComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    LayoutsModule,
    SharedModule,
    TranslationModule
  ],
  exports: [
    SaveButtonComponent,
    HomeComponent,
    PcSliderComponent,
    MobileSliderComponent,
    LoreComponent,
    ItemsComponent,
    SpellsComponent,
    AccountComponent,
    AbilitiesComponent,
    DeathSavesComponent,
    SpellcastingComponent,
    WalletComponent,
    FeaturesFeatsComponent,
    NotesComponent,
    LandscapeWarningComponent
  ]
})
export class ComponentsModule { }
