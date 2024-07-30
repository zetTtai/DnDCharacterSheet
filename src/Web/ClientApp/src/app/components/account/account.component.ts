import { Component, Input } from '@angular/core';
import { BaseComponent } from 'src/app/core/components/base.component';
import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';
import { EventService } from 'src/app/core/services/event/event.service';
import { EVENTS } from 'src/app/shared/constants/app-constants';
import { ModalData } from 'src/app/shared/models/modal-data.model';
import { AuthService } from '@auth0/auth0-angular';


@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']
})
export class AccountComponent extends BaseComponent{
  static key = 'account';

  @Input() isOpen: boolean = false;

  constructor(
    private eventService: EventService,
    sharedDataService: SharedDataService,
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
    console.log(this.isAuthenticated());
    this.auth.loginWithPopup();
  }

  logout() {
    this.auth.logout();
  }

  user() {
    console.log(this.sharedDataService.user);
    /**
     * email:"raulbeltmarc@gmail.com"
       email_verified: false
       name: "raulbeltmarc@gmail.com"
       nickname: "raulbeltmarc"
       picture: "https://s.gravatar.com/avatar/87bd228ad4357a79eb04fe3a1023163a?s=480&r=pg&d=https%3A%2F%2Fcdn.auth0.com%2Favatars%2Fra.png"
       sub: "auth0|66a8ec7b7686a649a4defa0f"
       updated_at: "2024-07-30T13:36:59.438Z"
     */
    return this.sharedDataService.user;
  }
}
