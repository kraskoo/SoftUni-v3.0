const Cube = require('../models/Cube');

/**
 * 
 * @param {Number} value 
 */
function isDifficultyValid(value) {
  return value >= 1 && value <= 6;
}

module.exports = {
  homeGet: (req, res) => {
    Cube.find().then(result => {
      res.render('index', { title: 'Home', cubes: result });
    }).catch(err => {
      if (err) {
        console.error(err);
      }
    });
  },
  aboutGet: (req, res) => {
    res.render('about', { title: 'About' });
  },
  searchPost: (req, res) => {
    let { search, from, to } = req.body;
    if (from !== '') {
      from = parseInt(from);
    }

    if (to !== '') {
      to = parseInt(to);
    }
    
    let hasFrom = typeof(from) === 'number';
    let hasTo = typeof(to) === 'number';
    if (hasFrom || hasTo) {
      let errors = [];
      if (hasFrom && !isDifficultyValid(from)) {
        errors.push('From value must be in range [1.. 6]');
      }

      if (hasTo && !isDifficultyValid(to)) {
        errors.push('To value must be in range [1.. 6]');
      }

      if (errors.length > 0) {
        res.locals.globalErrors = errors;
        res.render('index', { title: 'Search Results' });
        return;
      }
    }

    let query = search !== '' ? Cube.find({ name: { $regex: search, $options: 'i' } }) : Cube.find();
    if (hasFrom || hasTo) {
      if (hasFrom) {
        query = hasTo ? query.where('difficulty').gte(from).lte(to) : query.where('difficulty').gte(from);
      } else if (hasTo) {
        query = query.where('difficulty').lte(to);
      }
    }

    query.then(result => {
      res.render('index', { title: 'Search Results', cubes: result });
    }).catch(err => {
      if (err) {
        console.log(err);
      }
    });
  }
};