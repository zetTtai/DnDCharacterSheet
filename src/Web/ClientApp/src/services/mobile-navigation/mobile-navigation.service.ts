import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MobileNavigationService {

  private mainMobile: HTMLElement | null = null;

  constructor() { }

  setMainMobile(element: HTMLElement) {
    this.mainMobile = element;
  }

  mobileSlide(view: number, bySwipe: boolean = false) {
    if (!this.mainMobile) return;

    if (bySwipe) {
      this.mainMobile!.classList.add("slider-transition");
      this.mainMobile!.style.transform = `translateX(-${100 * view}%)`;
      return;
    }

    this.mainMobile!.classList.remove("slider-transition");

    const onOpacityTransitionEnd = (event: TransitionEvent) => {
      if (event.propertyName === 'opacity') {
        this.mainMobile!.style.transform = `translateX(-${100 * view}%)`;
        this.mainMobile!.style.opacity = "1";

        this.mainMobile!.removeEventListener('transitionend', onOpacityTransitionEnd);
      }
    };

    this.mainMobile.addEventListener('transitionend', onOpacityTransitionEnd);
    this.mainMobile.style.opacity = "0";
  }
}
