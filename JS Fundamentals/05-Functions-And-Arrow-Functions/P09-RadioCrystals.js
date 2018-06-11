function solve(input) {
  let [cut, lap, grind, etch, xRay, transportingAndWashing] = [
    ((x) => x / 4),
    ((x) => x - (x * 0.2)),
    ((x) => x - 20),
    ((x) => x - 2),
    ((x) => x + 1),
    ((x) => parseInt(x))
  ];
  let desiredThickness = input[0];
  for (let i = 1; i < input.length; i++) {
    let thickness = input[i];
    let [cuts, laps, grinds, etches, xRays] = [0, 0, 0, 0, 0];
    console.log(`Processing chunk ${thickness} microns`);
    while (thickness !== desiredThickness) {
      if (cut(thickness) >= desiredThickness - 1) {
        thickness = cut(thickness);
        cuts++;
        thickness = transportingAndWashing(thickness);
        continue;
      }

      if (lap(thickness) >= desiredThickness - 1) {
        thickness = lap(thickness);
        laps++;
        thickness = transportingAndWashing(thickness);
        continue;
      }

      if (grind(thickness) >= desiredThickness - 1) {
        thickness = grind(thickness);
        grinds++;
        thickness = transportingAndWashing(thickness);
        continue;
      }

      if (etch(thickness) >= desiredThickness - 1) {
        thickness = etch(thickness);
        etches++;
        thickness = transportingAndWashing(thickness);
        continue;
      }

      if (xRay(thickness) === desiredThickness) {
        thickness = xRay(thickness);
        xRays++;
      }
    }

    if (cuts > 0) {
      console.log(`Cut x${cuts}`);
      console.log("Transporting and washing");
    }

    if (laps > 0) {
      console.log(`Lap x${laps}`);
      console.log("Transporting and washing");
    }

    if (grinds > 0) {
      console.log(`Grind x${grinds}`);
      console.log("Transporting and washing");
    }

    if (etches > 0) {
      console.log(`Etch x${etches}`);
      console.log("Transporting and washing");
    }

    if (xRays > 0) {
      console.log(`X-ray x${xRays}`);
    }

    console.log(`Finished crystal ${thickness} microns`);
  }
}

solve([1000, 4000, 8100]);