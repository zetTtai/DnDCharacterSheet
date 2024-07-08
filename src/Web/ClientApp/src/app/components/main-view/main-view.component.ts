import { Component } from '@angular/core';
import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';

@Component({
  selector: 'app-main-view',
  templateUrl: './main-view.component.html',
  styleUrl: './main-view.component.scss'
})
export class MainViewComponent {
  static key = 'home';
  constructor(private sharedDataService: SharedDataService) { }

  isDesktop(): boolean {
    return this.sharedDataService.isDesktop;
  }
}
