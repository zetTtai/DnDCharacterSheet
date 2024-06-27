import { Component, OnInit } from '@angular/core';
import { ToggleService } from '../../core/services/toggle/toggle.service';
import { DelayService } from '../../core/services/delay/delay.service';
import { ICONS } from '../../shared/constants/app-constants';

@Component({
  selector: 'app-fixed-toggle-buttons',
  templateUrl: './fixed-toggle-buttons.component.html',
  styleUrls: ['./fixed-toggle-buttons.component.scss']
})
export class FixedToggleButtonsComponent implements OnInit{

  defaultIconSize: string = ICONS.FIXED_TOGGLE_BUTTONS_DEFAULT_SIZE;

  toggleStates = {
    abilities: false,
    deathSaves: false,
    wallet: false,
    spellcasting: false
  };

  sections = {
    'abilities': {
      'direction': 'right',
      'onLeft': true,
      'distance': () => this.getElementWidth('abilities')
    },
    'death-saves': {
      'direction': 'right',
      'onLeft': true,
      'distance': () => this.getElementWidth('death-saves')
    },
    'wallet': {
      'direction': 'left',
      'onLeft': false,
      'distance': () => this.getElementWidth('wallet')
    },
    'spellcasting': {
      'direction': 'left',
      'onLeft': false,
      'distance': () => this.getElementWidth('spellcasting')
    }
  };

  constructor(private toggleService: ToggleService, private delayService: DelayService) { }

  ngOnInit() {
    const width = this.calculateDistanceForBottomFixedButtons();
    document.getElementById(this.getElementId('spellcasting')).style.maxWidth = `${width}px`;
    document.getElementById(this.getElementId('death-saves')).style.maxWidth = `${width}px`;

    this.setInitialPositions();
  }

  private setInitialPositions() {
    Object.keys(this.sections).forEach(section => {
      const elementId = this.getElementId(section);
      const element = document.getElementById(elementId) as HTMLElement;
      if (element) {
        const onLeftSide = this.sections[section].onLeft;
        const offset = this.sections[section].distance();
        const positionStyle = onLeftSide ? 'left' : 'right';
        element.style[positionStyle] = `-${offset}px`;
      }
    });
  }

  private getElementWidth(id: string) {
    const element = document.getElementById(this.getElementId(id));
    return element ? element.offsetWidth : 0;
  }

  private calculateDistanceForBottomFixedButtons(): number {
    const body = document.body;
    const html = document.documentElement;

    // Get the maximum of the document's width
    const width = Math.max(
      body.scrollWidth,
      body.offsetWidth,
      html.clientWidth,
      html.scrollWidth,
      html.offsetWidth
    );
    return width - 100;
  }

  private getElementId(key: string): string {
    return `mobile-${key.toLowerCase()}-content`;
  }

  private toggleSection(key: string): void {
    const elementId = this.getElementId(key);
    const toggleId = `toggle-${key}`;
    const isOpen = this.toggleStates[key];
    let delay = 0;

    if (!isOpen) {
      this.toggleService.expand(elementId, toggleId, this.sections[key].distance(), this.sections[key].direction);
    } else {
      this.toggleService.collapse(elementId, toggleId, this.sections[key].distance(), this.sections[key].direction);
      delay = this.delayService.getDelayInSeconds(elementId);
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
