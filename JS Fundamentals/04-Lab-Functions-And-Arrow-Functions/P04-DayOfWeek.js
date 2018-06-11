function solve(day) {
  day = day.toLowerCase();
  let num = 0;
  switch (day) {
    case "monday": num = 1; break;
    case "tuesday": num = 2; break;
    case "wednesday": num = 3; break;
    case "thursday": num = 4; break;
    case "friday": num = 5; break;
    case "saturday": num = 6; break;
    case "sunday": num = 7; break;
  }

  console.log(num === 0 ? "error" : num);
}