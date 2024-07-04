import { Directive, EventEmitter, HostListener, Output } from '@angular/core';

@Directive({
  selector: '[appSlideControl]'
})
export class SlideControlDirective {

  @Output() mouseWheelEvent = new EventEmitter<'down' | 'up'>();
  @Output() keydownEvent = new EventEmitter<'down' | 'up'>();

  constructor() { }

  @HostListener('window:wheel', ['$event'])
  onWheel(event: WheelEvent) {
    const target = event.target as HTMLElement;

    if (!target.closest('#main-pc')) {
      return;
    }

    if (event.deltaY > 0) {
      this.mouseWheelEvent.emit('down');
      return;
    }

    this.mouseWheelEvent.emit('up');
  }

  @HostListener('window:keydown', ['$event'])
  onKeydown(event: KeyboardEvent) {
    const key = event.key;

    if (key === 'ArrowDown') {
      this.keydownEvent.emit('down');
      return;
    }

    if (key === 'ArrowUp') {
      this.keydownEvent.emit('up');
      return;
    }
  }
}
