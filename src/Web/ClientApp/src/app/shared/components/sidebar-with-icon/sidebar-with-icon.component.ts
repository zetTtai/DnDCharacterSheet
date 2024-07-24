import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-sidebar-with-icon',
  templateUrl: './sidebar-with-icon.component.html',
  styleUrl: './sidebar-with-icon.component.scss'
})
export class SidebarWithIconComponent {
  @Input() iconName: string;
  @Input() title: string;

  @Output() toggle = new EventEmitter<void>();

  toggleSection() {
    this.toggle.emit();
  }
}
