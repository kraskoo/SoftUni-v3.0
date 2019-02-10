const Cube = require('../models/Cube');

module.exports = {
  cubeDetail: (req, res) => {
    let id = req.params.id;
    Cube.findById(id).then(result => {
      let { name, description, image, difficulty } = result._doc;
      res.render('details', { title: 'Cube Details', name, description, image, difficulty });
    }).catch(err => {
      if (err) {
        console.error(err);
      }
    })
  },
  cubeCreateGet: (req, res) => {
    res.render('create', { title: 'Create Cube' });
  },
  cubeCreatePost: (req, res) => {
    console.log();
    let { name, description, image, difficulty } = req.body;
    let cube = new Cube({ name, description, image, difficulty });
    cube.save().then(() => {
      res.redirect('/');
    }).catch(err => {
      if (err) {
        let output = [];
        let errors = err.errors;
        let errorKeys = Object.keys(errors);
        for (let key of errorKeys) {
          let message = errors[key].message;
          output.push(`${key}: ${message}`);
        }

        res.locals.globalErrors = output;
        console.error(err);
        res.render('create');
      }
    });
  }
};