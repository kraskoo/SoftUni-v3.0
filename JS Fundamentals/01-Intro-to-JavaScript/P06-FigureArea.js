function solve(w, h, W, H) {
    var area1 = w * h;
    var area2 = W * H;
    var area3 = Math.min(w, W) * Math.min(h, H);
    console.log(area1 + area2 - area3);
}