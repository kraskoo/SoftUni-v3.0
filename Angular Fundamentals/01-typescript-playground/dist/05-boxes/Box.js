"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class Box {
    constructor() {
        this.store = [];
    }
    add(element) {
        this.store.unshift(element);
    }
    remove() {
        this.store.shift();
    }
    get count() {
        return this.store.length;
    }
}
exports.default = Box;
//# sourceMappingURL=Box.js.map