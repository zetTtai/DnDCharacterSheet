import { Component, ViewChild, ViewContainerRef } from '@angular/core';
import { WEB } from '../../shared/constants/app-constants';
import { LanguageService } from '../../core/services/language/language.service';
import { DelayService } from '../../core/services/delay/delay.service';


@Component({
  selector: 'app-pc-fixed-layout',
  templateUrl: './pc-fixed-layout.component.html',
  styleUrl: './pc-fixed-layout.component.scss'
})
export class PcFixedLayoutComponent {
  @ViewChild('content', { read: ViewContainerRef }) content: ViewContainerRef;

  public isOpen: boolean = true;
  public toggleInfo: string = "";

  fixedLayouts = [
    {
      id: 'features-feats',
      iconName: 'features-feats',
      showWallet: false,
    },
    {
      id: 'items',
      iconName: 'mobile-items',
      showWallet: true,
    },
    {
      id: 'notes',
      iconName: 'notes',
      showWallet: false,
    }
  ];

  constructor(private delayService: DelayService, private languageService: LanguageService) {
    this.languageService.get(WEB.PC_FIXED_LAYOUT_EXPAND).subscribe((translatedInfo: string) => {
      this.toggleInfo = translatedInfo;
    });
  }

  closeFixedLayouts() {
    this.fixedLayouts.forEach(layout => {
      const element = document.getElementById(layout.id);
      element.style.gridTemplateColumns = 'auto';
    });
  }

  openFixedLayouts() {
    this.fixedLayouts.forEach(layout => {
      const element = document.getElementById(layout.id);
      element.style.gridTemplateColumns = 'max-content auto';
    });
  }

  toggleFixedLayout() {

    this.isOpen = !this.isOpen;

    console.log(this.isOpen);

    const content = document.getElementById('content');

    if (!this.isOpen) {
      content.style.gridTemplateColumns = 'max-content max-content auto 4%';
      this.languageService.get(WEB.PC_FIXED_LAYOUT_EXPAND).subscribe((translatedInfo: string) => {
        this.toggleInfo = translatedInfo;
      });
      this.closeFixedLayouts();
      return;
    }

    this.languageService.get(WEB.PC_FIXED_LAYOUT_COLLAPSE).subscribe((translatedInfo: string) => {
      this.toggleInfo = translatedInfo;
    });

    content.style.gridTemplateColumns = WEB.PC_FIXED_LAYOUT;
    this.openFixedLayouts();
  }

}
