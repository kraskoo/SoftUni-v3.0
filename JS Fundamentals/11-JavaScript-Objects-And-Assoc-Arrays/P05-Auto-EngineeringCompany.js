function solve(input) {
  let result = new Map();
  for (let row of input) {
    let data = row.split(' | ');
    let [car, model, produced] = [data[0], data[1], Number(data[2])];
    if (!result.has(car)) {
      result.set(car, new Map());
    }

    if (!result.get(car).has(model)) {
      result.get(car).set(model, produced);
    } else {
      result.get(car).set(model, result.get(car).get(model) + produced);
    }
  }

  for (let [carKey, carValue] of result) {
    console.log(carKey);
    for (let [modelKey, modelValue] of carValue) {
      console.log(`###${modelKey} -> ${modelValue}`);
    }
  }
}