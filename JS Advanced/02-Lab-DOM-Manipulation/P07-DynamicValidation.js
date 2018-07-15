function validate() {
  document.querySelector('input').addEventListener('change', onChange);
  let regex = /^([\w\-.]+)@([a-z]+)(\.[a-z]+)+$/;
  function onChange(event) {
    if (!regex.test(event.target.value))
      event.target.classList.add('error');
    else
      event.target.classList.remove('error')
  }
}