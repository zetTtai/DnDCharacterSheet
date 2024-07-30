import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalData } from 'src/app/shared/models/modal-data.model';
import { FormBuilder, FormGroup, ValidatorFn } from '@angular/forms';
import { ValidationService } from 'src/app/core/services/validation/validation.service';

@Component({
  template: ''
})
export class InputModalComponent implements OnInit {

  @Input() data: ModalData;
  @Output() cancel = new EventEmitter<void>();
  inputForm: FormGroup;

  constructor(protected formBuilder: FormBuilder, protected validationService: ValidationService) { }

  ngOnInit() {
    if (this.data) {
      const validators: ValidatorFn[] = this.validationService.getValidators(this.data.validators);
      this.inputForm = this.formBuilder.group({
        [this.data.id]: [this.data.value, validators || []]
      });
    }

    const modal = document.getElementById("mobile-modal");
    const input = modal.querySelector('input');
    input.focus();
  }

  close() {
    this.cancel.emit();
  }

  onCancel(event: Event) {
    event.preventDefault();
    this.close();
  }

  onSubmit() {
    if (!this.inputForm.valid) {
      console.error('Form not valid');
      return;
    }
    const input = document.getElementById(this.data.id) as HTMLInputElement;
    input.value = this.inputForm.value[this.data.id];
    this.close();
  }

  onKeydown(event: KeyboardEvent) {
    if (event.key === 'Enter') {
      event.preventDefault();
      this.onSubmit();
    }
  }
}
