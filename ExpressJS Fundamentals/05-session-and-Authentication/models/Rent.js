const mongoose = require('mongoose');

const RentSchema = new mongoose.Schema({
  days: { type: Number, required: true },
  car: { type: mongoose.Schema.Types.ObjectId, ref: 'Car', required: true },
  owner: { type: mongoose.Schema.Types.ObjectId, ref: 'User', required: true }
});

const Rent = mongoose.model('Rent', RentSchema);
module.exports = Rent;