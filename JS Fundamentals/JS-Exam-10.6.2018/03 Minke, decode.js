function solve(input) {
  let countryPattern = /[A-Z][a-z]*[A-Z]/;
  let townPattern = /\d{3}\.*\d*/g;
  let startIndex = Number(input[0]);
  let endIndex = Number(input[1]);
  let replaceWith = input[2];
  let sentence = input[3];
  let match = countryPattern.exec(sentence);
  let country = '';
  if (match) {
    country = match[0];
  }

  let fromReplace = country.split('').splice(startIndex, endIndex - startIndex + 1).join('');
  country = country.replace(fromReplace, replaceWith);
  country = `${country[0]}${country.split('').splice(1).join('').toLowerCase()}`;

  let townArray = [];
  match = townPattern.exec(sentence);
  while (match) {
    townArray.push(Math.ceil(Number(match[0])));
    match = townPattern.exec(sentence);
  }

  let town = townArray.map(x => String.fromCharCode(x)).join('');
  town = `${town[0].toUpperCase()}${town.split('').slice(1).join('').toLowerCase()}`;
  console.log(`${country} => ${town}`);
}

solve(["3", "5", "gar","114 sDfia 1, riDl10 confin$4%#ed117 likewise it humanity aTe114.223432 BultoriA - Varnd railLery101 an unpacked as he"]);