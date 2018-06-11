function solve(arr) {
    var x1 = arr[0];
    var y1 = arr[1];
    var z1 = arr[2];
    var x2 = arr[3];
    var y2 = arr[4];
    var z2 = arr[5];
    var deltaX = x1 - x2;
    var deltaY = y1 - y2;
    var deltaZ = z1 - z2;
    var distance = Math.sqrt(Math.pow(deltaX, 2) + Math.pow(deltaY, 2) + Math.pow(deltaZ, 2));
    console.log(distance);
}