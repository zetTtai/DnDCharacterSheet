import { Component } from '@angular/core';
import { ModalData } from '../../shared/models/modal-data.model';

@Component({
  selector: 'app-mobile-header',
  templateUrl: './mobile-header.component.html',
  styleUrls: ['./mobile-header.component.scss']
})
export class MobileHeaderComponent {
  isModalVisible: boolean = false;
  data: ModalData;

  openModal(data: ModalData) {
    this.data = data;
    this.isModalVisible = true;
  }

  closeModal() {
    this.isModalVisible = false;
  }
}
