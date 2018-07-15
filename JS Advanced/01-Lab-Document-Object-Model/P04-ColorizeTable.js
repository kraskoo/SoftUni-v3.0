function colorize() {
  let tableRows = document.querySelectorAll('table tr');
  for (let i = 0; i < tableRows.length; i++) {
    if ((i + 1) % 2 === 0) {
      tableRows[i].style.backgroundColor = "teal";
    }
  }
}