import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

export type SupportedLanguages = 'es' | 'en';

@Injectable({
  providedIn: 'root'
})

export class SwitchLanguageService {

  constructor(private translate: TranslateService) { }

  switch(lang: SupportedLanguages) {
    this.translate.use(lang);

    this.translate.get("title").subscribe((translatedTitle: string) => {
      document.title = translatedTitle;
    });
    document.documentElement.lang = lang;
  }
}
