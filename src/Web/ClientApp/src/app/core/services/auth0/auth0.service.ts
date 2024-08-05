import { Injectable, OnDestroy } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { Subject, takeUntil } from 'rxjs';
import { AUTH0 } from 'src/app/shared/constants/auth0-constants';
import { SharedDataService } from 'src/app/core/services/shared-data/shared-data.service';

@Injectable({
  providedIn: 'root'
})
export class Auth0Service implements OnDestroy {

  private readonly destroy$ = new Subject<void>();

  constructor(
    private sharedDataService: SharedDataService,
    private auth: AuthService
  ) { }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  login() {
    this.auth.loginWithPopup({
      authorizationParams: AUTH0.DEV.authorizationParams
    });
  }

  logout() {
    this.auth.logout();
  }

  isLogged(): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.auth.user$
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: (user) => {
            if (user == null) {
              resolve(false);
              return;
            }

            this.sharedDataService.userId = user.sub;
            resolve(true);
          },
          error: () => {
            reject(false);
          }
        });
    });
  }
}
