function getDollarFormatter(formatter) {
	function formatCurrency(separator, symbol, symbolFirst, value) {
		let result = Math.trunc(value) + separator;
		result += value.toFixed(2).substr(-2, 2);
		if (symbolFirst) return `${symbol} ${result}`;
		else return `${result} ${symbol}`;
	}

  function dollarFormatter(value) {
		return formatter(',', '$', true, value);
  }

  return dollarFormatter;
}