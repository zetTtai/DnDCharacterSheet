import { Component, EventEmitter, Input, Output } from '@angular/core';

interface ModalData {
  type: string;
  label: string;
  value: any;
}

@Component({
  selector: 'app-form-field',
  templateUrl: './form-field.component.html',
  styleUrls: ['./form-field.component.scss']
})
export class FormFieldComponent {
  @Input() label: string;
  @Input() type: string = 'text';
  @Input() name: string;
  @Input() value: any;
  @Input() disabled: boolean = false;
  @Input() class: string;

  @Output() clickEvent = new EventEmitter<ModalData>();

  edit() {
    if (this.disabled) return;

    const data: ModalData = {
      type: this.type,
      label: this.label,
      value: this.value
    };

    this.clickEvent.emit(data);
  }
}
