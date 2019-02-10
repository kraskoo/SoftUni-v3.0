const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const CubeSchema = new Schema({
  name: { type: String },
  description: { type: String },
  image: { type: String },
  difficulty: { type: Number }
});

CubeSchema.path('name').validate(function() {
  return this.name.length >= 3 && this.name.length <= 15;
}, 'Name must be between 3 and 15 symbols');
CubeSchema.path('description').validate(function() {
  return this.description.length >= 20 && this.description.length <= 300;
}, 'Description must be between 20 and 300 symbols');
CubeSchema.path('image').validate(function() {
  return this.image.startsWith('https') && (this.image.endsWith('.jpg') || this.image.endsWith('.png'));
}, 'Image Url should start with https and end with .jpg or .png');
CubeSchema.path('difficulty').validate(function() {
  return this.difficulty >= 1 && this.difficulty <= 6;
}, 'Difficulty must be in range [1.. 6]');


const Cube = mongoose.model('Cube', CubeSchema);

module.exports = Cube;