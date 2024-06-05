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
export class InputTextModalComponent implements InputModal, OnInit{
  @Input() data: ModalData;
  @Output() cancel = new EventEmitter<void>();
  inputTextForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private validationService: ValidationService) { }


  ngOnInit() {
    
    if (this.data) {
      const validators: ValidatorFn[] = this.validationService.getValidators(this.data.validators);
      this.inputTextForm = this.formBuilder.group({
        [this.data.id]: [this.data.value, validators || []]
      });
    }

    const modal = document.getElementById("mobile-modal");
    const input = modal.querySelector('input');
    input.focus();
    modal.classList.add("modal-top");
  }

  close() {
    this.cancel.emit();
    const modal = document.getElementById("mobile-modal");
    modal.classList.remove("modal-top");
  }

  onCancel(event: Event) {
    event.preventDefault();
    this.close();
  }

  onSubmit() {
    if (!this.inputTextForm.valid) {
      console.error('Form not valid');
      return;
    }
    const input = document.getElementById(this.data.id) as HTMLInputElement;
    input.value = this.inputTextForm.value[this.data.id];
    this.close();
  }
}
