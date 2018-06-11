function solve(input) {
  let townArray = [];
  let aggregate = 0;
  for (let i = 0; i < input.length; i++) {
    let townAndIncome = input[i].split('|');
    let town = townAndIncome[1].trim();
    let income = Number(townAndIncome[2].trim());
    townArray.push(town);
    aggregate += income;
  }

  console.log(townArray.join(', '));
  console.log(aggregate);
}