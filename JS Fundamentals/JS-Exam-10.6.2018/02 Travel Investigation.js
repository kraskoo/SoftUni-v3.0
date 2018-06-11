function solve(input) {
  let separator = input[1];
  let companies = input[0].split(separator).map(x => x.trim());
  let valid = [];
  let invalid = [];
  for (let i = 2; i < input.length; i++) {
    let current = input[i].toLowerCase();
    let isValid = true;
    for (let j = 0; j < companies.length; j++) {
      if (!current.includes(companies[j])) {
        isValid = false;
      }
    }

    if (isValid) {
      valid.push(current);
    } else {
      invalid.push(current);
    }
  }

  if (valid.length > 0) {
    console.log('ValidSentences');
    for (let i = 0; i < valid.length; i++) {
      console.log(`${i + 1}. ${valid[i]}`);
    }
  }

  if (invalid.length > 0) {
    if (valid.length > 0) {
      console.log('==============================');
    }

    console.log('InvalidSentences');
    for (let i = 0; i < invalid.length; i++) {
      console.log(`${i + 1}. ${invalid[i]}`);
    }
  }
}

solve(["bulgariatour@, minkatrans@, koftipochivkaltd",
  "@,",
  "Mincho e KoftiPochivkaLTD Tip 123  ve MinkaTrans BulgariaTour",
  "dqdo mraz some text but is KoftiPochivkaLTD MinkaTrans",
  "someone continues as no "]
);