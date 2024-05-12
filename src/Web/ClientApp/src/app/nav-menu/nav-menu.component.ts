import { Component } from '@angular/core';
import { MobileNavigationService } from '../../services/mobile-navigation/mobile-navigation.service';
import { SupportedLanguages, SwitchLanguageService } from '../../services/switch-language/switch-language.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {

  constructor(
    private mobileNavService: MobileNavigationService,
    private switchLangService: SwitchLanguageService
  ) { }

  ngOnInit() {
    const mainMobile = document.getElementById("main-mobile");
    if (mainMobile) {
      this.mobileNavService.setMainMobile(mainMobile);
    }
  }

  switchLanguage(lang: SupportedLanguages) {
    this.switchLangService.switch(lang);
  }

  mobileSlide(view: number) {
    this.mobileNavService.mobileSlide(view);
  }
}
