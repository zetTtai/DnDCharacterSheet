import { Validators } from "@angular/forms";
import { CustomValidatorFn } from "../models/custom-validator";

export const MOBILE_HEADER_FIELDS: { [fieldId: string]: CustomValidatorFn[] } = {
  character_name: [
    { messageId: 'validation.required', validator: Validators.required },
    { messageId: 'validation.minLength.5', validator: Validators.minLength(5) }
  ],
  id2: [{ messageId: 'validation.email', validator: Validators.email }],
  id3: [{ messageId: 'validation.requiredTrue', validator: Validators.requiredTrue }]
};
