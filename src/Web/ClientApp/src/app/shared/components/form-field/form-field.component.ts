import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ModalData } from '../../models/modal-data.model';

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

  @Output() clickEvent = new EventEmitter<ModalData>();

  edit(event: Event) {
    if (this.disabled) return;

    const id = this.getTargetId(event);

    if (!id) return;

    const data: ModalData = {
      id: id,
      type: this.type,
      label: this.label,
      value: this.value
    };

    this.clickEvent.emit(data);
  }

  getTargetId(event: Event): string {
    const target = event.target as HTMLElement;

    if (!target.classList.contains('input-group')) {
      return target.id;
    }

    return target.querySelector('input').name;
  }
}
