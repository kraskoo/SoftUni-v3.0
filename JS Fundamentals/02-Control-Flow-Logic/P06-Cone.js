function solve(r, h) {
    var v = (Math.PI * Math.pow(r, 2) * h) / 3;
    var l = Math.PI * r * Math.sqrt(Math.pow(r, 2) + Math.pow(h, 2));
    var b = Math.PI * Math.pow(r, 2);
    var a = l + b;
    console.log(`volume = ${v.toFixed(4)}`);
    console.log(`area = ${a.toFixed(4)}`);
}