function solve(input) {
  console.log(input.replace(/(-*\d+\.?\d*)\s*\*\s*(-*\d+\.?\d*)/g, (x, y, z) => Number(y) * Number(z)));
}

solve('My bill: 2*2.50 (beer); 2* 1.20 (kepab); -2  * 0.5 (deposit).');