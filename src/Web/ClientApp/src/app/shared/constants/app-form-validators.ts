import { Validators } from "@angular/forms";
import { CustomValidatorFn } from "../models/custom-validator.model";

export const MOBILE_HEADER_FIELDS: { [fieldId: string]: CustomValidatorFn[] } = {
  character_name: [
    { messageId: 'validation.required', validator: Validators.required },
    { messageId: 'validation.maxLength.100', validator: Validators.maxLength(100) }
  ]
};
