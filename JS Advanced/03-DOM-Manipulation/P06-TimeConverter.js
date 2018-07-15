function attachEventsListeners() {
  let daysBtn = document.getElementById('daysBtn');
  let hoursBtn = document.getElementById('hoursBtn');
  let minutesBtn = document.getElementById('minutesBtn');
  let secondsBtn = document.getElementById('secondsBtn');
  let days = document.getElementById('days');
  let hours = document.getElementById('hours');
  let minutes = document.getElementById('minutes');
  let seconds = document.getElementById('seconds');
  daysBtn.addEventListener('click', function () {
    let val = Number(days.value);
    let h = val * 24;
    let m = h * 60;
    let s = m * 60;
    hours.value = h;
    minutes.value = m;
    seconds.value = s;
  });
  hoursBtn.addEventListener('click', function () {
    let val = Number(hours.value);
    let d = val / 24;
    let m = val * 60;
    let s = m * 60;
    days.value = d;
    minutes.value = m;
    seconds.value = s;
  });
  minutesBtn.addEventListener('click', function () {
    let val = Number(minutes.value);
    let h = val / 60;
    let d = h / 24;
    let s = val * 60;
    hours.value = h;
    days.value = d;
    seconds.value = s;
  });
  secondsBtn.addEventListener('click', function () {
    let val = Number(seconds.value);
    let m = val / 60;
    let h = m / 60;
    let d = h / 24;
    minutes.value = m;
    hours.value = h;
    days.value = d;
  });
}