import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutUsComponent } from 'src/app/components/about-us/about-us.component';
import { HomeComponent } from 'src/app/components/home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'about-us', component: AboutUsComponent },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
