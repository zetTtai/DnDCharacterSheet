import { Component, HostListener, Type } from '@angular/core';
import { NavigationService } from 'src/app/core/services/navigation/navigation.service';
import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';
import { HomeComponent } from 'src/app/components/home/home.component';
import { WEB } from 'src/app/shared/constants/app-constants';
import { ModalData } from 'src/app/shared/models/modal-data.model';

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
    private sharedDataService: SharedDataService
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
    this.sharedDataService.isDesktop = this.isDesktop;
  }
}
