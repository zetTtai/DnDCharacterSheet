import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';


import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { MobileAccountComponent } from './mobile-account/mobile-account.component';
import { MobileItemsComponent } from './mobile-items/mobile-items.component';
import { MobileLoreComponent } from './mobile-lore/mobile-lore.component';
import { MobileSpellsComponent } from './mobile-spells/mobile-spells.component';
import { SwipeGestureDirective } from '../directives/swipe-gesture/swipe-gesture.directive';

export function createTranslateLoader(http: HttpClient){
  return new TranslateHttpLoader(http, '/assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    MobileLoreComponent,
    MobileItemsComponent,
    MobileSpellsComponent,
    MobileAccountComponent
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
    SwipeGestureDirective
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
