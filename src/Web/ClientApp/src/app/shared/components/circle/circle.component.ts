import { Component, Input } from '@angular/core';
import { CIRCLE_CONFIG } from '../../constants/app-constants';

@Component({
  selector: 'app-circle',
  templateUrl: './circle.component.html',
  styleUrls: ['./circle.component.scss']
})
export class CircleComponent {
  @Input() diameter: number = CIRCLE_CONFIG.DIAMETER;
  @Input() empty: boolean = true;
  @Input() disabled: boolean = false;
  @Input() id: string;

  defaultMarginRight: string = CIRCLE_CONFIG.MARGIN_RIGHT + 'px';
}
