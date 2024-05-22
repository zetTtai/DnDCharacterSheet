import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-toggle-button',
  templateUrl: './toggle-button.component.html',
  styleUrls: ['./toggle-button.component.scss']
})
export class ToggleButtonComponent {
  @Input() bottom: boolean;
  @Input() top: boolean;
  @Input() left: boolean;
  @Input() right: boolean;
}
