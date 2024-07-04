import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.scss']
})
export class ItemsComponent {
  static key = 'items';
  @Input() isOpen: boolean;

  @Output() toggle = new EventEmitter<void>();
  onToggle() {
    this.toggle.emit();
  }
}
