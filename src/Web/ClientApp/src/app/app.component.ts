import { Component, HostListener, OnInit } from '@angular/core';
import { WEB } from 'src/app/shared/constants/app-constants';
import { ModalData } from 'src/app/shared/models/modal-data.model';
import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';
import { LanguageService } from './core/services/language/language.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  isModalVisible: boolean = false;
  data: ModalData;
  isDesktop: boolean = window.innerWidth > WEB.MOBILE_SIZE;

  constructor(private sharedDataService: SharedDataService, private languageService: LanguageService) { }
  ngOnInit(): void {
    this.languageService.setLanguage();
  }

  openModal(data: ModalData) {
    this.isModalVisible = true;
    this.data = data;
  }

  closeModal() {
    this.isModalVisible = false;
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.isDesktop = window.innerWidth > WEB.MOBILE_SIZE;
    this.sharedDataService.isDesktop = this.isDesktop;
  }
}
