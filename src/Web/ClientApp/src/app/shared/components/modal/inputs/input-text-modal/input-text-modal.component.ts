import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalData } from '../../../../models/modal-data.model';
import { InputModal } from '../../modal.component';
import { FormBuilder, FormGroup, ValidatorFn } from '@angular/forms';
import { ValidationService } from '../../../../../core/services/validation/validation.service';

@Component({
  selector: 'app-input-text-modal',
  templateUrl: './input-text-modal.component.html',
  styleUrls: ['./input-text-modal.component.scss']
})
export class InputTextModalComponent implements InputModal, OnInit {
  @Input() data: ModalData;
  @Output() cancel = new EventEmitter<void>();
  inputTextForm: FormGroup;

  constructor(private fb: FormBuilder, private validationService: ValidationService) { }

  ngOnInit() {
    if (this.data) {
      const validators: ValidatorFn[] = this.validationService.getValidators(this.data.validators);
      this.inputTextForm = this.fb.group({
        [this.data.id]: ['', validators || []]
      });
    }
  }

  onCancel(event: Event) {
    event.preventDefault();
    this.cancel.emit();
  }

  onSubmit() {
    if (!this.inputTextForm.valid) {
      console.log('Form not valid');
      return;
    }
    console.log('Form Submitted', this.inputTextForm.value);
  }
}
