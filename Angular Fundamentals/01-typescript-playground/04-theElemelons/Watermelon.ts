import Melon from "./Melon";

export default class Watermelon extends Melon {
  constructor(public weight: number, public melonSort: string) {
    super(weight, melonSort);
    this.name = 'Water';
  }
}