import { Component } from '@angular/core';
import { SharedDataService } from '../../core/services/shared-data/shared-data.service';

@Component({
  selector: 'app-lore',
  templateUrl: './lore.component.html',
  styleUrls: ['./lore.component.scss']
})
export class LoreComponent {
  static key = 'lore';

  constructor(private sharedDataService: SharedDataService) { }

  isDesktop(): boolean {
    return this.sharedDataService.isDesktop;
  }
}
