import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TranslationModule } from './translation.module';
import { SharedModule } from './shared.module';


import { AbilitiesComponent } from 'src/app/components/abilities/abilities.component';
import { DeathSavesComponent } from 'src/app/components/death-saves/death-saves.component';
import { SpellcastingComponent } from 'src/app/components/spellcasting/spellcasting.component';
import { WalletComponent } from 'src/app/components/wallet/wallet.component';
import { FeaturesFeatsComponent } from 'src/app/components/features-feats/features-feats.component';
import { ItemsComponent } from 'src/app/components/items/items.component';
import { NotesComponent } from 'src/app/components/notes/notes.component';
import { AccountComponent } from 'src/app/components/account/account.component';

@NgModule({
  declarations: [
    AbilitiesComponent,
    DeathSavesComponent,
    SpellcastingComponent,
    WalletComponent,
    FeaturesFeatsComponent,
    ItemsComponent,
    NotesComponent,
    AccountComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TranslationModule,
    SharedModule
  ],
  exports: [
    AbilitiesComponent,
    DeathSavesComponent,
    SpellcastingComponent,
    WalletComponent,
    FeaturesFeatsComponent,
    ItemsComponent,
    NotesComponent,
    AccountComponent
  ]
})
export class LayoutComponentsModule { }
