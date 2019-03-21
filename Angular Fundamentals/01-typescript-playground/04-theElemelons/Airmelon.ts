import Melon from "./Melon";

export default class Airmelon extends Melon {
  constructor(public weight: number, public melonSort: string) {
    super(weight, melonSort);
    this.name = 'Air';
  }
}