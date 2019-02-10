const Car = require('../models/Car');

module.exports = {
  index: (req, res) => {
    res.render('home/index');
  },
  searchPost: (req, res) => {
    let search = req.body.model;
    Car.find({ model: { $regex: search, $options: 'i' } })
      .where({ isRented: false })
      .then(cars => {
        res.render('car/all', { cars });
      }).catch(console.error);
  }
};