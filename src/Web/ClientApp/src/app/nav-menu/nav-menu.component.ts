import { Component } from '@angular/core';
import { SharedDataService } from '../../services/shared-data/shared-data.service';
import { SupportedLanguages, LanguageService } from '../../services/language/language.service';
import { NavigationService } from '../../services/navigation/navigation.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {

  public components: {id: string, name: string}[] = [];

  constructor(
    private navService: NavigationService,
    private langService: LanguageService,
    private sharedDataService: SharedDataService
  ) { }

  ngOnInit() {
    const mainMobile = document.getElementById("main-mobile");

    if (mainMobile) {
      this.navService.setMainMobile(mainMobile);
    }

    this.sharedDataService.mobileComponents.forEach(component => {
      this.components.push({
        id: `mobile-${component.key}`,
        name: `nav-menu.${component.key}`
      })
    })
  }

  switchLanguage(lang: SupportedLanguages) {
    this.langService.switchLocale(lang);
  }

  mobileSlide(view: number) {
    this.navService.mobileSlide(view);
  }

  getCurrentView(): number {
    return this.navService.currentViewMobile;
  }
}
