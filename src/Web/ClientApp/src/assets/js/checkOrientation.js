function isMobileDevice() {
  return /Mobi|Android|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
}

function checkOrientation() {
  if (isMobileDevice() && window.innerHeight < window.innerWidth) {
    document.getElementById('landscape-warning').style.display = 'flex';
  } else {
    document.getElementById('landscape-warning').style.display = 'none';
  }
}

window.addEventListener('resize', checkOrientation);
window.addEventListener('orientationchange', checkOrientation);

checkOrientation();
