import { Directive, EventEmitter, HostListener, Output } from '@angular/core';

@Directive({
  selector: '[appMouseWheel]'
})
export class MouseWheelDirective {

  @Output() mouseWheelEvent = new EventEmitter<'down' | 'up'>();

  constructor() { }

  @HostListener('window:wheel', ['$event'])
  onWheel(event: WheelEvent) {
    const target = event.target as HTMLElement;

    if (!target.closest('#main-pc')) {
      return;
    }

    if (event.deltaY > 0) {
      this.mouseWheelEvent.emit('down');
    } else {
      this.mouseWheelEvent.emit('up');
    }
  }
}
