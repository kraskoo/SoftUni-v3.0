const mongoose = require('mongoose');

const CarSchema = new mongoose.Schema({
  model: { type: String, required: true },
  image: { type: String, required: true },
  pricePerDay: { type: Number, required: true },
  isRented: { type: Boolean, required: true, default: false }
});

const Car = mongoose.model('Car', CarSchema);
module.exports = Car;