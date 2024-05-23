import { Component } from '@angular/core';
import { Directions, ToggleService } from '../../core/services/toggle/toggle.service';

@Component({
  selector: 'app-fixed-toggle-buttons',
  templateUrl: './fixed-toggle-buttons.component.html',
  styleUrls: ['./fixed-toggle-buttons.component.scss']
})
export class FixedToggleButtonsComponent {

  toggleStates = {
    abilities: false,
    deathSaves: false,
    wallet: false,
    spellcasting: false
  };

  sections = {
    'abilities': {
      'direction': 'right',
      'distance': 100
    },
    'death-saves': {
      'direction': 'right',
      'distance': 300
    },
    'wallet': {
      'direction': 'left',
      'distance': 100
    },
    'spellcasting': {
      'direction': 'left',
      'distance': 300
    },
  };

  constructor(private toggleService: ToggleService) { }

  private getElementId(key: string): string {
    return `mobile-${key.toLowerCase()}-content`;
  }

  private getDelay(id: string): number {
    const element = document.getElementById(id);
    if (!element) return 0;

    return parseFloat(getComputedStyle(element).transitionDuration) * 1000;
  }

  private toggleSection(key: string): void {
    const elementId = this.getElementId(key);
    const toggleId = `toggle-${key}`;
    const isOpen = this.toggleStates[key];
    let delay = 0;

    if (!isOpen) {
      this.toggleService.expand(elementId, toggleId, this.sections[key].distance, this.sections[key].direction);
    } else {
      this.toggleService.collapse(elementId, toggleId, this.sections[key].distance, this.sections[key].direction);
      delay = this.getDelay(elementId);
    }

    this.toggleStates[key] = !isOpen;
    setTimeout(() => {
      document.getElementById(elementId).classList.toggle('active');
    }, delay);
  }

  private closeSections(keys: string[]) {
    keys.forEach(key => {
      if (this.toggleStates[key]) {
        this.toggleSection(key);
      }
    });
  }

  toggleAbilities(): void {
    this.closeSections(['death-saves', 'spellcasting'])
    this.toggleSection('abilities');
  }

  toggleDeathSaves(): void {
    this.closeSections(['abilities', 'spellcasting', 'wallet'])
    this.toggleSection('death-saves');
  }

  toggleWallet(): void {
    this.closeSections(['death-saves', 'spellcasting'])
    this.toggleSection('wallet');
  }

  toggleSpellcasting(): void {
    this.closeSections(['death-saves', 'abilities'])
    this.toggleSection('spellcasting');
  }

}
