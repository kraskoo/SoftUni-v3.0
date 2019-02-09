const Thread = require('../models/Thread');
const messageHandler = require('../config/message-handler');

module.exports = {
  index: (req, res) => {
    Thread.find()
      .populate('users')
      .then(threads => {
        res.render('home/index', { threads });
      })
      .catch(err => {
        messageHandler(res, err.message, 'home/index');
      });
  }
};