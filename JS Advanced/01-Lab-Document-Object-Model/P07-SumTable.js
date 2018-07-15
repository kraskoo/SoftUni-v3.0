function sum() {
  let rows = document.querySelectorAll('table tr');
  let result = 0;
  for (let i = 1; i < rows.length; i++)
    result += Number(rows[i].children[1].textContent);
  document.getElementById('sum').textContent = result;
}