const CACHE_NAME = "offline-v1"; // Update the cache name to include a version when deploy
const filesToCache = [
  '/',
  '/assets/offline.html'
];

const preLoad = function () {
  return caches.open(CACHE_NAME).then(function (cache) {
    // caching index and important routes
    return cache.addAll(filesToCache);
  });
};

self.addEventListener("install", function (event) {
  event.waitUntil(preLoad().then(() => self.skipWaiting()));
});

const checkResponse = function (request) {
  return new Promise(function (fulfill, reject) {
    fetch(request).then(function (response) {
      if (response.status !== 404) {
        fulfill(response);
      } else {
        reject();
      }
    }, reject);
  });
};

const addToCache = function (request) {
  return caches.open(CACHE_NAME).then(function (cache) {
    return fetch(request).then(function (response) {
      return cache.put(request, response);
    });
  });
};

const returnFromCache = function (request) {
  return caches.open(CACHE_NAME).then(function (cache) {
    return cache.match(request).then(function (matching) {
      if (!matching) {
        return cache.match("/assets/offline.html");
      } else {
        return matching;
      }
    });
  });
};

self.addEventListener("fetch", function (event) {
  event.respondWith(checkResponse(event.request).catch(function () {
    return returnFromCache(event.request);
  }));
  if (!event.request.url.startsWith('http')) {
    event.waitUntil(addToCache(event.request));
  }
});

// Handle activating the new service worker and deleting old caches
self.addEventListener('activate', function (event) {
  const cacheWhitelist = [CACHE_NAME];
  event.waitUntil(
    caches.keys().then(function (keyList) {
      return Promise.all(keyList.map(function (key) {
        if (cacheWhitelist.indexOf(key) === -1) {
          return caches.delete(key);
        }
      }));
    }).then(() => self.clients.claim())
  );
});
