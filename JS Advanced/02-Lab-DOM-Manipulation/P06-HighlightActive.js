function focus() {
  let inputs = document.getElementsByTagName('input');
  Array.from(inputs).forEach(i => {
    i.addEventListener('focus', function() {
      if (!this.parentNode.classList.contains('focused')) {
        this.parentNode.classList.add('focused');
      }
    });
    i.addEventListener('blur', function() {
      if (this.parentNode.classList.contains('focused')) {
        this.parentNode.classList.remove('focused');
      }
    });
  });
}