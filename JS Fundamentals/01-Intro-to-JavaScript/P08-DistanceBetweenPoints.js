function solve(x1, y1, x2, y2) {
    var point1 = { 'x': x1, 'y': y1 };
    var point2 = { 'x': x2, 'y': y2 };
    var distance = Math.sqrt(Math.pow(point1.x - point2.x, 2) + Math.pow(point1.y - point2.y, 2));
    console.log(distance);
}