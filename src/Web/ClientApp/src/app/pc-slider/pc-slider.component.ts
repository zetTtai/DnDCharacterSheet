import { Component } from '@angular/core';
import { SharedDataService } from '../../services/shared-data/shared-data.service';
import { NavigationService } from '../../services/navigation/navigation.service';

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

  ngOnInit() {
    const mainPc = document.getElementById("main-pc");

    if (mainPc) {
      this.navService.setMainPc(mainPc);
    }
  }

  pcSlide(view: number) {
    this.navService.pcSlide(view);
  }

  getCurrentView() : number {
    return this.navService.currentViewPc;
  }
}
