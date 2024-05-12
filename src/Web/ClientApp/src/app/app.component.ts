import { Component, Type } from '@angular/core';
import { MobileLoreComponent } from './mobile-lore/mobile-lore.component';
import { MobileItemsComponent } from './mobile-items/mobile-items.component';
import { HomeComponent } from './home/home.component';
import { MobileSpellsComponent } from './mobile-spells/mobile-spells.component';
import { MobileAccountComponent } from './mobile-account/mobile-account.component';
import { MobileNavigationService } from '../services/mobile-navigation/mobile-navigation.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  public pcComponents: Type<any>[] = [
    HomeComponent,
    MobileSpellsComponent
  ]

  public mobileComponents: Type<any>[] = [
    MobileLoreComponent,
    MobileItemsComponent,
    HomeComponent,
    MobileSpellsComponent,
    MobileAccountComponent
  ]

  public mobileMainSlide: string;
  public pcMainSlide: string;

  constructor(private mobileNavService: MobileNavigationService) {
    this.mobileMainSlide = `-${100 * this.mobileComponents.findIndex(comp => comp === HomeComponent)}%`;
    this.pcMainSlide = `-${100 * this.pcComponents.findIndex(comp => comp === HomeComponent)}%`;
  }


  private mobileSlideBySwipe(view: number) {
    if (view < 0 || view > this.mobileComponents.length) return;
    this.mobileNavService.mobileSlide(view, true);
  }

  onSwipeLeft(view: number) {
    this.mobileSlideBySwipe(++view);
  }

  onSwipeRight(view: number) {
    this.mobileSlideBySwipe(--view);
  }
}
