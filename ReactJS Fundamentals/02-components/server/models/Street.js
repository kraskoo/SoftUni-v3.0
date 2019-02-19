const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const streetSchema = new Schema({
  location: {
    type: String,
    required: true
  },
  homes: [{
    type: Schema.Types.ObjectId,
    ref: 'House'
  }]
});

module.exports = mongoose.model('Street', streetSchema);