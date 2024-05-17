import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {

  public currentViewPc: number;
  public currentViewMobile: number;

  constructor() { }

  pcSlide(view: number) {
    if (view == this.currentViewPc) return;
    this.currentViewPc = view;
  }
}
