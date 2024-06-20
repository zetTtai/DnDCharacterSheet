import { Directive, EventEmitter, HostListener, Output } from '@angular/core';

@Directive({
  selector: '[appScrollListener]'
})
export class ScrollListenerDirective {
  private lastScrollTop = 0;

  @Output() scroll = new EventEmitter<'up' | 'down'>();

  constructor() { }

  @HostListener('window:scroll', ['$event'])
  onWindowScroll(event: Event): void {
    const scrollTop = window.scrollY || document.documentElement.scrollTop;

    if (scrollTop > this.lastScrollTop) {
      this.scroll.emit('down');
    } else {
      this.scroll.emit('up');
    }

    this.lastScrollTop = scrollTop <= 0 ? 0 : scrollTop; // For Mobile or negative scrolling
  }
}
