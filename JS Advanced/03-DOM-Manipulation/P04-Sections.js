function create(sentences) {
  let content = document.getElementById('content');
  for (let sentence of sentences) {
    let newDiv = document.createElement('div');
    let newParagraph = document.createElement('p');
    newParagraph.textContent = sentence;
    newParagraph.style.display = 'none';
    newDiv.appendChild(newParagraph);
    newDiv.addEventListener('click', function () {
      this.children[0].style.display = 'block';
    });
    content.appendChild(newDiv)
  }
}