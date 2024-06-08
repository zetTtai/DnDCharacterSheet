## Purpose
Where all the constants of the frontend are declared in order to avoid hardcoded variables.

### Functions

- `app-constants`: Constants in general, most of them are here.
- `app-form-validators`: Specific constants related to inputs that have the following structure:
```
  input_id : [
    { messageId: 'string', validator: Validators.required },
    { messageId: 'string', validator: Validators.maxLength(100) }
  ]
```
If you want to change the validations of an input then change them in `app-form-validators`

### Related with
- [shared/components/modal.component](../components/modal/README.md)
- [shared/components/form-field](../components/form-field/README.md)
- [shared/constants/app-form-validators](../constants/README.md)
- All modal inputs (Inside of shared/components/modal/inputs)
