import { Injectable } from '@angular/core';
import { CustomValidatorFn } from '../../../shared/models/custom-validator.model';
import { ValidatorFn, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class ValidationService {

  constructor() { }

  getValidators(customValidators: CustomValidatorFn[]): ValidatorFn[] {
    return customValidators.map(customValidator => customValidator.validator);
  }

  getValidatorErrorName(validator: ValidatorFn): string {
    if (validator === Validators.required) return 'required';
    if (validator.toString().includes('minlength')) return 'minlength';
    if (validator.toString().includes('maxlength')) return 'maxlength';

    console.error("Validator not registered");
    return 'unknown';
  }
}
