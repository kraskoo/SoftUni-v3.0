"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const Melon_1 = require("./Melon");
class Earthmelon extends Melon_1.default {
    constructor(weight, melonSort) {
        super(weight, melonSort);
        this.weight = weight;
        this.melonSort = melonSort;
        this.name = 'Earth';
    }
}
exports.default = Earthmelon;
//# sourceMappingURL=Earthmelon.js.map