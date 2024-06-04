import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ModalData } from '../../models/modal-data.model';
import { MOBILE_HEADER_FIELDS } from '../../constants/app-form-validators';

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
  @Input() last: boolean = false;

  @Output() editFormField = new EventEmitter<ModalData>();

  edit(event: Event) {
    if (this.disabled) return;

    const id = this.getTargetId(event);

    if (!id) return;

    const data: ModalData = {
      id: id,
      type: this.type,
      label: this.label,
      value: this.value,
      validators: MOBILE_HEADER_FIELDS[id] || []
    };

    this.editFormField.emit(data);
  }

  getTargetId(event: Event): string {
    const target = event.target as HTMLElement;

    if (!target.classList.contains('input-group')) {
      return target.id;
    }

    return target.querySelector('input').name;
  }
}
