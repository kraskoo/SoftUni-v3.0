function toggle() {
  let span = document.querySelector('span[class="button"]');
  let isDivIsVisible = span.textContent === 'More';
  let extra = document.getElementById('extra');
  if (isDivIsVisible) {
    span.textContent = 'Less';
    extra.style.display = 'block';
  } else {
    span.textContent = 'More';
    extra.style.display = 'none';
  }
}