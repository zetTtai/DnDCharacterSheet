import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutUsComponent } from 'src/app/components/about-us/about-us.component';
import { MainViewComponent } from 'src/app/components/main-view/main-view.component';

const routes: Routes = [
  { path: '', component: MainViewComponent},
  { path: 'about-us', component: AboutUsComponent },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
