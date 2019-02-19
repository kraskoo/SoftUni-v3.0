const House = require('../models/House');
const Street = require('../models/Street');

module.exports = {
  createStreet: (req, res) => {
    // create a street and return it with a message
    let location = req.body.location;
    Street.create({
        location
      })
      .then((street) => {
        res
          .status(201)
          .json({
            message: 'Street created successfully.',
            street
          })
      })
      .catch((error) => {
        if (!error.statusCode) {
          error.statusCode = 500;
        }
        next(error);
      })
  },
  createHouse: (req, res) => {
    // Create a house, add it to a street and return it with a message
    let location = req.body.location;
    let type = req.body.type;
    let description = req.body.description;
    let price = req.body.price;
    let imageUrl = req.body.imageUrl;
    House.create({
        type,
        description,
        price,
        imageUrl
      })
      .then((house) => {
        Street.findOne({
            location
          })
          .then((street) => {
            street.homes.push(house._id);
            street.save()
              .then(() => {
                res
                  .status(200)
                  .json({
                    message: 'House created successfully.',
                    house
                  });
              })
          })
      })
      .catch((error) => {
        if (!error.statusCode) {
          error.statusCode = 500;
        }
        next(error);
      })
  },
  getStreets: (req, res) => {
    // Retrieve all streets in JSON format
    Street.find()
      .populate('homes')
      .then((streets) => {
        res
          .status(200)
          .json({
            message: 'Fetched streets successfully.',
            streets
          });
      })
      .catch((error) => {
        if (!error.statusCode) {
          error.statusCode = 500;
        }

        next(error);
      });
  }
}