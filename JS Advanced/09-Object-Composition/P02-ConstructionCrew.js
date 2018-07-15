function solve(input) {
	if (input.handsShaking) {
		input.bloodAlcoholLevel += 0.1 * input.weight * input.experience;
		input.handsShaking = false;
	}
	
	return input;
}