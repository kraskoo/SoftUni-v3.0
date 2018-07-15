function addItem() {
  let items = document.getElementById('items');
  let newItem = document.getElementById('newItemText');
  let newList = document.createElement('li');
  newList.textContent = newItem.value;
  items.appendChild(newList);
  newItem.value = '';
}