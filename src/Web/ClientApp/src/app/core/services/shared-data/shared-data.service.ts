import { Injectable, Type } from '@angular/core';
import { MainViewComponent } from 'src/app/components/main-view/main-view.component';
import { SpellsComponent } from 'src/app/components/spells/spells.component';
import { LoreComponent } from 'src/app/components/lore/lore.component';
import { ItemsComponent } from 'src/app/components/items/items.component';
import { AccountComponent } from 'src/app/components/account/account.component';
import { WEB } from 'src/app/shared/constants/app-constants';
import { AuthService, User } from '@auth0/auth0-angular';

@Injectable({
  providedIn: 'root'
})
export class SharedDataService {

  private sliderWrapper: HTMLElement;

  public pcComponents: { class: Type<any>, key: string }[] = [
    { class: MainViewComponent, key: MainViewComponent.key },
    { class: SpellsComponent, key: SpellsComponent.key },
    { class: LoreComponent, key: LoreComponent.key },
  ];

  public mobileComponents: { class: Type<any>, key: string }[] = [
    { class: LoreComponent, key: LoreComponent.key },
    { class: ItemsComponent, key: ItemsComponent.key },
    { class: MainViewComponent, key: MainViewComponent.key },
    { class: SpellsComponent, key: SpellsComponent.key },
    { class: AccountComponent, key: AccountComponent.key }
  ];
  public currentIndex: number = 0;
  public isDesktop: boolean = window.innerWidth > WEB.MOBILE_SIZE;
  public userId: string = null;

  constructor(public auth: AuthService) {
    this.sliderWrapper = document.getElementById("sliderWrapper");
  }

  setSliderWrapper(sliderWrapper: HTMLElement) {
    this.sliderWrapper = sliderWrapper;
  }

  getSliderWrapper(): HTMLElement {
    return this.sliderWrapper;
  }

  addTransitionClass() {
    if (this.sliderWrapper) {
      this.sliderWrapper.classList.add('slider-transition');
    }
  }

  removeTransitionClass() {
    if (this.sliderWrapper) {
      this.sliderWrapper.classList.remove('slider-transition');
    }
  }
}
