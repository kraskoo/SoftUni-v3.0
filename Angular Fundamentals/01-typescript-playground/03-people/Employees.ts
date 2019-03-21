abstract class Employee {
  public salary: number;
  public tasks: Array<string>;

  constructor(public name: string, public age: number) {
    this.salary = 0;
    this.tasks = [];
  }

  public work(): void {
    const currentTask = this.tasks.shift();
    console.log(this.name + currentTask);
    this.tasks.push(currentTask);
  }

  public collectSalary(): void {
    console.log(`${this.name} received ${this.getSalary()} this month.`);
  }

  public getSalary(): number {
    return this.salary;
  }
}

export class Junior extends Employee {
  constructor(public name: string, public salary: number) {
    super(name, salary);
    this.tasks.push(` is working on a simple task`);
  }
}

export class Senior extends Employee {
  constructor(public name: string, public salary: number) {
    super(name, salary);
    this.tasks.push(' is working on complicated task.');
    this.tasks.push(' is taking time off work.');
    this.tasks.push(' is supervising junior workers');
  }
}

export class Manager extends Employee {
  public divident: number;

  constructor(public name: string, public salary: number) {
    super(name, salary);
    this.divident = 0;
    this.tasks.push(' is scheduled a meeting.');
    this.tasks.push(' is preparing a quarterly meeting.');
  }

  getSalary(): number {
    return this.salary + this.divident;
  }
}