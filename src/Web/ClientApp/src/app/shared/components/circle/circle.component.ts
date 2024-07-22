import { Component, Input } from '@angular/core';
import { CIRCLE_CONFIG } from 'src/app/shared/constants/app-constants';

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
  @Input() customClass: string;

  defaultMarginRight: string = CIRCLE_CONFIG.MARGIN_RIGHT + 'px';
}
