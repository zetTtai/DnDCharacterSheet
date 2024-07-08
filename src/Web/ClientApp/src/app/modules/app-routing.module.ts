import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ItemsComponent } from 'src/app/components/items/items.component';
import { LoreComponent } from 'src/app/components/lore/lore.component';
import { SpellsComponent } from 'src/app/components/spells/spells.component';
import { AccountComponent } from 'src/app/components/account/account.component';

const routes: Routes = [
  { path: 'items', component: ItemsComponent },
  { path: 'lore', component: LoreComponent },
  { path: 'spells', component: SpellsComponent },
  { path: 'account', component: AccountComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', redirectTo: '/home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
