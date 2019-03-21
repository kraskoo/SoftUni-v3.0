"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const Box_1 = require("./Box");
let intBox = new Box_1.default();
intBox.add(1);
intBox.add(2);
intBox.add(3);
console.log(intBox.count);
let stringBox = new Box_1.default();
stringBox.add("Pesho");
stringBox.add("Gosho");
console.log(stringBox.count);
stringBox.remove();
console.log(stringBox.count);
//# sourceMappingURL=index.js.map