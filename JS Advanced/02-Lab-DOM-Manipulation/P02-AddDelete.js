function addItem() {
  let newText = document.getElementById('newText');
  let newList = document.createElement('li');
  newList.innerHTML = newText.value + ' <a href="#">[Delete]</a>';
  document.getElementById('items').appendChild(newList);
  newList.children[0].addEventListener('click', deleteItem);
  function deleteItem() {
    this.parentNode.parentNode.removeChild(this.parentNode)
  }
}