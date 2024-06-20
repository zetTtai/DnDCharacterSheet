import { Component } from '@angular/core';

@Component({
  selector: 'app-pc-fixed-layout',
  templateUrl: './pc-fixed-layout.component.html',
  styleUrl: './pc-fixed-layout.component.scss'
})
export class PcFixedLayoutComponent {

  public isOpen: boolean = true;

  toggleFixedLayout() {
    this.isOpen = !this.isOpen;

    const content = document.getElementById('main-pc');

    if (!this.isOpen) {
      content.style.gridTemplateColumns = 'max-content max-content auto max-content';
      return;
    } 
    content.style.gridTemplateColumns = 'max-content max-content auto 20%';

  }

}
