import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { HammerModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { TranslationModule } from 'src/app/modules/translation.module';
import { AppRoutingModule } from 'src/app/modules/app-routing.module';
import { SharedModule } from 'src/app/modules/shared.module';
import { LayoutsModule } from 'src/app/modules/layouts.module';
import { ComponentsModule } from 'src/app/modules/components.module';

import { AppComponent } from 'src/app/app.component';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    HammerModule,
    ReactiveFormsModule,
    TranslationModule,
    AppRoutingModule,
    LayoutsModule,
    SharedModule,
    ComponentsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
