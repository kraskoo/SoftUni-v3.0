function solve() {
	let client = {
		name: arguments[0],
		personalInfo: {
			age: arguments[1],
			weight: arguments[2],
			height: arguments[3],
		},
		BMI: 0,
		status: '',
	};
	client.BMI = Math.round(client.personalInfo.weight / Math.pow(client.personalInfo.height / 100, 2));
	if (client.BMI < 18.5) {
		client.status = 'underweight';
	} else if (client.BMI < 25) {
		client.status = 'normal';
	} else if (client.BMI < 30) {
		client.status = 'overweight';
	} else {
		client.status = 'obese';
		client['recommendation'] = 'admission required';
	}
	
	return client;
}