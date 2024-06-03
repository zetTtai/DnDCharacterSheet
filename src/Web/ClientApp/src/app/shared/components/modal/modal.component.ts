import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges, ViewChild, ViewContainerRef } from '@angular/core';
import { ModalData } from '../../models/modal-data.model';
import { InputTextModalComponent } from './inputs/input-text-modal/input-text-modal.component';
import { DelayService } from '../../../core/services/delay/delay.service';

export interface InputModal {
  data: ModalData;
}

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})

export class ModalComponent implements OnChanges {
  @Input() isVisible: boolean = false;
  @Input() data: ModalData;
  @Output() closeModal = new EventEmitter<void>();
  @ViewChild('content', { read: ViewContainerRef }) content: ViewContainerRef;
  private modalTypes = {
    'text': InputTextModalComponent,
  };

  constructor(private delayService: DelayService) { }

  async ngOnChanges(changes: SimpleChanges) {
    if (changes.data && this.data) {
      let key = this.data.type === 'custom'
        ? this.data.id
        : this.data.type;
      if (!this.modalTypes[key]) {
        console.error(`Unhandled type of modal (${key})`)
        return;
      }
      await this.loadComponent(this.modalTypes[key]);
    }
  }

  async loadComponent(component: any) {
    this.content.clear();
    const componentRef = this.content.createComponent(component);
    const instance = componentRef.instance as InputModal;
    instance.data = this.data;
  }

  onClose(event: Event) {
    const target = event.target as HTMLElement;
    if (target.id != "mobile-modal" && target.id != "close") return;

    this.closeModal.emit();
    const delay = this.delayService.getDelayInSeconds('mobile-modal');
    setTimeout(() => {
      this.content.clear();
    }, delay);
  }
}
