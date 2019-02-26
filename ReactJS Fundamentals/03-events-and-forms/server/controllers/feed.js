const Game = require('../models/Game');

module.exports = {
  getGames: (req, res) => {
    Game.find().then(games => {
      res.status(200).json({ message: 'Fetched games successfully.', games });
    }).catch(error => {
      if (!error.statusCode) {
        error.statusCode = 500;
      }

      next(error);
    });
  },
  createGame: (req, res) => {
    const game = req.body;
    Game.create(game).then((game) => {
      res.status(200).json({ message: 'Game created successfully!', game })
    }).catch(error => {
      if (!error.statusCode) {
        error.statusCode = 500;
      }

      next(error);
    });
  },
  getGamesByCategory: (req, res) => {
    const category = req.params.category;
    Game.find({ categories: { $all: [category] } }).then(games => {
      res.status(200).json({ message: `${category} games fetched.`, games })
    }).catch(error => {
      if (!error.statusCode) {
        error.statusCode = 500;
      }

      next(error);
    });
  }
}