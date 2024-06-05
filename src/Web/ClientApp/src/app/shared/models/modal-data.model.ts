import { CustomValidatorFn } from "./custom-validator";

export interface ModalData {
  id: string;
  type: string;
  label: string;
  value: any;
  validators: CustomValidatorFn[]
}
