export default abstract class Melon {
  protected name: string;
  constructor(public weight: number, public melonSort: string) {
    this.name = 'Melon';
  }

  get elementIndex(): number {
    return this.weight * this.melonSort.length;
  }

  toString(): string {
    return `Element: ${this.name}\nSort: ${this.melonSort}\nElement Index: ${this.elementIndex}`;
  }
};