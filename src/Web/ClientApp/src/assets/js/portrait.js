// Enforce Orientation with JavaScript (Only vertically, portrait)
if (screen.orientation && screen.orientation.lock) {
  screen.orientation.lock('portrait').catch(function (error) {
    console.error('Orientation lock failed: ', error);
  });
} else if (window.screen.lockOrientation) {
  // For older versions of the API
  window.screen.lockOrientation('portrait');
} else {
  console.warn('Screen Orientation API is not supported in this browser.');
}
