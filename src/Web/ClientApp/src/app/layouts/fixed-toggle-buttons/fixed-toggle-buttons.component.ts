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

  constructor(private toggleService: ToggleService) { }

  private getElementId(key: string): string {
    return `mobile-${key.toLowerCase()}-content`;
  }

  private getDelay(id: string): number {
    const computedStyle = getComputedStyle(document.getElementById(id));
    return parseFloat(computedStyle.transitionDuration) * 1000;
  }

  private toggleSection(key: string, direction: Directions): void {
    const elementId = this.getElementId(key);
    const toggleId = `toggle-${key}`;
    const isOpen = this.toggleStates[key];
    let delay = 0;

    if (!isOpen) {
      this.toggleService.expand(elementId, toggleId, 100, direction);
    } else {
      this.toggleService.collapse(elementId, toggleId, 100, direction);
      delay = this.getDelay(elementId);
    }

    this.toggleStates[key] = !isOpen;
    setTimeout(() => {
      document.getElementById(elementId).classList.toggle('active');
    }, delay);
  }

  toggleAbilities(): void {
    this.toggleSection('abilities', 'right');
  }

  toggleDeathSaves(): void {
    this.toggleSection('death-saves', 'right');
  }

  toggleWallet(): void {
    this.toggleSection('wallet', 'left');
  }

  toggleSpellcasting(): void {
    this.toggleSection('spellcasting', 'left');
  }

}
