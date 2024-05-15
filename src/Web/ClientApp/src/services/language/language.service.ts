import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';

export type SupportedLanguages = 'es' | 'en';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {

  constructor(private translate: TranslateService) { }

  switchLocale(lang: SupportedLanguages) {
    this.translate.use(lang);

    this.translate.get("title").subscribe((translatedTitle: string) => {
      document.title = translatedTitle;
    });
    document.documentElement.lang = lang;
  }

  get(key: string): Observable<string> {
    return this.translate.get(key);
  }
}
