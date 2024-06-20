import { Component } from '@angular/core';
import { NavigationService } from '../../core/services/navigation/navigation.service';

@Component({
  selector: 'app-spells',
  templateUrl: './spells.component.html',
  styleUrls: ['./spells.component.scss']
})
export class SpellsComponent {
  static key = 'spells';

  constructor(private navService: NavigationService) { }

  onMouseWheel(direction: string) {
    if (direction === 'down') {
      this.navService.pcSlide(this.navService.currentViewPc + 1);
      return;
    }
    this.navService.pcSlide(this.navService.currentViewPc - 1);
  }
}
