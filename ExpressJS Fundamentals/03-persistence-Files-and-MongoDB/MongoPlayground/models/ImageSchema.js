const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const ImageSchema = new Schema({
  url: { type: String, required: true },
  creationDate: { type: Date, default: Date.now },
  title: { type: String },
  description: { type: String },
  tags: [{
    type: Schema.Types.ObjectId,
    ref: 'Tag'
  }]
});
const Tag = mongoose.model('Image', ImageSchema);

module.exports = Tag;