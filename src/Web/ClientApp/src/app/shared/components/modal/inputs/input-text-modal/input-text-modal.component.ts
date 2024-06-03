import { Component, Input } from '@angular/core';
import { ModalData } from '../../../../models/modal-data.model';
import { InputModal } from '../../modal.component';

@Component({
  selector: 'app-input-text-modal',
  templateUrl: './input-text-modal.component.html',
  styleUrls: ['./input-text-modal.component.scss']
})
export class InputTextModalComponent implements InputModal {
  @Input() data: ModalData;

  constructor() { }
}
