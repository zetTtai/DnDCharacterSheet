import { Component } from '@angular/core';
import { CURRENCY, EVENTS } from 'src/app/shared/constants/app-constants';
import { BaseComponent } from 'src/app/core/components/base.component';
import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';
import { EventService } from 'src/app/core/services/event/event.service';
import { ModalData } from 'src/app/shared/models/modal-data.model';

@Component({
  selector: 'app-wallet',
  templateUrl: './wallet.component.html',
  styleUrls: ['./wallet.component.scss']
})
export class WalletComponent extends BaseComponent {
  public showConvertOptions: boolean = false;
  public minCurrency: number = CURRENCY.MIN;
  public maxCurrency: number = CURRENCY.MAX;

  // TODO: Get by Database
  // This array must be ordered by its quality (order in Enum?)
  public currencies: { id: string, value: string }[] = [
    {
      id: "copper",
      value: "",
    },
    {
      id: "silver",
      value: "",
    },
    {
      id: "electrum",
      value: "",
    },
    {
      id: "gold",
      value: "",
    },
    {
      id: "platinum",
      value: "",
    },
  ];

  constructor(sharedDataService: SharedDataService, private eventService: EventService) {
    super(sharedDataService);
  }

  public toggleConvertOptions() {
    this.showConvertOptions = !this.showConvertOptions;
  }

  public getCurrentValueById(id: string): number {
    let content = document.getElementById(id) as HTMLInputElement;
    return Number(content.value);
  }

  public convert(currencyName: string, isAllowed: boolean, isUpgrading: boolean = false) {

    if (!isAllowed) return;

    let index = this.currencies.findIndex(currency => currency.id == currencyName);

    if (index === -1) {
      console.error(`currency with name ${currencyName} not found`);
      return;
    }

    let currencyTarget = this.currencies[isUpgrading ? index - 1 : index + 1];

    if (!this.isDesktop()) {
      const data: ModalData = {
        id: currencyName,
        type: 'currency',
        value: {
          isUpgrading,
          currentCurrency: this.currencies[index],
          currencyTarget
        }
      };
      this.eventService.emit({
        name: EVENTS.OPEN_MODAL,
        data: data
      });
      return;
    }

    if (isUpgrading) {
      console.log(`upgrading ${currencyName} to ${currencyTarget.id}`)
      return;
    }

    console.log(`downgrading ${currencyName} to ${currencyTarget.id}`)

  }
}
