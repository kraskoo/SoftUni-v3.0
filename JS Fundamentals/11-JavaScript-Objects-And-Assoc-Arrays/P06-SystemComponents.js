function solve(input) {
  let result = new Map();
  for (let row of input) {
    let data = row.split(' | ');
    let [system, component, subComponent] = [data[0], data[1], data[2]];
    if (!result.has(system)) {
      result.set(system, new Map());
    }

    if (!result.get(system).has(component)) {
      result.get(system).set(component, []);
    }

    result.get(system).get(component).push(subComponent);
  }

  let sortedSystems = Array.from(result.keys()).sort((a, b) => {
    let sizeDiff = result.get(b).size - result.get(a).size;
    if (sizeDiff === 0) {
      return a.localeCompare(b);
    } else {
      return sizeDiff;
    }
  });

  for (let system of sortedSystems) {
    console.log(system);
    let sortedComponents = Array.from(result.get(system).keys())
      .sort((a, b) => result.get(system).get(b).length - result.get(system).get(a).length);
    for (let component of sortedComponents) {
      console.log(`|||${component}`);
      for (let subComponent of result.get(system).get(component)) {
        console.log(`||||||${subComponent}`);
      }
    }
  }
}

solve(['SULS | Main Site | Home Page',
  'SULS | Main Site | Login Page',
  'SULS | Main Site | Register Page',
  'SULS | Judge Site | Login Page',
  'SULS | Judge Site | Submittion Page',
  'Lambda | CoreA | A23',
  'SULS | Digital Site | Login Page',
  'Lambda | CoreB | B24',
  'Lambda | CoreA | A24',
  'Lambda | CoreA | A25',
  'Lambda | CoreC | C4',
  'Indice | Session | Default Storage',
  'Indice | Session | Default Security'
]);