/* eslint-disable no-console */
const mongoose = require('mongoose');
mongoose.Promise = global.Promise;

const User = require('../models/User');

module.exports = config => {
  mongoose.connect(config.dbPath, {
    useNewUrlParser: true,
    useCreateIndex: true
  });
  const db = mongoose.connection;
  db.once('open', err => {
    if (err) {
      throw err;
    }

    User.seedAdminUser();
    console.log('Database ready');
  });
  db.on('error', reason => {
    console.log(reason);
  });
};