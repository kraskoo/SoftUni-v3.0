function solve(input) {
  let heroes = [];
  for (let i = 0; i < input.length; i++) {
    let data = input[i].split(/ \/ /);
    heroes.push({ name: data[0], level: Number(data[1]), items: [] });
    if (data.length > 2) {
      let items = data[2].split(', ');
      for (let j = 0; j < items.length; j++) {
        heroes[heroes.length - 1].items.push(items[j]);
      }
    }
  }

  console.log(JSON.stringify(heroes));
}