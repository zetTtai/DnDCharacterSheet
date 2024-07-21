import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';

export class BaseComponent {
  constructor(protected sharedDataService: SharedDataService) { }

  isDesktop(): boolean {
    return this.sharedDataService.isDesktop;
  }
}
