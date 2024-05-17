import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { HammerModule } from '@angular/platform-browser';


import { AppComponent } from './app.component';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { SwipeGestureDirective } from './shared/directives/swipe-gesture/swipe-gesture.directive';


import { NavbarComponent } from './layouts/navbar/navbar.component';
import { MobileHeaderComponent } from './layouts/mobile-header/mobile-header.component';
import { SaveButtonComponent } from './features/save-button/save-button.component';
import { HomeComponent } from './features/home/home.component';
import { MobileLoreComponent } from './features/mobile-lore/mobile-lore.component';
import { MobileItemsComponent } from './features/mobile-items/mobile-items.component';
import { MobileSpellsComponent } from './features/mobile-spells/mobile-spells.component';
import { MobileAccountComponent } from './features/mobile-account/mobile-account.component';
import { PcSliderComponent } from './features/pc-slider/pc-slider.component';
import { MobileSliderComponent } from './features/mobile-slider/mobile-slider.component';



export function createTranslateLoader(http: HttpClient){
  return new TranslateHttpLoader(http, '/assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    SaveButtonComponent,
    MobileHeaderComponent,
    HomeComponent,
    MobileLoreComponent,
    MobileItemsComponent,
    MobileSpellsComponent,
    MobileAccountComponent,
    PcSliderComponent,
    MobileSliderComponent
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
      { path: 'items', component: MobileItemsComponent },
      { path: 'lore', component: MobileLoreComponent },
      { path: 'spells', component: MobileSpellsComponent },
      { path: 'account', component: MobileAccountComponent }
    ]),
    SwipeGestureDirective,
    HammerModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
