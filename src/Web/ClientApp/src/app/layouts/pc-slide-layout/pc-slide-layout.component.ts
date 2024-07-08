import { Component, Input, ViewEncapsulation } from '@angular/core';
import { NavigationService } from 'src/app/core/services/navigation/navigation.service';

@Component({
  selector: 'app-pc-slide-layout',
  templateUrl: './pc-slide-layout.component.html',
  styleUrl: './pc-slide-layout.component.scss',
  encapsulation: ViewEncapsulation.None
})
export class PcSlideLayoutComponent {
  @Input() leftInputDisabled: boolean = false;
  @Input() leftInputLabel: string = '';
  @Input() gridTemplateColumns: string = 'repeat(1, 1fr)';

  constructor(private navService: NavigationService) { }

  handleSlide(direction: string) {
    if (direction === 'down') {
      if (this.navService.currentViewPc == 2) return;
      this.navService.pcSlide(this.navService.currentViewPc + 1);
      return;
    }
    if (this.navService.currentViewPc == 0) return;
    this.navService.pcSlide(this.navService.currentViewPc - 1);
  }
}
