function solve(input) {
  let towns = [];
  for (let i = 1; i < input.length; i++) {
    let data = input[i].split(/\s*\|\s*/g).filter(x => x !== "");
    towns.push({ Town: data[0], Latitude: Number(data[1]), Longitude: Number(data[2]) });
  }

  console.log(JSON.stringify(towns));
}

solve(['| Town | Latitude | Longitude |',
  '| Sofia | 42.696552 | 23.32601 |',
  '| Beijing | 39.913818 | 116.363625 |']);