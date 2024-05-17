import { Directive, EventEmitter, HostListener, Output } from '@angular/core';

@Directive({
  selector: '[appSwipeGesture]',
  standalone: true
})
export class SwipeGestureDirective {

  @Output() swipeLeft = new EventEmitter<void>();
  @Output() swipeRight = new EventEmitter<void>();

  private touchStartX: number = 0;
  private touchEndX: number = 0;

  constructor() {  }

  @HostListener('touchstart', ['$event'])
  onTouchStart(event: TouchEvent) {
      this.touchStartX = event.changedTouches[0].clientX;
  }

  @HostListener('touchend', ['$event'])
  onTouchEnd(event: TouchEvent) {
      this.touchEndX = event.changedTouches[0].clientX;
      this.handleGesture();
  }

  private handleGesture() {
    const deltaX = this.touchEndX - this.touchStartX;
    const minSwipeDistance = 30;

    if (Math.abs(deltaX) > minSwipeDistance) {
      deltaX > 0
        ? this.swipeRight.emit()
        : this.swipeLeft.emit();
    }
  }
}
