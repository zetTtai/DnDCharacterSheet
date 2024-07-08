import { Component, Type } from '@angular/core';
import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';
import { NavigationService } from 'src/app/core/services/navigation/navigation.service';
import { MainViewComponent } from 'src/app/components/main-view/main-view.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  public mobileComponents: { class: Type<any>, key: string }[] = [];
  public pcComponents: { class: Type<any>, key: string }[] = [];

  constructor(
    private navService: NavigationService,
    private sharedDataService: SharedDataService
  ) {
    this.mobileComponents = sharedDataService.mobileComponents;
    this.pcComponents = sharedDataService.pcComponents;

    const mobileView = this.mobileComponents.findIndex(comp => comp.class === MainViewComponent);
    const pcView = this.pcComponents.findIndex(comp => comp.class === MainViewComponent);

    sharedDataService.currentIndex = mobileView;
    navService.currentViewPc = pcView;
  }

  getCurrentViewPc(): number {
    return this.navService.currentViewPc;
  }

  isDesktop(): boolean {
    return this.sharedDataService.isDesktop;
  }
}
