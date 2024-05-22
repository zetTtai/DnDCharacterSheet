import { Component } from '@angular/core';
import { ModalData } from '../../shared/models/modal-data.model';
import { CIRCLE_CONFIG } from '../../shared/constants/app-constants';

@Component({
  selector: 'app-mobile-header',
  templateUrl: './mobile-header.component.html',
  styleUrls: ['./mobile-header.component.scss']
})
export class MobileHeaderComponent {
  isModalVisible: boolean = false;
  data: ModalData;
  classInputMaxWidth: number = CIRCLE_CONFIG.DIAMETER + (CIRCLE_CONFIG.MARGIN_RIGHT * 2);

  openModal(data: ModalData) {
    this.data = data;
    this.isModalVisible = true;
  }

  closeModal() {
    this.isModalVisible = false;
  }

  expandHeader() {
    alert("EY");
  }
}
