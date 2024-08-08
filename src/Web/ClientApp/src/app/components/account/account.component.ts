import { Component, Input } from '@angular/core';
import { BaseComponent } from 'src/app/core/components/base.component';
import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';
import { EventService } from 'src/app/core/services/event/event.service';
import { EVENTS } from 'src/app/shared/constants/app-constants';
import { ModalData } from 'src/app/shared/models/modal-data.model';
import { Auth0Service } from 'src/app/core/services/auth0/auth0.service';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']
})
export class AccountComponent extends BaseComponent {
  static key = 'account';

  @Input() isOpen: boolean = false;

  user: any = null;

  constructor(
    private eventService: EventService,
    sharedDataService: SharedDataService,
    public auth0Service: Auth0Service,
    public auth: AuthService
  ) {
    super(sharedDataService);
  }

  switchLanguages() {
    const data: ModalData = {
      id: 'langs',
      type: 'languages'
    }

    this.eventService.emit({
      name: EVENTS.OPEN_MODAL,
      data: data
    });

  }

  login() {
    this.auth0Service.login();
  }

  logout() {
    this.auth0Service.logout();
  }
}
