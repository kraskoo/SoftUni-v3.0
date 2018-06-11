function solve(input) {
  let usernames = [];
  for (let i = 0; i < input.length; i++) {
    let userAndDomain = input[i].split('@');
    let user = userAndDomain[0];
    let domain = userAndDomain[1];
    let address = domain.split('.');
    let addressDomain = "";
    for (let j = 0; j < address.length; j++) {
      addressDomain += address[j][0];
    }

    usernames.push(user + "." + addressDomain);
  }

  console.log(usernames.join(', '));
}