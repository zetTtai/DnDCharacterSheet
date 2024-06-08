## Purpose
It's the mobile modal structure that will be used in all the application.
It generates its content dynamically and all the transitions effects are declared here.

### Functions

- `loadComponent`: It uses ViewContainerRef in order to create dynamically a component declared in `modalTypes` variable.
All components declared in modalTypes must inherit from `input-modal.component.ts`
Component will be generate on `<ng-template>` (#content)

### Children

- [input-modal](input-modal/README.md)

### Related with
- [shared/models/modal-data](../models/README.md)
