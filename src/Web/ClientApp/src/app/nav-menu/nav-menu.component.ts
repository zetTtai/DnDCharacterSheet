import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

type SupportedLanguages = 'es' | 'en';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {

  constructor(private translate: TranslateService) { }

  switchLanguage(lang: SupportedLanguages) {
    this.translate.use(lang);
  }

}
