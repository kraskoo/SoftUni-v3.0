const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const TagSchema = new Schema({
  name: { type: String, required: true, unique: true },
  creationDate: { type: Date, default: Date.now },
  images: [{
    type: Schema.Types.ObjectId,
    ref: 'Image'
  }]
});
TagSchema.pre('save', function(next) {
  let doc = this;
  if (doc.isNew || doc.isModified(doc.name)) {
    doc.name = doc.name.toLowerCase();
  }
  
  next();
});
const Tag = mongoose.model('Tag', TagSchema);

module.exports = Tag;