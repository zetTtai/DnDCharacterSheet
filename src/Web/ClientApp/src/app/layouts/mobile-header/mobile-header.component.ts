import { Component } from '@angular/core';
import { ModalData } from '../../shared/models/modal-data.model';
import { CIRCLE_CONFIG } from '../../shared/constants/app-constants';
import { ToggleService } from '../../core/services/toggle/toggle.service';

@Component({
  selector: 'app-mobile-header',
  templateUrl: './mobile-header.component.html',
  styleUrls: ['./mobile-header.component.scss']
})
export class MobileHeaderComponent {
  isModalVisible: boolean = false;
  isHeaderOpen: boolean = false;
  data: ModalData;
  classInputMaxWidth: number = (CIRCLE_CONFIG.DIAMETER * 2) + (CIRCLE_CONFIG.MARGIN_RIGHT * 2) - 2;

  constructor(private toggleService: ToggleService) { }

  get maxWidth(): string {
    return `${this.classInputMaxWidth}px`;
  }

  openModal(data: ModalData) {
    this.data = data;
    this.isModalVisible = true;
  }

  closeModal() {
    this.isModalVisible = false;
  }

  toggleHeader() {

    if (!this.isHeaderOpen) {
      this.toggleService.expand('mobile-header', 'toggle-header', 300, 'bottom');
      this.isHeaderOpen = true;
      return;
    }

    this.toggleService.collapse('mobile-header', 'toggle-header', 300, 'bottom');
    this.isHeaderOpen = false;

  }
}
