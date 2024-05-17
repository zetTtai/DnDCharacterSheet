import { Injectable, Type } from '@angular/core';
import { HomeComponent } from '../../../features/home/home.component';
import { MobileSpellsComponent } from '../../../features/mobile-spells/mobile-spells.component';
import { MobileLoreComponent } from '../../../features/mobile-lore/mobile-lore.component';
import { MobileItemsComponent } from '../../../features/mobile-items/mobile-items.component';
import { MobileAccountComponent } from '../../../features/mobile-account/mobile-account.component';

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

  public currentIndex: number = 0;

  constructor() {

  }
}
