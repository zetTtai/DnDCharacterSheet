import { Injectable, Type } from '@angular/core';
import { HomeComponent } from '../../../components/home/home.component';
import { SpellsComponent } from '../../../components/spells/spells.component';
import { LoreComponent } from '../../../components/lore/lore.component';
import { ItemsComponent } from '../../../components/items/items.component';
import { AccountComponent } from '../../../components/account/account.component';

@Injectable({
  providedIn: 'root'
})
export class SharedDataService {

  private sliderWrapper: HTMLElement;

  public pcComponents: { class: Type<any>, key: string }[] = [
    { class: HomeComponent, key: HomeComponent.key },
    { class: SpellsComponent, key: SpellsComponent.key },
    { class: LoreComponent, key: LoreComponent.key },
  ];

  public mobileComponents: { class: Type<any>, key: string }[] = [
    { class: LoreComponent, key: LoreComponent.key },
    { class: ItemsComponent, key: ItemsComponent.key },
    { class: HomeComponent, key: HomeComponent.key },
    { class: SpellsComponent, key: SpellsComponent.key },
    { class: AccountComponent, key: AccountComponent.key }
  ];

  public currentIndex: number = 0;

  constructor() {
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
