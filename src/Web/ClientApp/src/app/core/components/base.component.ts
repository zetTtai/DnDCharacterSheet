import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';

export class BaseComponent {
  constructor(protected sharedDataService: SharedDataService) { }

  isDesktop(): boolean {
    return this.sharedDataService.isDesktop;
  }

  public checkRange(event: Event, min: number, max: number) {
    const input = event.target as HTMLInputElement;
    let value = parseInt(input.value, 10);

    if (value < min) value = min;
    if (value > max) value = max;

    input.value = value.toString();
  }
}
