import { Component } from '@angular/core';
import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';
import { BaseComponent } from 'src/app/core/components/base.component';

@Component({
  selector: 'app-abilities',
  templateUrl: './abilities.component.html',
  styleUrls: ['./abilities.component.scss']
})
export class AbilitiesComponent extends BaseComponent {
  constructor(sharedDataService: SharedDataService) {
    super(sharedDataService);
  }
}
