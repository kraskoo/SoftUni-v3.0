function solve(input) {
  let map = new Map();
  for (const townPopulation of input) {
    let townAndPopulation = townPopulation.split(/\s*<->\s*/);
    if (!map.has(townAndPopulation[0])) {
      map.set(townAndPopulation[0], Number(townAndPopulation[1]));
    } else {
      map.set(townAndPopulation[0], map.get(townAndPopulation[0]) + Number(townAndPopulation[1]));
    }
  }

  for ([key, value] of map) {
    console.log(`${key} : ${value}`);
  }
}

solve(['Sofia <-> 1200000',
  'Montana <-> 20000',
  'New York <-> 10000000',
  'Washington <-> 2345000',
  'Las Vegas <-> 1000000']);