import { Component, HostListener, Type } from '@angular/core';
import { NavigationService } from './core/services/navigation/navigation.service';
import { SharedDataService } from './core/services/shared-data/shared-data.service';
import { HomeComponent } from './components/home/home.component';
import { WEB } from './shared/constants/app-constants';
import { ModalData } from './shared/models/modal-data.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  isModalVisible: boolean = false;
  data: ModalData;
  isDesktop: boolean = window.innerWidth > WEB.MOBILE_SIZE;

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

  openModal(data: ModalData) {
    this.isModalVisible = true;
    this.data = data;
  }

  closeModal() {
    this.isModalVisible = false;
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.isDesktop = window.innerWidth > WEB.MOBILE_SIZE;
  }

  isDesktopView(): boolean {
    return this.isDesktop;
  }
}
