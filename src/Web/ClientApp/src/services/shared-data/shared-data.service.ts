import { Injectable, Type } from '@angular/core';
import { HomeComponent } from '../../app/home/home.component';
import { MobileSpellsComponent } from '../../app/mobile-spells/mobile-spells.component';
import { MobileLoreComponent } from '../../app/mobile-lore/mobile-lore.component';
import { MobileItemsComponent } from '../../app/mobile-items/mobile-items.component';
import { MobileAccountComponent } from '../../app/mobile-account/mobile-account.component';

@Injectable({
  providedIn: 'root'
})
export class SharedDataService {

  public pcComponents: { class: Type<any>, key: string }[] = [
    { class: HomeComponent, key: "home" },
    { class: MobileSpellsComponent, key: "spells" },
    { class: MobileLoreComponent, key: "lore" },
  ];

  public mobileComponents: { class: Type<any>, key: string }[] = [
    { class: MobileLoreComponent, key: "lore" },
    { class: MobileItemsComponent, key: "items" },
    { class: HomeComponent, key: "home" },
    { class: MobileSpellsComponent, key: "spells" },
    { class: MobileAccountComponent, key: "account" }
  ];

  constructor() { }
}
