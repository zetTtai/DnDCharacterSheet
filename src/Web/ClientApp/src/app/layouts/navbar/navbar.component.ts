import { Component } from '@angular/core';
import { LanguageService, SupportedLanguages } from 'src/app/core/services/language/language.service';
import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';
import { ICONS, WEB } from 'src/app/shared/constants/app-constants';
import { BaseComponent } from 'src/app/core/components/base.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent extends BaseComponent{
  public defaultIconSize: string = ICONS.MOBILE_NAVBAR_DEFAULT_SIZE;
  public components: { id: string, name: string }[] = [];
  public isAccountSectionOpenned: boolean = false;
  public currentLangSize: string = WEB.PC_CURRENT_LANG_SIZE;
  public langSize: string = WEB.PC_LANG_SIZE;


  constructor(
    private langService: LanguageService,
    public sharedDataService: SharedDataService
  ) {
    super(sharedDataService);
  }

  ngOnInit() {

    this.sharedDataService.mobileComponents.forEach(component => {
      this.components.push({
        id: `mobile-${component.key}`,
        name: `nav-menu.${component.key}`
      })
    })
  }

  switchLanguage(lang: SupportedLanguages) {
    this.langService.switchLang(lang);
  }

  mobileSlide(view: number) {
    this.sharedDataService.removeTransitionClass();
    this.sharedDataService.currentIndex = view;
  }

  getCurrentView(): number {
    return this.sharedDataService.currentIndex;
  }

  toggleAccount() {
    this.isAccountSectionOpenned = !this.isAccountSectionOpenned;
    console.log(this.isAccountSectionOpenned ? "Open" :"Cloze")
  }

  getCurrentLanguage() {
    return this.langService.getCurrentLang();
  }

  getSupportedLanguages() {
    return this.langService.supportedLanguages;
  }
}
