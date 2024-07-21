import { Component, HostListener, OnInit } from '@angular/core';
import { EVENTS, WEB } from 'src/app/shared/constants/app-constants';
import { ModalData } from 'src/app/shared/models/modal-data.model';
import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';
import { LanguageService } from 'src/app/core/services/language/language.service';
import { EventService } from 'src/app/core/services/event/event.service';
import { CommandRegistry } from 'src/app/core/services/command/command-registry.service';
import { OpenModalCommand } from 'src/app/core/services/command/commands/open-modal.command';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  isModalVisible: boolean = false;
  data: ModalData;
  isDesktop: boolean = window.innerWidth > WEB.MOBILE_SIZE;

  constructor(
    private eventService: EventService,
    private sharedDataService: SharedDataService,
    private languageService: LanguageService,
    private commandRegistry: CommandRegistry) {

    this.registerCommands();
  }

  private registerCommands() {
    this.commandRegistry.registerCommand(EVENTS.OPEN_MODAL, new OpenModalCommand(this));
  }

  ngOnInit(): void {
    this.languageService.setLanguage();
    this.eventService.event$.subscribe((event) => {
      this.commandRegistry.executeCommand(event.name, event.data);
    })
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
