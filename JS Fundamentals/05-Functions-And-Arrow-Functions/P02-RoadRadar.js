function solve(input) {
  let [speed, area] = input;
  function getLimit(zone) {
    switch (zone) {
      case "motorway": return 130;
      case "interstate": return 90;
      case "city": return 50;
      case "residential": return 20;
    }
  }
  
  function getInfraction(speed, limit) {
    let overSpeed = speed - limit;
    if (overSpeed <= 0) {
      return false;
    } else {
      if (overSpeed <= 20) {
        return "speeding";
      } else if (overSpeed <= 40) {
        return "excessive speeding";
      } else {
        return "reckless driving";
      }
    }
  }

  let limit = getLimit(area);
  let infraction = getInfraction(speed, limit);
  if (infraction) {
    console.log(infraction);
  }
}