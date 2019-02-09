const mongoose = require('mongoose');

const messageSchema =  mongoose.Schema({
  content: { type: String, required: true },
  user: { type: mongoose.Schema.Types.ObjectId, ref: 'User', required: true },
  thread: { type: mongoose.Schema.Types.ObjectId, ref: 'Thread', required: true }
});

const Message = mongoose.model('Message', messageSchema);

module.exports = Message;