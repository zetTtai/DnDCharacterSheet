import { Component } from '@angular/core';
import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';
import { BaseComponent } from 'src/app/core/components/base.component';

@Component({
  selector: 'app-main-view',
  templateUrl: './main-view.component.html',
  styleUrl: './main-view.component.scss'
})
export class MainViewComponent extends BaseComponent {
  static key = 'home';
  constructor(sharedDataService: SharedDataService) {
    super(sharedDataService);
  }
}
