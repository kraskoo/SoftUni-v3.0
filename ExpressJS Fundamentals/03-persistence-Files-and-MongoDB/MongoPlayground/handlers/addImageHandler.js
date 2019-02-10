const formidable = require('formidable');
const Image = require('../models/ImageSchema');
const Tag = require('../models/TagSchema');

function addImage(req, res) {
  let form = new formidable.IncomingForm();
  let newImage = {};
  let tagIdValues = null;
  form.on('field', (name, value) => {
    if (name === 'imageUrl') {
      name = 'url';
    } else if (name === 'imageTitle') {
      name = 'title';
    } else if (name === 'tagsID') {
      tagIdValues = value.split(',').filter(x => x !== '');
    }

    if (name !== 'tagsID' && name !== 'tags') {
      newImage[name] = value;
    }
  }).on('end', () => {
    newImage.tags = tagIdValues;
    let image = new Image(newImage);
    image.save().then(result => {
      let imageId = result._id;
      tagIdValues.forEach((id, index, array) => {
        Tag.findByIdAndUpdate(id, { $push: { images: imageId } }, { 'new': true }, (err, result) => {
          if (err) {
            console.error(err);
          }
          
          if (index === array.length - 1) {
            res.redirect('/');
          }
        });
      });
    }).catch(err => {
      if (err) {
        console.error(err);
      }
    });
  }).parse(req);
}

function deleteImg(req, res) {
  let id = req.pathquery.id;
  Image.findById(id).then(result => {
    let tagIds = [];
    result.tags.forEach(x => {
      tagId = x._id.toHexString();
      if (!tagIds.includes(tagId)) {
        tagIds.push(tagId);
      }
    });
    for (let tagId of tagIds) {
      Tag.findByIdAndUpdate(tagId, { $pull: { images: id } }, (err, result) => {
        if (err) {
          console.error(err);
        }
      });
    }

    Image.findByIdAndDelete(id, (err, data) => {
      if (err) {
        console.error(err);
      }
      
      res.redirect('/');
    });
  }).catch(err => {
    if (err) {
      console.error(err);
    }
  });
}

module.exports = (req, res) => {
  if (req.pathname === '/addImage' && req.method === 'POST') {
    addImage(req, res);
  } else if (req.pathname === '/delete' && req.method === 'GET') {
    deleteImg(req, res);
  } else {
    return true;
  }
};