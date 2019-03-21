"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class Employee {
    constructor(name, age) {
        this.name = name;
        this.age = age;
        this.salary = 0;
        this.tasks = [];
    }
    work() {
        const currentTask = this.tasks.shift();
        console.log(this.name + currentTask);
        this.tasks.push(currentTask);
    }
    collectSalary() {
        console.log(`${this.name} received ${this.getSalary()} this month.`);
    }
    getSalary() {
        return this.salary;
    }
}
class Junior extends Employee {
    constructor(name, salary) {
        super(name, salary);
        this.name = name;
        this.salary = salary;
        this.tasks.push(` is working on a simple task`);
    }
}
exports.Junior = Junior;
class Senior extends Employee {
    constructor(name, salary) {
        super(name, salary);
        this.name = name;
        this.salary = salary;
        this.tasks.push(' is working on complicated task.');
        this.tasks.push(' is taking time off work.');
        this.tasks.push(' is supervising junior workers');
    }
}
exports.Senior = Senior;
class Manager extends Employee {
    constructor(name, salary) {
        super(name, salary);
        this.name = name;
        this.salary = salary;
        this.divident = 0;
        this.tasks.push(' is scheduled a meeting.');
        this.tasks.push(' is preparing a quarterly meeting.');
    }
    getSalary() {
        return this.salary + this.divident;
    }
}
exports.Manager = Manager;
//# sourceMappingURL=Employees.js.map