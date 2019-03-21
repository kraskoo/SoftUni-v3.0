"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const Melon_1 = require("./Melon");
class Watermelon extends Melon_1.default {
    constructor(weight, melonSort) {
        super(weight, melonSort);
        this.weight = weight;
        this.melonSort = melonSort;
        this.name = 'Water';
    }
}
exports.default = Watermelon;
//# sourceMappingURL=Watermelon.js.map