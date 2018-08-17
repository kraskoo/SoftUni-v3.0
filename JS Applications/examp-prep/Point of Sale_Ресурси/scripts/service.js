let service = (function() {
	function getActiveReceipt(userId) {
		let endPoint = `receipts?query={"_acl.creator":"${userId}","active":"true"}`;
		return remote.get('appdata', endPoint, 'kinvey');
	}
	
	function getEntriesByReceiptId(receiptId) {
		let endPoint = `entries?query={"receiptId":"${receiptId}"}`;
		return remote.get('appdata', endPoint, 'kinvey');
	}
	
	function createReceipt(active, productCount, total) {
		let data = { active, productCount, total };
		return remote.post('appdata', 'receipts', 'kinvey', data, true);
	}
	
	function updateReceipt(receiptId, active, productCount, total) {
		let endPoint = `receipts/${receiptId}`;
		let data = { active, productCount, total };
		return remote.update('appdata', endPoint, 'kinvey', data, true);
	}
	
	function addEntry(type, qty, price, receiptId) {
		let data = { type, qty, price, receiptId };
		return remote.post('appdata', 'entries', 'kinvey', data, true);
	}
	
	function deleteEntry(entryId) {
		let endPoint = `entries/${entryId}`;
		return remote.remove('appdata', endPoint, 'kinvey');
	}
	
	function getMyReceipts(userId) {
		let endPoint = `receipts?query={"_acl.creator":"${userId}","active":"false"}`;
		return remote.get('appdata', endPoint, 'kinvey');
	}
	
	function receiptDetails(receiptId) {
		let endPoint = `receipts/${receiptId}`;
		return remote.get('appdata', endPoint, 'kinvey');
	}
	
	function commitReceipt(receiptId) {
		let endPoint = `receipts/${receiptId}`;
		return remote.get('appdata', endPoint, 'kinvey', true);
	}
	
	return { getActiveReceipt, getEntriesByReceiptId, createReceipt, updateReceipt, addEntry, deleteEntry, getMyReceipts, receiptDetails, commitReceipt };
}());