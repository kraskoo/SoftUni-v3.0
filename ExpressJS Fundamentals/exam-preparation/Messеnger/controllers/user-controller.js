const encryption = require('../util/encryption');
const User = require('mongoose').model('User');
const messageHandler = require('../config/message-handler');

module.exports = {
  registerGet: (req, res) => {
    res.render('users/register');
  },
  registerPost: async (req, res) => {
    const body = req.body;
    const salt = encryption.generateSalt();
    const hashedPass = encryption.generateHashedPassword(salt, body.password);
    try {
      const user = await User.create({
        username: body.username,
        firstName: body.firstName,
        lastName: body.lastName,
        hashedPass,
        salt,
        roles: [ 'User' ]
      });
      req.logIn(user, (err, user) => {
        if (err) {
          messageHandler(res, err.message, 'users/register', user);
        } else {
          res.redirect('/');
        }
      });
    } catch (e) {
      messageHandler(res, e.message, 'users/register');
    }
  },
  logout: (req, res) => {
    req.logout();
    res.redirect('/');
  },
  loginGet: (req, res) => {
    res.render('users/login');
  },
  loginPost: async (req, res) => {
    const body = req.body;
    try {
      const user = await User.findOne({ username: body.username });
      if (!user) {
        messageHandler(res, 'Invalid user data', 'users/login');
        return;
      }
      
      if (!user.authenticate(body.password)) {
        messageHandler(res, 'Invalid user data', 'users/login');
        return;
      }

      req.logIn(user, (err, user) => {
        if (err) {
          messageHandler(res, err.message, 'users/login');
        } else {
          res.redirect('/');
        }
      });
    } catch (e) {
      messageHandler(res, e.message, 'users/login');
    }
  }
};