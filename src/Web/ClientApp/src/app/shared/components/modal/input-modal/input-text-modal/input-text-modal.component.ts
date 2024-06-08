import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ValidationService } from '../../../../../core/services/validation/validation.service';
import { InputModalComponent } from '../input-modal.component';

@Component({
  selector: 'app-input-text-modal',
  templateUrl: './input-text-modal.component.html',
  styleUrls: ['./input-text-modal.component.scss']
})
export class InputTextModalComponent extends InputModalComponent{

  inputForm: FormGroup;
  constructor(formBuilder: FormBuilder, validationService: ValidationService) {
    super(formBuilder, validationService);
    this.inputForm = super.inputForm;
  }
}
