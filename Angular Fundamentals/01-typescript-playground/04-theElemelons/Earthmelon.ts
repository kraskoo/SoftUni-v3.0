import Melon from "./Melon";

export default class Earthmelon extends Melon {
  constructor(public weight: number, public melonSort: string) {
    super(weight, melonSort);
    this.name = 'Earth';
  }
}