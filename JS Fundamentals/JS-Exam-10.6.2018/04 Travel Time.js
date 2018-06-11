function solve(input) {
  let result = {};
  for (let row of input) {
    let data = row.split(' > ');
    let country = data[0];
    let city = data[1];
    let travelCost = Number(data[2]);
    city = `${city[0].toUpperCase()}${city.split('').splice(1).join('')}`;
    if (!result.hasOwnProperty(country)) {
      result[country] = {};
    }

    if (!result[country].hasOwnProperty(city)) {
      result[country][city] = travelCost;
    } else {
      if (result[country][city] > travelCost) {
        result[country][city] = travelCost;
      }
    }
  }

  let resultKeys = Object.keys(result).sort((a, b) => a.localeCompare(b));
  for (let country of resultKeys) {
    let towns = Object.keys(result[country]).sort((a, b) => result[country][a] - result[country][b]).map(x => `${x} -> ${result[country][x]}`);
    console.log(`${country} -> ${towns.join(' ')}`);
  }
}

solve(["Bulgaria > Sofia > 500",
  "Bulgaria > Sopot > 800",
  "France > Paris > 2000",
  "Albania > Tirana > 1000",
  "Bulgaria > Sofia > 200" ]);