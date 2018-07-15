function addItem() {
  let text = document.getElementById('newItemText');
  let value = document.getElementById('newItemValue');
  let newOption = document.createElement('option');
  newOption.value = value.value;
  newOption.textContent = text.value;
  document.getElementById('menu').appendChild(newOption);
  value.value = '';
  text.value = '';
}