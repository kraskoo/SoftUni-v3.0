const encryption = require('../util/encryption');
const User = require('../models/User');
const Car = require('../models/Car');
const Rent = require('../models/Rent');

module.exports = {
  registerGet: (req, res) => {
    res.render('user/register');
  },
  registerPost: async (req, res) => {
    const reqUser = req.body;
    if (!reqUser.password || !reqUser.repeatPassword) {
      reqUser.error = 'Please fill all fields';
      res.render('user/register', reqUser);
      return;
    }

    if (reqUser.password !== reqUser.repeatPassword) {
      reqUser.error = 'Passwords must match!';
      res.render('user/register', reqUser);
      return;
    }

    const salt = encryption.generateSalt();
    const hashedPass = encryption.generateHashedPassword(salt, reqUser.password);
    try {
      const user = await User.create({
        username: reqUser.username,
        hashedPass,
        salt,
        firstName: reqUser.firstName,
        lastName: reqUser.lastName,
        roles: [ 'User' ]
      });
      req.logIn(user, err => {
        if (err) {
          reqUser.error = err;
          res.render('user/register', reqUser);
          return;
        } else {
          res.redirect('/');
        }
      });
    } catch (err) {
      reqUser.error = err;
      res.render('user/register', reqUser);
    }
  },
  logout: (req, res) => {
    req.logout();
    res.redirect('/');
  },
  loginGet: (req, res) => {
    res.render('user/login');
  },
  loginPost: async (req, res) => {
    const reqUser = req.body;
    try {
      const user = await User.findOne({ username: reqUser.username });
      if (!user) {
        reqUser.error = 'Invalid username!';
        res.render('user/login', reqUser);
        return;
      }

      if (!user.authenticate(reqUser.password)) {
        reqUser.error = 'Invalid password!';
        res.render('user/login', reqUser);
        return;
      }

      req.logIn(user, err => {
        if (err) {
          reqUser.error = err;
          res.render('user/login', reqUser);
        } else {
          res.redirect('/');
        }
      });
    } catch (err) {
      reqUser.error = err;
      res.render('user/login', reqUser);
    }
  },
  rentGet: (req, res) => {
    let userId = req.user._doc._id;
    Rent.find({ owner: userId }).then(value => {
      let carIds = value.map(x => x._doc.car);
      Car.find({ _id: { $in: carIds } }).then(cars => {
        let extendedCars = cars.map((x, i, a) => {
          let car = x;
          car.expiresOn = value[i].days;
          return car;
        });
        res.render('user/rented', { cars: extendedCars });
      }).catch(console.error);
    }).catch(console.error);
  }
};