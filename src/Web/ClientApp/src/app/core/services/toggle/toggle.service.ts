import { Injectable } from '@angular/core';

export type Directions = 'top' | 'left' | 'right' | 'bottom';

@Injectable({
  providedIn: 'root'
})
export class ToggleService {

  constructor() { }

  private adjustElementPosition(content: HTMLElement, button: HTMLElement, distance: number, direction: Directions, isExpand: boolean) {

    const factor = isExpand ? 1 : -1;
    const offset = distance * factor;

    const adjustStyle = (element: HTMLElement, styleProp: string, offset: number) => {
      const computedStyle = window.getComputedStyle(element);
      element.style[styleProp] = `${parseInt(computedStyle[styleProp]) + offset}px`;
    };
    const adjustments = {
      top: () => {
        adjustStyle(content, 'height', offset);
        adjustStyle(button, 'top', -offset);
      },
      left: () => {
        // Special case: To avoid overflow, the content expands/collapses 2 pixels less when expanding
        adjustStyle(content, 'width', isExpand ? offset - 2 : offset);
        adjustStyle(button, 'right', offset);
      },
      right: () => {
        adjustStyle(content, 'width', offset);
        adjustStyle(button, 'left', offset);
      },
      bottom: () => {
        adjustStyle(content, 'height', offset);
        adjustStyle(button, 'top', offset);
      },
    };

    if (!adjustments[direction]) {
      console.error('Invalid direction');
      return;
    }

    adjustments[direction]()
  }

  expand(contentId: string, buttonId: string, distance: number, direction: Directions) {
    const content = document.getElementById(contentId);
    const button = document.getElementById(buttonId);

    if (!content || !button) {
      console.error(`Invalid contentId(${contentId}) or buttonId(${buttonId})`);
      return;
    }

    this.adjustElementPosition(content, button, distance, direction, true);
  }

  collapse(contentId: string, buttonId: string, distance: number, direction: Directions) {
    const content = document.getElementById(contentId);
    const button = document.getElementById(buttonId);

    if (!content || !button) {
      console.error('Invalid contentId or buttonId');
      return;
    }

    this.adjustElementPosition(content, button, distance, direction, false);
  }
}
