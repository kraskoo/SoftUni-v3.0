const mongoose = require('mongoose');
mongoose.Promise = global.Promise;
const User = require('../models/User');

module.exports = config => {
  mongoose.connect(config.dbPath, {
    useNewUrlParser: true
  });     
  const db = mongoose.connection;
  db.once('open', err => {
    if (err) {
      console.log(err);
    }
    
    User.seedAdminUser().then(() => {
      console.log('Database ready');
    }).catch(err => {
      if (err) {
        console.error(err);
      }
    });
  });

  db.on('error', reason => {
    console.log(reason);
  });
};