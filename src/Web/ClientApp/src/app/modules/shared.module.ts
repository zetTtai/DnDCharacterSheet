import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TranslationModule } from './translation.module';

import { SlideControlDirective } from 'src/app/shared/directives/slide-control/slide-control.directive';

import { CircleComponent } from 'src/app/shared/components/circle/circle.component';
import { FormFieldComponent } from 'src/app/shared/components/form-field/form-field.component';
import { ModalComponent } from 'src/app/shared/components/modal/modal.component';
import { ToggleButtonComponent } from 'src/app/shared/components/toggle-button/toggle-button.component';
import { AppIconComponent } from 'src/app/shared/components/app-icon/app-icon.component';
import { InputTextModalComponent } from 'src/app/shared/components/modal/input-modal/input-text-modal/input-text-modal.component';
import { ValidationMessagesComponent } from 'src/app/shared/components/validation-messages/validation-messages.component';
import { SidebarWithIconComponent } from 'src/app/shared/components/sidebar-with-icon/sidebar-with-icon.component';

import { AbilitiesComponent } from 'src/app/components/abilities/abilities.component';
import { DeathSavesComponent } from 'src/app/components/death-saves/death-saves.component';
import { SpellcastingComponent } from 'src/app/components/spellcasting/spellcasting.component';
import { WalletComponent } from 'src/app/components/wallet/wallet.component';
import { FeaturesFeatsComponent } from '../components/features-feats/features-feats.component';
import { ItemsComponent } from '../components/items/items.component';
import { NotesComponent } from '../components/notes/notes.component';

@NgModule({
  declarations: [
    SlideControlDirective,
    CircleComponent,
    FormFieldComponent,
    ModalComponent,
    ToggleButtonComponent,
    AppIconComponent,
    InputTextModalComponent,
    ValidationMessagesComponent,
    SidebarWithIconComponent,
    AbilitiesComponent,
    DeathSavesComponent,
    SpellcastingComponent,
    WalletComponent,
    FeaturesFeatsComponent,
    ItemsComponent,
    NotesComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TranslationModule
  ],
  exports: [
    SlideControlDirective,
    CircleComponent,
    FormFieldComponent,
    ModalComponent,
    ToggleButtonComponent,
    AppIconComponent,
    InputTextModalComponent,
    ValidationMessagesComponent,
    SidebarWithIconComponent,
    AbilitiesComponent,
    DeathSavesComponent,
    SpellcastingComponent,
    WalletComponent,
    FeaturesFeatsComponent,
    ItemsComponent,
    NotesComponent
  ]
})
export class SharedModule { }
