function solve(input) {
  let purchases = [];
  let totalPrice = 0;
  for (let i = 0; i < input.length; i += 2) {
    purchases.push(input[i]);
    totalPrice += Number(input[i + 1]);
  }

  console.log(`You purchased ${purchases.join(', ')} for a total sum of ${totalPrice}`);
}