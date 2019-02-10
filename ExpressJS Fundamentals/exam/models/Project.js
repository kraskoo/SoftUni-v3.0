const mongoose = require('mongoose')

const projectSchema = mongoose.Schema({
  name: { type: mongoose.Schema.Types.String, require: true, unique: true },
  description: { type: mongoose.Schema.Types.String, required: true, maxLength: 50 },
  team: { type: mongoose.Schema.Types.String, ref: 'Team' }
});

const Project = mongoose.model('Project', projectSchema);

module.exports = Project;