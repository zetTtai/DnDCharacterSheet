import { Component, Type } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { MobileNavigationService } from '../services/mobile-navigation/mobile-navigation.service';
import { SharedDataService } from '../services/shared-data/shared-data.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  public mobileComponents: { class: Type<any>, key: string }[] = [];
  public pcComponents: { class: Type<any>, key: string }[] = [];

  public mobileMainSlide: string;
  public pcMainSlide: string;

  constructor(
    private mobileNavService: MobileNavigationService,
    sharedDataService: SharedDataService
  ) {
    this.mobileComponents = sharedDataService.mobileComponents;
    this.pcComponents = sharedDataService.pcComponents;

    this.mobileMainSlide = `-${100 * this.mobileComponents.findIndex(comp => comp.class === HomeComponent)}%`;
    this.pcMainSlide = `-${100 * this.pcComponents.findIndex(comp => comp.class === HomeComponent)}%`;
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
