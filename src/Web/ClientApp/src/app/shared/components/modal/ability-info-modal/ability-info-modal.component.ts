import { Component, Input } from '@angular/core';
import { ModalData } from 'src/app/shared/models/modal-data.model';

@Component({
  selector: 'app-ability-info-modal',
  templateUrl: './ability-info-modal.component.html',
  styleUrl: './ability-info-modal.component.scss'
})
export class AbilityInfoModalComponent {
  @Input() data: ModalData;
}
