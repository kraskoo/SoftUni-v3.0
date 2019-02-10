const Car = require('../models/Car');
const Rent = require('../models/Rent');

module.exports = {
  addGet: (req, res) => {
    res.render('car/add');
  },
  addPost: (req, res) => {
    let { model, image, pricePerDay } = req.body;
    Car.create({ model, image, pricePerDay }).then(() => {
      res.redirect('/');
    }).catch(console.error);
  },
  allGet: async (req, res) => {
    let cars = await Car.find().where('isRented').equals(false);
    res.render('car/all', { cars });
  },
  rentGet: (req, res) => {
    let id = req.params.id;
    Car.findById(id).then((value) => {
      let { _id, image } = value._doc;
      res.render('car/rent', { _id, image });
    }).catch(console.error)
  },
  rentPost: (req, res) => {
    let carId = req.params.id
    let days = parseInt(req.body.days);
    Rent.create({
      days,
      car: carId,
      owner: req.user._id
    }).then(() => {
      Car.findByIdAndUpdate(carId, { isRented: true }).then(() => {
        res.redirect('/car/all');
      }).catch(console.error);
    }).catch(console.error);  
  },
  editGet: (req, res) => {
    let id = req.params.id;
    Car.findById(id).then(car => {
      let { model, image, pricePerDay } = car;
      res.render('car/edit', { model, image, pricePerDay });
    }).catch(console.error);
  },
  editPost: async (req, res) => {
    let id = req.params.id;
    let { model, image, pricePerDay } = req.body;
    try {
      let car = await Car.findById(id);
      car.model = model;
      car.image = image;
      car.pricePerDay = parseInt(pricePerDay);
      try {
        await car.save();
        res.redirect('/car/all');
      } catch (error) {
        console.error(error);
      }
    } catch (error) {
      console.error(error);
    }
  }
};