import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges, ViewChild, ViewContainerRef } from '@angular/core';
import { ModalData } from 'src/app/shared/models/modal-data.model';
import { InputTextModalComponent } from './input-modal/input-text-modal/input-text-modal.component';
import { DelayService } from 'src/app/core/services/delay/delay.service';
import { LanguagesModalComponent } from './languages-modal/languages-modal.component';
import { AbilityInfoModalComponent } from './ability-info-modal/ability-info-modal.component';

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

  public isLoaded: boolean = false;
  public top: boolean = true;

  @ViewChild('content', { read: ViewContainerRef }) content: ViewContainerRef;

  private modalTypes = {
    'text': InputTextModalComponent,
    'languages': LanguagesModalComponent,
    'ability-info': AbilityInfoModalComponent
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
      const delay = this.delayService.getDelayInSeconds('mobile-modal');
      setTimeout(() => {
        this.isLoaded = true;
      }, delay + 100);
    }
  }

  async loadComponent(component: any) {
    this.content.clear();
    const componentRef = this.content.createComponent(component);
    const instance = componentRef.instance as InputModal;
    instance.data = this.data;

    if ('cancel' in instance) {
      (componentRef.instance as any).cancel.subscribe(() => this.onCancel());
    }

  }

  private closingModal() {
    this.closeModal.emit();
    const delay = this.delayService.getDelayInSeconds('mobile-modal');
    setTimeout(() => {
      this.content.clear();
      this.isLoaded = false;
    }, delay);
  }

  onClose(event: Event) {
    const target = event.target as HTMLElement;
    if (target.id != "mobile-modal") return;

    this.closingModal();
  }

  onCancel() {
    this.closingModal();
  }
}
