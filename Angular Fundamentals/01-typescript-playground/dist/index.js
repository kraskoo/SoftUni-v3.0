class User {
    constructor(name) {
        this.name = name;
    }
    sayHello() {
        return `${this.name} says hi!`;
    }
}
const user = new User('Pesho');
console.log(user.sayHello());
//# sourceMappingURL=index.js.map