import { Component } from '@angular/core';
import { WEB } from 'src/app/shared/constants/app-constants';
import { LanguageService, SupportedLanguages } from 'src/app/core/services/language/language.service';

@Component({
  selector: 'app-languages-modal',
  templateUrl: './languages-modal.component.html',
  styleUrl: './languages-modal.component.scss'
})
export class LanguagesModalComponent {
  public supportedLanguages: readonly SupportedLanguages[] = WEB.SUPPORTED_LANGUAGES;

  constructor(private languageService: LanguageService) { }

  getCurrentLang() {
    return this.languageService.getCurrentLang();
  }

  switchLanguage(lang: SupportedLanguages) {
    this.languageService.switchLang(lang);
  }
}
