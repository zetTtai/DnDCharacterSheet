import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { WEB } from 'src/app/shared/constants/app-constants';

export type SupportedLanguages = typeof WEB.SUPPORTED_LANGUAGES[number];

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  constructor(@Inject(DOCUMENT) private document: Document, private translate: TranslateService) {
    let defaultLang: SupportedLanguages = WEB.DEFAULT_LANG.toLowerCase() as SupportedLanguages;

    if (!WEB.SUPPORTED_LANGUAGES.includes(defaultLang)) {
      defaultLang = WEB.SUPPORTED_LANGUAGES[0];
    }

    this.translate.setDefaultLang(defaultLang);
    this.switchLang(defaultLang);
  }

  switchLang(lang: SupportedLanguages) {
    this.translate.use(lang);

    this.translate.get("title").subscribe((translatedTitle: string) => {
      document.title = translatedTitle;
    });
    document.documentElement.lang = lang;
  }

  get(key: string): Observable<string> {
    return this.translate.get(key);
  }

  getCurrentLang(): SupportedLanguages {
    return this.translate.currentLang as SupportedLanguages ?? 'en';
  }

  setLanguage() {
    if (!WEB.SUPPORTED_LANGUAGES.includes(WEB.DEFAULT_LANG.toLowerCase() as SupportedLanguages)) return;

    this.document.documentElement.lang = WEB.DEFAULT_LANG.toLowerCase();
  }
}
