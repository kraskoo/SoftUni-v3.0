const Product = require('../models/Product');
const Category = require('../models/Category');

function getData(req, products) {
  let data = { products };
  if (req.query.error) {
    data.error = req.query.error;
  }

  if (req.query.success) {
    data.success = req.query.success;
  }

  return data;
}

module.exports = {
  index: (req, res) => {
    if (!req.user) {
      res.render('home/index');
      return;
    }

    Category.find()
      .populate('products')
      .then(categories => {
        let products = [];
        for (let category of categories) {
          let categoryProducts = category.products.filter(x => x.buyer === undefined);
          for (let product of categoryProducts) {
            product.category = category;
            products.push(product);
          }
        }

        products.map(x => {
          x.splittedDescription = x.description.split(/\r?\n/g);
          x.isCreator = req.user._id.toHexString() === x.creator._id.toHexString();
          return x;
        });
        let data = getData(req, products);
        res.render('home/index', {
          products: data.products,
          error: data.error,
          success: data.success
        });
      })
      .catch(err => {
        res.redirect(`/?error=${encodeURIComponent(err.message)}`);
      });
  },
  search: (req, res) => {
    if (!req.user) {
      res.redirect('/user/login');
    }

    let search = req.body.query;
    Product.find({ name: { $regex: search, $options: 'i' } })
      .populate('category')
      .where('buyer')
      .equals(undefined)
      .then(products => {
        products.map(x => {
          x.splittedDescription = x.description.split(/\r?\n/g);
          x.isCreator = req.user._id.toHexString() === x.creator._id.toHexString();
          return x;
        });
        let data = getData(req, products);
        res.render('home/index', {
          products: data.products,
          error: data.error,
          success: data.success
        });
      })
      .catch(err => {
        res.redirect(`/?error=${encodeURIComponent(err.message)}`);
      });
  }
};