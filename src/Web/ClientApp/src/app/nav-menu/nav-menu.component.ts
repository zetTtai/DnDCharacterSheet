import { Component } from '@angular/core';
import { MobileNavigationService } from '../../services/mobile-navigation/mobile-navigation.service';
import { Observable } from 'rxjs';
import { SharedDataService } from '../../services/shared-data/shared-data.service';
import { SupportedLanguages, LanguageService } from '../../services/language/language.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {

  public components: {id: string, name: string}[] = [];

  constructor(
    private mobileNavService: MobileNavigationService,
    private LangService: LanguageService,
    private sharedDataService: SharedDataService
  ) { }

  ngOnInit() {
    const mainMobile = document.getElementById("main-mobile");

    if (mainMobile) {
      this.mobileNavService.setMainMobile(mainMobile);
    }

    this.sharedDataService.mobileComponents.forEach(component => {
      this.components.push({
        id: `mobile-${component.key}`,
        name: `nav-menu.${component.key}`
      })
    })
  }

  switchLanguage(lang: SupportedLanguages) {
    this.LangService.switchLocale(lang);
  }

  mobileSlide(view: number) {
    this.mobileNavService.mobileSlide(view);
  }
}
