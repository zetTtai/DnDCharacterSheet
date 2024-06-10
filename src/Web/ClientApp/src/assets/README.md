## Purpose
Folder configured in angular.json that include all public content of the application.

### Children

- [i18n](): Where translations can be found (Json files with the name {iso}.json, for example: es.json)
- [icons](): Where all icons can be found.
- [images](): Where all images can be found.
- [js](): Where the public javascript can be found, for example: serviceWorker.js

### Related with
- `manifest.json`: what makes this application PWA
- `serviceWorker.js`: It's a small javascript file where we can cache different routes and content of our PWA.
- `offline.html`: HTML that appears when user tries to connect without connection
