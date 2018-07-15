window.onload = function() {
  countdown(600);
  function countdown(time) {
    setInterval(() => {
      time--;
      let min = Math.floor(time / 60);
      let sec = time % 60;
      let resultTime = min + ':' + (sec < 10 ? '0' + sec : sec);
      document.getElementById('time').value = resultTime;
    }, 1000);
  }
}