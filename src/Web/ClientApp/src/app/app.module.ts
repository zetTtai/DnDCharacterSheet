import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { HammerModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { CircleComponent } from './shared/components/circle/circle.component';
import { FormFieldComponent } from './shared/components/form-field/form-field.component';
import { ModalComponent } from './shared/components/modal/modal.component';
import { ToggleButtonComponent } from './shared/components/toggle-button/toggle-button.component';
import { IconComponent } from './shared/components/icon/icon.component';

import { NavbarComponent } from './layouts/navbar/navbar.component';
import { MobileHeaderComponent } from './layouts/mobile-header/mobile-header.component';
import { SaveButtonComponent } from './components/save-button/save-button.component';
import { HomeComponent } from './components/home/home.component';
import { PcSliderComponent } from './components/pc-slider/pc-slider.component';
import { MobileSliderComponent } from './components/mobile-slider/mobile-slider.component';
import { LoreComponent } from './components/lore/lore.component';
import { ItemsComponent } from './components/items/items.component';
import { SpellsComponent } from './components/spells/spells.component';
import { AccountComponent } from './components/account/account.component';
import { FixedToggleButtonsComponent } from './layouts/fixed-toggle-buttons/fixed-toggle-buttons.component';
import { AbilitiesComponent } from './components/abilities/abilities.component';
import { DeathSavesComponent } from './components/death-saves/death-saves.component';
import { SpellcastingComponent } from './components/spellcasting/spellcasting.component';
import { WalletComponent } from './components/wallet/wallet.component';



export function createTranslateLoader(http: HttpClient){
  return new TranslateHttpLoader(http, '/assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    CircleComponent,
    FormFieldComponent,
    FixedToggleButtonsComponent,
    ToggleButtonComponent,
    ModalComponent,
    NavbarComponent,
    SaveButtonComponent,
    MobileHeaderComponent,
    HomeComponent,
    LoreComponent,
    ItemsComponent,
    SpellsComponent,
    AccountComponent,
    PcSliderComponent,
    MobileSliderComponent,
    AbilitiesComponent,
    DeathSavesComponent,
    SpellcastingComponent,
    WalletComponent,
    IconComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    TranslateModule.forRoot({
      defaultLanguage: 'en',
      useDefaultLang: true,
      loader: {
        provide: TranslateLoader,
        useFactory: createTranslateLoader,
        deps: [HttpClient]
      }
    }),
    RouterModule.forRoot([
      { path: 'items', component: ItemsComponent },
      { path: 'lore', component: LoreComponent },
      { path: 'spells', component: SpellsComponent },
      { path: 'account', component: AccountComponent }
    ]),
    HammerModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
