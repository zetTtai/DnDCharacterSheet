import { Component } from '@angular/core';
import { NavigationService } from '../../core/services/navigation/navigation.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  static key = 'home';

  constructor(private navService: NavigationService) { }

  handleSlide(direction: string) {
    if (direction === 'down') {
      this.navService.pcSlide(this.navService.currentViewPc + 1);
    }
  }
}
