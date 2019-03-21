export default class Box<T> {
  private store: Array<T> = [];

  add(element: T): void {
    this.store.unshift(element);
  }

  remove(): void {
    this.store.shift();
  }

  get count(): number {
    return this.store.length;
  }
}