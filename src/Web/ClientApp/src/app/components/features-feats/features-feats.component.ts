import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-features-feats',
  templateUrl: './features-feats.component.html',
  styleUrl: './features-feats.component.scss'
})
export class FeaturesFeatsComponent {
  @Input() isOpen: boolean;

  @Output() toggle = new EventEmitter<void>();
  onToggle() {
    this.toggle.emit();
  }
}
