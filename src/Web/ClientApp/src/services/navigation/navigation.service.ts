import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {

  private mainMobile: HTMLElement | null = null;
  private mainPc: HTMLElement | null = null;
  public currentViewPc: number;
  public currentViewMobile: number;

  constructor() { }

  private opacityTransition(main: HTMLElement, view: number) {
    const onOpacityTransitionEnd = (event: TransitionEvent) => {
      if (event.propertyName === 'opacity') {
        main.style.transform = `translateX(-${100 * view}%)`;
        main.style.opacity = "1";

        main.removeEventListener('transitionend', onOpacityTransitionEnd);
      }
    };
    main.addEventListener('transitionend', onOpacityTransitionEnd);
    main.style.opacity = "0";
  }

  setMainMobile(element: HTMLElement) {
    this.mainMobile = element;
  }

  setMainPc(element: HTMLElement) {
    this.mainPc = element;
  }

  pcSlide(view: number) {
    if (!this.mainPc || view == this.currentViewPc) return;


    this.opacityTransition(this.mainPc, view);
    this.currentViewPc = view;
  }

  mobileSlide(view: number, bySwipe: boolean = false) {
    if (!this.mainMobile || view == this.currentViewMobile) return;

    if (bySwipe) {
      this.mainMobile!.classList.add("slider-transition");

      this.mainMobile!.style.transform = `translateX(-${100 * view}%)`;
      this.currentViewMobile = view;
      return;
    }

    this.mainMobile!.classList.remove("slider-transition");

    this.opacityTransition(this.mainMobile, view);
    this.currentViewMobile = view;
  }
}
