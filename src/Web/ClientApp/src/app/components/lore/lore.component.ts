import { Component } from '@angular/core';
import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';
import { BaseComponent } from 'src/app/core/components/base.component';

@Component({
  selector: 'app-lore',
  templateUrl: './lore.component.html',
  styleUrls: ['./lore.component.scss']
})
export class LoreComponent extends BaseComponent{
  static key = 'lore';

  constructor(sharedDataService: SharedDataService) {
    super(sharedDataService);
  }
}
