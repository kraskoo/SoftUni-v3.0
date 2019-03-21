"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class Melon {
    constructor(weight, melonSort) {
        this.weight = weight;
        this.melonSort = melonSort;
        this.name = 'Melon';
    }
    get elementIndex() {
        return this.weight * this.melonSort.length;
    }
    toString() {
        return `Element: ${this.name}\nSort: ${this.melonSort}\nElement Index: ${this.elementIndex}`;
    }
}
exports.default = Melon;
;
//# sourceMappingURL=Melon.js.map