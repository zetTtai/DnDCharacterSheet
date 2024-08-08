import { AuthConfig } from "@auth0/auth0-angular";

export const AUTH0: { DEV: AuthConfig, PROD: AuthConfig } = {
  DEV: {
    domain: 'dev-eyqtl22nkjyid0lf.us.auth0.com',
    clientId: 'T6XA6ouhjxcByDfqyszfAmstnOxjfYtt',
    authorizationParams: {
      redirect_uri: window.location.origin
    }
  },
  PROD: {
    domain: 'dev-eyqtl22nkjyid0lf.us.auth0.com',
    clientId: 'T6XA6ouhjxcByDfqyszfAmstnOxjfYtt',
    authorizationParams: {
      redirect_uri: window.location.origin
    }
  }
}
