const encryption = require('../utilities/encryption');
const messageHandler = require('../config/message-handler');
const User = require('../models/User');

module.exports = {
  registerGet: (req, res) => {
    res.render('users/register');
  },
  registerPost: (req, res) => {
    const user = req.body;
    if (user.password && user.password != user.confirmedPassword) {
      messageHandler(res, 'Passwords do not match!', 'users/register');
      return;
    }

    const salt = encryption.generateSalt();
    user.salt = salt;
    if (user.password) {
      let hashedPassaword = encryption.generateHashedPassword(salt, user.password);
      user.password = hashedPassaword;
      User.create(user).then((user) => {
        req.logIn(user, (error, user) => {
          if (error) {
            messageHandler(res, 'Authentication not working!', 'users/register');
            return;
          }

          req.user = user;
          res.redirect('/');
        });
      }).catch(err => {
        messageHandler(res, err.message, 'users/register');
      });
    }
  },
  logout: (req, res) => {
    req.logout();
    res.redirect('/');
  },
  loginGet: (req, res) => {
    res.render('users/login');
  },
  loginPost: (req, res) => {
    let userToLogin = req.body;
    User.findOne({ username: userToLogin.username }).then(user => {
      if (!user || !user.authenticate(userToLogin.password)) {
        messageHandler(res, 'Invalid credentials!', 'users/login');
      } else {
        req.logIn(user, (error, user) => {
          if (error) {
            messageHandler(res, 'Authentication not working!', 'users/login');
            return;
          }

          req.user = user;
          res.redirect('/');
        });
      }
    }).catch(err => {
      messageHandler(res, err.message, 'users/login');
    });
  }
};