function solve(input) {
  let specializedProfessions = ['Programming', 'Hardware maintenance', 'Cooking', 'Translating', 'Designing'];
  let averageProfessions = ['Driving', 'Managing', 'Fishing', 'Gardening'];
  let clumsyProfessions = ['Singing', 'Accounting', 'Teaching', 'Exam-Making', 'Acting', 'Writing', 'Lecturing', 'Modeling', 'Nursing'];
  let specialized = [];
  let average = [];
  let clumsy = [];
  let bonus = 0;
  for (let row of input) {
    let data = row.split(' : ');
    let profession = data[0];
    let gold = Number(data[1]);
    let hasBonus = false;
    if (specializedProfessions.includes(profession)) {
      if (gold >= 200) {
        specialized.push(gold);
        if (specialized.length % 2 === 0) {
          hasBonus = true;
        }

        specialized[specialized.length - 1] = specialized[specialized.length - 1] - (specialized[specialized.length - 1] * 0.2);
      }
    } else if (averageProfessions.includes(profession)) {
      average.push(gold);
    } else if (clumsyProfessions.includes(profession)) {
      clumsy.push(gold);
      if (clumsy.length % 2 === 0) {
        clumsy[clumsy.length - 1] = clumsy[clumsy.length - 1] - (clumsy[clumsy.length - 1] * 0.05);
      } else if (clumsy.length % 3 === 0) {
        clumsy[clumsy.length - 1] = clumsy[clumsy.length - 1] - (clumsy[clumsy.length - 1] * 0.1);
      }
    }

    if (hasBonus) {
      bonus += 200;
    }
  }

  let total = (specialized.length > 0 ? specialized.reduce((a, b) => a + b) : 0) +
    (average.length > 0 ? average.reduce((a, b) => a + b) : 0) +
    (clumsy.length > 0 ? clumsy.reduce((a, b) => a + b) : 0) +
    bonus;
  if (total < 1000) {
    console.log(`Final sum: ${total.toFixed(2)}`);
    console.log(`Mariyka need to earn ${(1000 - total).toFixed(2)} gold more to continue in the next task.`);
  } else {
    console.log(`Final sum: ${total.toFixed(2)}`);
    console.log(`Mariyka earned ${(total - 1000).toFixed(2)} gold more.`);
  }
}

solve(["Programming : 500", "Driving : 243", "Singing : 100", "Cooking : 199"]);