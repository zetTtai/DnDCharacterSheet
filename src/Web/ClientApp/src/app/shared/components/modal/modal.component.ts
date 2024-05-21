import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { ModalData } from '../../models/modal-data.model';
import { HomeComponent } from '../../../components/home/home.component';
import { SpellsComponent } from '../../../components/spells/spells.component';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent implements OnChanges {
  @Input() isVisible: boolean = false;
  @Input() data: ModalData;
  @Output() closeModal = new EventEmitter<void>();

  component: any;

  private componentMap = {
    'race': HomeComponent,
    'character-name': SpellsComponent
  };

  ngOnChanges(changes: SimpleChanges) {
    if (changes.data && this.data) {
      this.component = this.componentMap[this.data.id];
    }
  }

  onClose() {
    this.closeModal.emit();
  }
}
