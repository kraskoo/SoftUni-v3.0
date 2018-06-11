function solve(input) {
  let obj = {};
  input.join(' ').split(/\W+/).filter(x => x !== "").forEach(x => {
    if (!obj.hasOwnProperty(x)) {
      obj[x] = 0;
    }

    obj[x]++;
  });
  console.log(JSON.stringify(obj));
}