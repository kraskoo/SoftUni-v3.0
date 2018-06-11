function solve(input) {
  function getQuestionAndAnswer(question, answer) {
    return `  <question>\n    ${question}\n  </question>\n  <answer>\n    ${answer}\n  </answer>\n`;
  }

  let str = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
  str += "<quiz>\n";
  for (let i = 0; i < input.length; i += 2) {
    let question = input[i];
    let answer = input[i + 1];
    str += getQuestionAndAnswer(question, answer);
  }
  
  str += "</quiz>";
  console.log(str);
}

solve(["Who was the forty-second president of the U.S.A.?", "William Jefferson Clinton"]);
solve(["Dry ice is a frozen form of which gas?", "Carbon Dioxide", "What is the brightest star in the night sky?", "Sirius"]);