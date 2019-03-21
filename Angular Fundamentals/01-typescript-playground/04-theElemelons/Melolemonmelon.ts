import Melon from './Melon';

export default class Melolemonmelon extends Melon {
  constructor(public weight: number, public melonSort: string) {
    super(weight, melonSort);
    this.name = 'Water';
  }

  morph() {
    if (this.name === 'Water') {
      this.name = 'Fire';
    } else if (this.name === 'Fire') {
      this.name = 'Earth';
    } else if (this.name === 'Earth') {
      this.name = 'Air';
    } else {
      this.name = 'Water';
    }
  }
};