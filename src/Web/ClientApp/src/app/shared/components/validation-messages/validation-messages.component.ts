import { Component, Input } from '@angular/core';
import { ModalData } from '../../models/modal-data.model';
import { FormGroup, ValidatorFn } from '@angular/forms';
import { ValidationService } from '../../../core/services/validation/validation.service';

@Component({
  selector: 'app-validation-messages',
  templateUrl: './validation-messages.component.html',
  styleUrls: ['./validation-messages.component.scss']
})
export class ValidationMessagesComponent {
  @Input() data: ModalData;
  @Input() form: FormGroup;

  constructor(private validationService: ValidationService) { }

  getValidatorErrorName(validator: ValidatorFn) {
    return this.validationService.getValidatorErrorName(validator);
  }
}
