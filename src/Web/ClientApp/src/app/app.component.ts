import { Component, Type } from '@angular/core';
import { NavigationService } from './core/services/navigation/navigation.service';
import { SharedDataService } from './core/services/shared-data/shared-data.service';
import { HomeComponent } from './components/home/home.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  isModalVisible: boolean = false;

  public mobileComponents: { class: Type<any>, key: string }[] = [];
  public pcComponents: { class: Type<any>, key: string }[] = [];

  constructor(
    private navService: NavigationService,
    sharedDataService: SharedDataService
  ) {
    this.mobileComponents = sharedDataService.mobileComponents;
    this.pcComponents = sharedDataService.pcComponents;

    const mobileView = this.mobileComponents.findIndex(comp => comp.class === HomeComponent);
    const pcView = this.pcComponents.findIndex(comp => comp.class === HomeComponent);

    sharedDataService.currentIndex = mobileView;
    navService.currentViewPc = pcView;
  }

  getCurrentViewPc(): number {
    return this.navService.currentViewPc;
  }

  openModal() {
    this.isModalVisible = true;
  }

  closeModal() {
    this.isModalVisible = false;
  }
}
