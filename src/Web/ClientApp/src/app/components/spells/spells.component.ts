import { Component } from '@angular/core';
import { SharedDataService } from '../../core/services/shared-data/shared-data.service';

@Component({
  selector: 'app-spells',
  templateUrl: './spells.component.html',
  styleUrls: ['./spells.component.scss']
})
export class SpellsComponent {
  static key = 'spells';

  constructor(private sharedDataService: SharedDataService) { }

  isDesktop(): boolean {
    return this.sharedDataService.isDesktop;
  }
}
