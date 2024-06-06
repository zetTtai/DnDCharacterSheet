import { CustomValidatorFn } from "./custom-validator.model";

export interface ModalData {
  id: string;
  type: string;
  label: string;
  value: any;
  validators: CustomValidatorFn[]
}
