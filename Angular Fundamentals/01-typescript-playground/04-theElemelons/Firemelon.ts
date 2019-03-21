import Melon from "./Melon";

export default class Firemelon extends Melon {
  constructor(public weight: number, public melonSort: string) {
    super(weight, melonSort);
    this.name = 'Fire';
  }
}