import { Component, Type } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { SharedDataService } from '../services/shared-data/shared-data.service';
import { NavigationService } from '../services/navigation/navigation.service';

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
    private navService: NavigationService,
    sharedDataService: SharedDataService
  ) {
    this.mobileComponents = sharedDataService.mobileComponents;
    this.pcComponents = sharedDataService.pcComponents;

    const mobileView = this.mobileComponents.findIndex(comp => comp.class === HomeComponent);
    const pcView = this.pcComponents.findIndex(comp => comp.class === HomeComponent);

    this.mobileMainSlide = `-${100 * mobileView}%`;
    this.pcMainSlide = `-${100 * pcView}%`;

    navService.currentViewMobile = mobileView;
    navService.currentViewPc = pcView;
  }


  private mobileSlideBySwipe(view: number) {
    if (view < 0 || view > this.mobileComponents.length - 1) return;
    this.navService.mobileSlide(view, true);
  }

  onSwipeLeft(view: number) {
    this.mobileSlideBySwipe(++view);
  }

  onSwipeRight(view: number) {
    this.mobileSlideBySwipe(--view);
  }

  getCurrentViewPc(): number {
    return this.navService.currentViewPc;
  }

  getCurrentViewMobile(): number {
    return this.navService.currentViewMobile;
  }
}
