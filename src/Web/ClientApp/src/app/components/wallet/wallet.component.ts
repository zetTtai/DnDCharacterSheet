import { Component } from '@angular/core';
import { CURRENCY } from 'src/app/shared/constants/app-constants';
import { BaseComponent } from 'src/app/core/components/base.component';
import { SharedDataService } from '../../core/services/shared-data/shared-data.service';

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
  // This array must be ordered by its quality (like order)
  public currencies: { id: string, value: string }[] = [
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
    {
      id: "silver",
      value: "",
    },
    {
      id: "copper",
      value: "",
    },
  ];

  constructor(sharedDataService: SharedDataService) {
    super(sharedDataService);
  }

  public toggleConvertOptions() {
    this.showConvertOptions = !this.showConvertOptions;
  }

  public convert(currencyName: string, isAllowed: boolean, isUpgrading: boolean = false) {

    if (!isAllowed) return;

    let index = this.currencies.findIndex(currency => currency.id == currencyName);

    if (index === -1) {
      console.error(`currency with name ${currencyName} not found`);
      return;
    }

    let currenyTarget = this.currencies[isUpgrading ? --index : ++index];

    if (isUpgrading) {
      console.log(`upgrading ${currencyName} to ${currenyTarget.id}`)
      return;
    }

    console.log(`downgrading ${currencyName} to ${currenyTarget.id}`)

  }
}
