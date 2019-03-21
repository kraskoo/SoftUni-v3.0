import Box from './Box';

let intBox = new Box<Number>();
intBox.add(1);
intBox.add(2);
intBox.add(3);
console.log(intBox.count);
let stringBox = new Box<String>();
stringBox.add("Pesho");
stringBox.add("Gosho");
console.log(stringBox.count);
stringBox.remove();
console.log(stringBox.count);