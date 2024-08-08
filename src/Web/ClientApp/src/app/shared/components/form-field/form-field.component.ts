import { Component, Input } from '@angular/core';
import { ModalData } from 'src/app/shared/models/modal-data.model';
import { MOBILE_HEADER_FIELDS } from 'src/app/shared/constants/app-form-validators';
import { EventService } from 'src/app/core/services/event/event.service';
import { EVENTS } from 'src/app/shared/constants/app-constants';

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
  @Input() readOnly: boolean = false;
  @Input() class: string;
  @Input() last: boolean = false;
  @Input() desktop: boolean = false;

  constructor(private eventService: EventService) { }

  edit(event: Event) {
    if (this.readOnly) return;

    const id = this.getTargetId(event);
    if (!id) return;

    const value = this.getTargetValue(event);

    const data: ModalData = {
      id: id,
      type: this.type,
      label: this.label,
      value: value,
      validators: MOBILE_HEADER_FIELDS[id] || []
    };

    this.eventService.emit({
      name: EVENTS.OPEN_MODAL,
      data: data
    });
  }

  getTargetId(event: Event): string {
    const target = event.target as HTMLElement;
    target.blur();
    const input = target.querySelector('input');
    if (input) {
      return input.id;
    }

    return target.id;
  }

  getTargetValue(event: Event): any {
    const target = event.target as HTMLElement;

    if (target instanceof HTMLInputElement) {
      return target.value;
    }

    if (target.classList.contains('input-group')) {
      const input = target.querySelector('input') as HTMLInputElement | null;
      return input?.value;
    }

    return undefined;
  }
}
