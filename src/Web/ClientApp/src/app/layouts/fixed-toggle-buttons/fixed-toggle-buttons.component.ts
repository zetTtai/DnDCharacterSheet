import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Directions, ToggleService } from 'src/app/core/services/toggle/toggle.service';
import { DelayService } from 'src/app/core/services/delay/delay.service';
import { ICONS } from 'src/app/shared/constants/app-constants';

@Component({
  selector: 'app-fixed-toggle-buttons',
  templateUrl: './fixed-toggle-buttons.component.html',
  styleUrls: ['./fixed-toggle-buttons.component.scss']
})
export class FixedToggleButtonsComponent implements OnInit, AfterViewInit{

  defaultIconSize: string = ICONS.FIXED_TOGGLE_BUTTONS_DEFAULT_SIZE;

  toggleStates = {
    abilities: false,
    deathSaves: false,
    wallet: false,
    spellcasting: false
  };

  sections = [
    {
      id: 'abilities',
      direction: 'right',
      onLeft: true,
      distance: 0
    },
    {
      id: 'death-saves',
      direction: 'right',
      onLeft: true,
      distance: 0
    },
    {
      id: 'wallet',
      direction: 'left',
      onLeft: false,
      distance: 0
    },
    {
      id: 'spellcasting',
      direction: 'left',
      onLeft: false,
      distance: 0
    }
  ];

  constructor(private toggleService: ToggleService, private delayService: DelayService) { }

  ngAfterViewInit(): void {
    this.setDistances();
    this.setInitialPositions();
  }

  ngOnInit() {
    const maxWidth = this.calculateMaxDistanceForBottomFixedButtons();
    document.getElementById(this.getElementId('spellcasting')).style.maxWidth = `${maxWidth}px`;
    document.getElementById(this.getElementId('death-saves')).style.maxWidth = `${maxWidth}px`;
  }

  private setDistances() {
    this.sections.forEach(section => {
      section.distance = this.getElementWidth(section.id)
    });
  }

  private setInitialPositions() {
    this.sections.forEach(section => {
      const id = this.getElementId(section.id);
      const element = document.getElementById(id) as HTMLElement;

      if (!element) {
        console.error(`Element with id ${id} not found`);
        return;
      }

      const onLeftSide = section.onLeft;
      const offset = section.distance;
      const positionStyle = onLeftSide ? 'left' : 'right';
      element.style[positionStyle] = `-${offset}px`;
    });
  }

  private getElementWidth(id: string) {
    const element = document.getElementById(this.getElementId(id));
    return element ? element.offsetWidth : 0;
  }

  private calculateMaxDistanceForBottomFixedButtons(): number {
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

    const section = this.sections.find(section => section.id === key);
    if (!section) {
      console.error(`Section with key ${key} not found`);
      return;
    }

    const elementId = this.getElementId(key);
    const toggleId = `toggle-${key}`;
    const isOpen = this.toggleStates[key];
    let delay = 0;

    if (!isOpen) {
      this.toggleService.expand(elementId, toggleId, section.distance, section.direction as Directions);
    } else {
      this.toggleService.collapse(elementId, toggleId, section.distance, section.direction as Directions);
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
