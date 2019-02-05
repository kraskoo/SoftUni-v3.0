const messageHandler = require('../config/message-handler');
const Category = require('../models/Category');

module.exports = {
  addGet: (req, res) => {
    res.render('categories/add');
  },
  addPost: (req, res) => {
    let { name } = req.body;
    let newCategory = { name, creator: req.user._id };
    Category.create(newCategory).then((category) => {
      req.user.createdCategories.push(category._id);
      req.user.save().then(() => {
        res.redirect(`/?success=${encodeURIComponent('Category was added successfully!')}`);
      }).catch(err => {
        messageHandler(res, err.message, 'products/edit');
      });
    }).catch(err => {
      messageHandler(res, err.message, 'categories/add');
    });
  },
  productsGet: (req, res) => {
    let name = req.params.name;
    Category.findOne({ name: name }).populate('products')
      .where('buyer')
      .equals(undefined)
      .then(category => {
        let products = category.products;
        products.map(x => {
          x.splittedDescription = x.description.split(/\r?\n/g);
          x.isCreator = req.user._id.toHexString() === x.creator._id.toHexString();
          return x;
        });
        res.render('categories/products', { name, products });
      }).catch(err => {
        messageHandler(res, err.message, `/category/${name}/products`);
      });
  }
};