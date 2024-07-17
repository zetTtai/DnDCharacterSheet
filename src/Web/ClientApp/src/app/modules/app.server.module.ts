import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { AppComponent } from 'src/app/app.component';
import { AppModule } from 'src/app/app.module';

@NgModule({
    imports: [AppModule, ServerModule],
    bootstrap: [AppComponent]
})
export class AppServerModule { }
