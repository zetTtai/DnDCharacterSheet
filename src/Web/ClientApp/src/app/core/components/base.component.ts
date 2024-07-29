import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';

export class BaseComponent {
  constructor(protected sharedDataService: SharedDataService) { }

  isDesktop(): boolean {
    return this.sharedDataService.isDesktop;
  }

  public checkRange(event: Event, min: number, max: number): number {
    const input = event.target as HTMLInputElement;
    let value = parseInt(input.value, 10);

    if (!isNaN(value)) {
      if (value < min) value = min;
      if (value > max) value = max;
    } else {
      value = -1;
    }

 

    input.value = value.toString();

    return value;
  }
}
