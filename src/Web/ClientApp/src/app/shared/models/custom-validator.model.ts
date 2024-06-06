import { ValidatorFn } from "@angular/forms";

export interface CustomValidatorFn {
  messageId: string;
  validator: ValidatorFn;
}
