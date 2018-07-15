function extractText() {
  let lists = document.querySelectorAll('#items > li');
  let result = '';
  for (let list of lists) {
    result += `${list.textContent}\n`;
  }

  document.getElementById('result').textContent = result;
}