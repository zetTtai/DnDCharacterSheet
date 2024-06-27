import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrl: './notes.component.scss'
})
export class NotesComponent {
  @Input() isOpen: boolean;

  @Output() toggle = new EventEmitter<void>();
  onToggle() {
    this.toggle.emit();
  }
}
