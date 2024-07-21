import { Component } from '@angular/core';
import { BaseComponent } from 'src/app/core/components/base.component';
import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';
import { EventService } from 'src/app/core/services/event/event.service';
import { EVENTS } from 'src/app/shared/constants/app-constants';
import { ModalData } from 'src/app/shared/models/modal-data.model';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']
})
export class AccountComponent extends BaseComponent{
  static key = 'account';

  constructor(
    private eventService: EventService,
    sharedDataService: SharedDataService) {
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
}
