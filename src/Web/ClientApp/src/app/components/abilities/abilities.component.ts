import { Component } from '@angular/core';
import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';
import { BaseComponent } from 'src/app/core/components/base.component';

@Component({
  selector: 'app-abilities',
  templateUrl: './abilities.component.html',
  styleUrls: ['./abilities.component.scss']
})
export class AbilitiesComponent extends BaseComponent {

  // TODO: Get by Database
  public abilities: {id: string, score: number}[] = [
    {
      id: "str",
      score: -1,
    },
    {
      id: "dex",
      score: -1,
    },
    {
      id: "con",
      score: -1,
    },
    {
      id: "int",
      score: -1,
    },
    {
      id: "wis",
      score: -1,
    },
    {
      id: "cha",
      score: -1,
    }
  ];

  constructor(sharedDataService: SharedDataService) {
    super(sharedDataService);
  }

  getModifier(score: number) : number {
    return (score - 10) - 2;
  }
}
