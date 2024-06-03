import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DelayService {

  constructor() { }

  getDelayInSeconds(id: string): number {
    const element = document.getElementById(id);
    if (!element) return 0;

    return parseFloat(getComputedStyle(element).transitionDuration) * 1000;
  }
}
