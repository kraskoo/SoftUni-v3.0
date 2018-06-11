function solve(arr) {
    let dist1 = (arr[0] / 3.6) * arr[2];
    let dist2 = (arr[1] / 3.6) * arr[2];
    let delta = Math.abs(dist1 - dist2);
    console.log(delta);
}