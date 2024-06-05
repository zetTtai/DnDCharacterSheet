import { Component, Input } from '@angular/core';
import { ICONS } from '../../constants/app-constants';

@Component({
  selector: 'app-icon',
  templateUrl: './app-icon.component.html',
  styleUrls: ['./app-icon.component.scss']
})
export class AppIconComponent {
  @Input() name: string;
  @Input() width: string;
  @Input() height: string;
  @Input() extension: string = ".svg";

  getIcon(): string {
    return `${ICONS.PATH}${this.name}${this.extension}`
  }
}
