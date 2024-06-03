import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ModalData } from '../../../../models/modal-data.model';
import { InputModal } from '../../modal.component';

@Component({
  selector: 'app-input-text-modal',
  templateUrl: './input-text-modal.component.html',
  styleUrls: ['./input-text-modal.component.scss']
})
export class InputTextModalComponent implements InputModal {
  @Input() data: ModalData;
  @Output() cancel = new EventEmitter<void>();

  constructor() { }

  onCancel(event: Event) {
    event.preventDefault();
    this.cancel.emit();
  }

  onSubmit(event: Event) {
    event.preventDefault();
    console.log("Eing");
  }
}
