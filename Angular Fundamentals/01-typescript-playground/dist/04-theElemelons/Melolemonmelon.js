"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const Melon_1 = require("./Melon");
class Melolemonmelon extends Melon_1.default {
    constructor(weight, melonSort) {
        super(weight, melonSort);
        this.weight = weight;
        this.melonSort = melonSort;
        this.name = 'Water';
    }
    morph() {
        if (this.name === 'Water') {
            this.name = 'Fire';
        }
        else if (this.name === 'Fire') {
            this.name = 'Earth';
        }
        else if (this.name === 'Earth') {
            this.name = 'Air';
        }
        else {
            this.name = 'Water';
        }
    }
}
exports.default = Melolemonmelon;
;
//# sourceMappingURL=Melolemonmelon.js.map