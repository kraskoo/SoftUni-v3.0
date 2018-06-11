function solve(input) {
  function rotateRight(array) {
    let last = array[array.length - 1];
    for (let i = array.length - 1; i >= 1; i--) {
      array[i] = array[i - 1];
    }

    array[0] = last;
  }

  let rotationCount = input[input.length - 1];
  let array = input.slice(0, input.length - 1);
  for (let i = 0; i < rotationCount % array.length; i++) {
    rotateRight(array);
  }

  console.log(array.join(' '));
}