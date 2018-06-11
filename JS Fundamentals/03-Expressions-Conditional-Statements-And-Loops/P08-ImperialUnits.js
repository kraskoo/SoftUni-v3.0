function solve(iu) {
    var inches = parseInt(iu / 12);
    var foots = iu % 12;
    console.log(`${inches}'-${foots}"`);
}