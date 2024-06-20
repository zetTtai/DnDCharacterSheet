import { Component } from '@angular/core';
import { NavigationService } from '../../core/services/navigation/navigation.service';

@Component({
  selector: 'app-lore',
  templateUrl: './lore.component.html',
  styleUrls: ['./lore.component.scss']
})
export class LoreComponent {
  static key = 'lore';

  constructor(private navService: NavigationService) { }

  onMouseWheel(direction: string) {
    if (direction === 'up') {
      this.navService.pcSlide(this.navService.currentViewPc - 1);
    }
  }
}
