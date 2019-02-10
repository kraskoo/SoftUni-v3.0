const formidable = require('formidable');
const Tag = require('../models/TagSchema');

module.exports = (req, res) => {
  if (req.pathname === '/generateTag' && req.method === 'POST') {
    let form = new formidable.IncomingForm();
    let newTag = {};
    form.on('field', (name, value) => {
      if (name === 'tagName') {
        name = 'name';
      }

      newTag[name] = value;
    }).on('end', () => {
      let tag = new Tag(newTag);
      tag.save().then(() => {
        res.redirect('/');
      }).catch(err => {
        if (err) {
          console.error(err);
          return;
        }
      })
    }).parse(req);
  } else {
    return true;
  }
};