function solve(num) {
  let sequence = "ATCGTTAGGG";
  const sequenceLength = sequence.length;
  let str = "";
  for (let i = 0, counter = 0; i < num; i++, counter += 2) {
    if (i % 4 === 0) {
      str += `**${sequence[counter % sequenceLength]}${sequence[(counter + 1) % sequenceLength]}**\n`;
    } else if (i % 4 === 1) {
      str += `*${sequence[counter % sequenceLength]}--${sequence[(counter + 1) % sequenceLength]}*\n`;
    } else if (i % 4 === 2) {
      str += `${sequence[counter % sequenceLength]}----${sequence[(counter + 1) % sequenceLength]}\n`;
    } else if (i % 4 === 3) {
      str += `*${sequence[counter % sequenceLength]}--${sequence[(counter + 1) % sequenceLength]}*\n`;
    }
  }

  console.log(str);
}