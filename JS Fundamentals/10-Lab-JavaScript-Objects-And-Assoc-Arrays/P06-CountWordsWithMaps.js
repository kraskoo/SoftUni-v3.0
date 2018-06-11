function solve(input) {
  let map = new Map();
  input.join(' ').toLowerCase().split(/\W+/).filter(x => x !== "").forEach(x => {
    if (!map.has(x)) {
      map.set(x, 0);
    }

    map.set(x, map.get(x) + 1);
  });

  let keys = Array.from(map.keys()).sort();
  for (let idx in keys) {
    console.log(`'${keys[idx]}' -> ${map.get(keys[idx])} times`);
  }
}

solve(['Far too slow, you\'re far too slow.']);