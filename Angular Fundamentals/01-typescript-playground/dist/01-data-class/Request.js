"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class Request {
    constructor(method, uri, version, message) {
        this.method = method;
        this.uri = uri;
        this.version = version;
        this.message = message;
        this.response = undefined;
        this.fulfilled = false;
    }
}
exports.default = Request;
//# sourceMappingURL=Request.js.map