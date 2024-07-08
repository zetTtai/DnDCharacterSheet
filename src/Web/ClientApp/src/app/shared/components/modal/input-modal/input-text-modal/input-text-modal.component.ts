import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ValidationService } from 'src/app/core/services/validation/validation.service';
import { InputModalComponent } from '../input-modal.component';

@Component({
  selector: 'app-input-text-modal',
  templateUrl: './input-text-modal.component.html',
  styleUrls: ['./input-text-modal.component.scss']
})
export class InputTextModalComponent extends InputModalComponent{

  constructor(formBuilder: FormBuilder, validationService: ValidationService) {
    super(formBuilder, validationService);
  }
}
