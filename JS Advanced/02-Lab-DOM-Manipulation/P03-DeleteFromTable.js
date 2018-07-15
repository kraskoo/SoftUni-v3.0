function deleteByEmail() {
  let rows = document.querySelectorAll('#customers tr');
  let emailToDelete = document.querySelector('input[name="email"]').value;
  document.getElementById('result').textContent = '';
  let hasFound = false;
  for (let row of rows) {
    let email = row.children[1].textContent;
    if (email === emailToDelete) {
      row.parentNode.removeChild(row);
      hasFound = true;
    }
  }

  if (!hasFound) {
    document.getElementById('result').textContent = 'Not found.';
  }
}