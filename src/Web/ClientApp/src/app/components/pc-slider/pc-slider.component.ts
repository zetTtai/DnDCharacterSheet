import { Component } from '@angular/core';
import { NavigationService } from '../../core/services/navigation/navigation.service';
import { SharedDataService } from '../../core/services/shared-data/shared-data.service';

@Component({
  selector: 'app-pc-slider',
  templateUrl: './pc-slider.component.html',
  styleUrls: ['./pc-slider.component.scss']
})
export class PcSliderComponent {
  public components: {id: string, name: string}[] = [];

  constructor(
    private navService: NavigationService,
    sharedDataService: SharedDataService
  ) {

    sharedDataService.pcComponents.forEach(component => {
      this.components.push({
        id: `pc-${component.key}`,
        name: `nav-menu.${component.key}`
      })
    })
  }

  pcSlide(view: number) {
    this.navService.pcSlide(view);
  }

  getCurrentView() : number {
    return this.navService.currentViewPc;
  }
}
