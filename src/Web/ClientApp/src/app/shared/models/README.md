## Purpose
In this folder is where we are going to declare all the interfaces that are used globally in the application with the `.model` extension.

### Functions

- `custom-validator`: Used with `Anguar Reactive Form`, its a key value structure where key is a `messageId` and value is a `ValidatorFn`.
- `modal-data`: Data structure send to modal from a form-field in order to create the input inside of modal with all the necessary data.

### Related with
- [shared/components/modal.component](../components/modal/README.md)
- [shared/components/form-field](../components/form-field/README.md)
- [shared/components/validation-messages]((../components/validation-messages/README.md))
- [shared/constants/app-form-validators](../constants/README.md)
- All modal inputs (Inside of shared/components/modal/inputs)
