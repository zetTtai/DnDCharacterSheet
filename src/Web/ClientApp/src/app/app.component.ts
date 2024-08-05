import { Component, HostListener, OnInit } from '@angular/core';
import { EVENTS, WEB } from 'src/app/shared/constants/app-constants';
import { ModalData } from 'src/app/shared/models/modal-data.model';
import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';
import { LanguageService } from 'src/app/core/services/language/language.service';
import { EventService } from 'src/app/core/services/event/event.service';
import { CommandRegistry } from 'src/app/core/services/command/command-registry.service';
import { OpenModalCommand } from 'src/app/core/services/command/commands/open-modal.command';
import { Auth0Service } from 'src/app/core/services/auth0/auth0.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  isModalVisible: boolean = false;
  data: ModalData;
  isDesktop: boolean = window.innerWidth > WEB.MOBILE_SIZE;
  isLoading: boolean = true;

  constructor(
    private eventService: EventService,
    private sharedDataService: SharedDataService,
    private languageService: LanguageService,
    private commandRegistry: CommandRegistry,
    private auth0Service: Auth0Service
  ) {

    this.registerCommands();
  }

  private registerCommands() {
    this.commandRegistry.registerCommand(EVENTS.OPEN_MODAL, new OpenModalCommand(this));
  }

  async ngOnInit(): Promise<void> {
    this.eventService.event$.subscribe((event) => {
      this.commandRegistry.executeCommand(event.name, event.data);
    });

    this.languageService.setLanguage();

    const isLogged = await this.auth0Service.isLogged();
    if (!isLogged) {
      this.isLoading = false;
      return;
    }

    console.log(`User id: ${this.sharedDataService.userId}`);
    console.log("Start getting user metadata by id (TODO)");
    console.log("Finish");
    this.isLoading = false;
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
