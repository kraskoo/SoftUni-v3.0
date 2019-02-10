const encryption = require('../util/encryption');
const User = require('mongoose').model('User');
const Team = require('../models/Team');
const Project = require('../models/Project');

module.exports = {
  registerGet: (req, res) => {
    res.render('users/register');
  },
  registerPost: async (req, res) => {
    const body = req.body;
    const salt = encryption.generateSalt();
    const hashedPass = encryption.generateHashedPassword(salt, body.password);
    try {
      let newUser = {
        username: body.username,
        hashedPass,
        salt,
        firstName: body.firstName,
        lastName: body.lastName,
        roles: [ 'User' ]
      };
      if (body.profilePicture && body.profilePicture !== '') {
        newUser.profilePicture = body.profilePicture;
      }

      const user = await User.create(newUser);
      req.logIn(user, (err, user) => {
        if (err) {
          res.locals.error = err;
          res.render('users/register', user);
        } else {
          res.redirect('/');
        }
      });
    } catch (e) {
      console.log(e);
      res.locals.error = e;
      res.render('users/register');
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
        errorHandler('Invalid user data');
        return;
      }

      if (!user.authenticate(body.password)) {
        errorHandler('Invalid user data');
        return;
      }

      req.logIn(user, (err, user) => {
        if (err) {
          errorHandler(err);
        } else {
          res.redirect('/');
        }
      });
    } catch (e) {
      errorHandler(e);
    }

    function errorHandler(e) {
      console.log(e);
      res.locals.error = e;
      res.render('users/login');
    }
  },
  profileGet: (req, res) => {
    Team.find({ members: { $in: [req.user._id] } })
      .populate('projects')
      .then(teams => {
        let hasTeams = teams.length > 0;
        let projects = [];
        teams.forEach(x => {
          for (let p of x.projects) {
            projects.push(p);
          }
        });
        let hasProjects = projects.length > 0;
        res.render('users/profile', {
          hasTeams,
          hasProjects,
          teams,
          projects
        });
      })
      .catch(err => {
        console.log(err);
      });
  }
};