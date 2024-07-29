import { Component } from '@angular/core';
import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';
import { BaseComponent } from 'src/app/core/components/base.component';
import { ABILITIES, EVENTS } from 'src/app/shared/constants/app-constants';
import { Ability } from 'src/app/shared/models/ability.model';
import { EventService } from 'src/app/core/services/event/event.service';
import { ModalData } from 'src/app/shared/models/modal-data.model';

@Component({
  selector: 'app-abilities',
  templateUrl: './abilities.component.html',
  styleUrls: ['./abilities.component.scss']
})
export class AbilitiesComponent extends BaseComponent {

  public minScore: number = ABILITIES.SCORE.MIN;
  public maxScore: number = ABILITIES.SCORE.MAX;
  public modifierPrefix: string = ABILITIES.MODIFIER_PREFIX;

  // TODO: Get by Database
  public abilities: Ability[] = [];

  constructor(sharedDataService: SharedDataService, private eventService: EventService) {
    super(sharedDataService);

    ABILITIES.VALUES.forEach(value => {
      this.abilities.push(new Ability(value.toLowerCase()));
    })
  }

  getModifier(score: number): string {
    let modifierValue = Math.floor((score - 10) / 2);
    return (modifierValue > 0 ? '+' : '') + modifierValue.toString();
  }

  onScoreChange(ability: Ability, event: Event): void {
    ability.score = this.checkRange(event, this.minScore, this.maxScore);

    let modifier = document.getElementById(`${this.modifierPrefix}-${ability.id}`) as HTMLElement;

    modifier.innerHTML = ability.score !== -1
      ? this.getModifier(ability.score)
      : '';
  }

  showAbilityInfo(abilityId: string) {

    const data: ModalData = {
      id: abilityId,
      type: 'ability-info'
    };

    this.eventService.emit({
      name: EVENTS.OPEN_MODAL,
      data: data
    });
  }
}
