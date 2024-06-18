import { Component } from '@angular/core';
import { LanguageService, SupportedLanguages } from '../../core/services/language/language.service';
import { SharedDataService } from '../../core/services/shared-data/shared-data.service';
import { ICONS } from '../../shared/constants/app-constants';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  public defaultIconSize: string = ICONS.MOBILE_NAVBAR_DEFAULT_SIZE;
  public components: { id: string, name: string }[] = [];

  constructor(
    private langService: LanguageService,
    private sharedDataService: SharedDataService
  ) { }

  ngOnInit() {

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
    this.sharedDataService.removeTransitionClass();
    this.sharedDataService.currentIndex = view;
  }

  getCurrentView(): number {
    return this.sharedDataService.currentIndex;
  }
}
