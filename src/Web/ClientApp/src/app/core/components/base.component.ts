import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';
import { ABILITIES } from 'src/app/shared/constants/app-constants';

export class BaseComponent {
  constructor(protected sharedDataService: SharedDataService) { }

  isDesktop(): boolean {
    return this.sharedDataService.isDesktop;
  }

  isAuthenticated(): boolean {
    return this.sharedDataService.user != null;
  }

  public checkRange(event: Event, min: number, max: number): number {
    const input = event.target as HTMLInputElement;
    let value = parseInt(input.value, 10);

    if (!isNaN(value)) {
      if (value < min) value = min;
      if (value > max) value = max;
    } else {
      value = ABILITIES.DEFAULT_VALUE;
    }
 
    input.value = value.toString();

    return value;
  }
}
