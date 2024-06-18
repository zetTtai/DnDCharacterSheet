import { Injectable } from '@angular/core';

export type Directions = 'top' | 'left' | 'right' | 'bottom';

@Injectable({
  providedIn: 'root'
})
export class ToggleService {

  constructor() { }

  private adjustElementPosition(content: HTMLElement, button: HTMLElement, distance: number, direction: Directions, isExpand: boolean, increase: boolean) {

    const factor = isExpand ? 1 : -1;
    const offset = distance * factor;

    const adjustStyle = (element: HTMLElement, styleProp: string, offset: number) => {
      const computedStyle = window.getComputedStyle(element);
      element.style[styleProp] = `${parseInt(computedStyle[styleProp]) + offset}px`;
    };
    const adjustments = {
      top: () => {
        adjustStyle(content, increase ? 'height' : 'top', offset);
        adjustStyle(button, 'top', -offset);
      },
      left: () => {
        // Special case: To avoid overflow, the content expands/collapses 2 pixels less when expanding
        adjustStyle(content, increase ? 'width' : 'right', isExpand ? offset - 2 : offset + 2);
        adjustStyle(button, 'right', isExpand ? offset - 4 : offset + 4);
      },
      right: () => {
        adjustStyle(content, increase ? 'width' : 'left', offset);
        adjustStyle(button, 'left', isExpand ? offset - 2 : offset + 2);
      },
      bottom: () => {
        adjustStyle(content, increase ? 'height' : 'top', offset);
        adjustStyle(button, 'top', offset);
      },
    };

    if (!adjustments[direction]) {
      console.error('Invalid direction');
      return;
    }

    adjustments[direction]()
  }

  expand(contentId: string, buttonId: string, distance: number, direction: Directions, increase: boolean = false) {
    const content = document.getElementById(contentId);
    const button = document.getElementById(buttonId);

    if (!content || !button) {
      console.error(`Invalid contentId(${contentId}) or buttonId(${buttonId})`);
      return;
    }
    this.adjustElementPosition(content, button, distance, direction, true, increase);
  }

  collapse(contentId: string, buttonId: string, distance: number, direction: Directions, increase: boolean = false) {
    const content = document.getElementById(contentId);
    const button = document.getElementById(buttonId);

    if (!content || !button) {
      console.error('Invalid contentId or buttonId');
      return;
    }

    this.adjustElementPosition(content, button, distance, direction, false, increase);
  }
}
