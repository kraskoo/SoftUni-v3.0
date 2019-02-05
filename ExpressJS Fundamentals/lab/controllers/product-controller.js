const fs = require('fs');
const formidable = require('formidable');
const uidk = require('uidk'); // my own library
const messageHandler = require('../config/message-handler');
const Product = require('../models/Product');
const Category = require('../models/Category');
const dbPath = './content/images/database/';

module.exports = {
  addGet: (req, res) => {
    Category.find().then(all => {
      res.render('products/add', { categories: all });
    }).catch(err => {
      messageHandler(res, err.message, 'products/add');
    });
  },
  addPost: (req, res) => {
    const form = new formidable.IncomingForm();
    let newProduct = {};
    form.on('error', err => {
      messageHandler(res, err.message, 'products/add');
    }).on('field', (name, value) => {
      newProduct[name] = value;
    }).on('fileBegin', (name, file) => {
      if (file.name === '') {
        return;
      }

      let splittedPath = file.name.split('.');
      let extension = splittedPath[splittedPath.length - 1];
      file.path = `${dbPath}${uidk()}.${extension}`;
      newProduct.image = file.path.replace('/content', '').replace('.', '');
    }).on('end', () => {
      newProduct.creator = req.user._id;
      let product = new Product(newProduct);
      Category.findById(newProduct.category).then(category => {
        req.user.createdProducts.push(product._id);
        req.user.save().then(() => {
          category.products.push(product._id);
          category.save().then(() => {
            product.save().then(() => {
              res.redirect(`/?success=${encodeURIComponent('Product was added successfully!')}`);
            }).catch(err => {
              Category.find().then(all => {
                messageHandler(res, err.message, 'products/add', { categories: all });
              }).catch(err => {
                if (err) {
                  messageHandler(res, err.message, 'home/index');
                }
              });
            });
          }).catch(err => {
            Category.find().then(all => {
              messageHandler(res, err.message, 'products/add', { categories: all });
            }).catch(err => {
              if (err) {
                messageHandler(res, err.message, 'home/index');
              }
            });
          });
        }).catch(err => {
          Category.find().then(all => {
            messageHandler(res, err.message, 'products/add', { categories: all });
          }).catch(err => {
            if (err) {
              messageHandler(res, err.message, 'home/index');
            }
          });
        });
      }).catch(err => {
        res.redirect(`/?error=${encodeURIComponent(err.message)}`);
      });
    }).parse(req);
  },
  buyGet: (req, res) => {
    let id = req.params.id;
    Product.findById(id).then((product) => {
      product.image = product.image;
      res.render('products/buy', { product });
    }).catch(err => {
      messageHandler(res, err.message, 'products/buy');
    })
  },
  buyPost: (req, res) => {
    let productId = req.params.id;
    Product.findById(productId).then(product => {
      if (product.buyer) {
        let error = `error=${encodeURIComponent('Product was already bought!')}`;
        res.redirect(`/?${error}`);
        return;
      }

      product.buyer = req.user._id;
      product.save().then(() => {
        req.user.boughtProducts.push(productId);
        req.user.save().then(() => {
          res.redirect(`/?success=${encodeURIComponent('Product bought successfully!')}`);
        }).catch(err => {
          messageHandler(res, err.message, 'products/buy');
        });
      }).catch(err => {
        messageHandler(res, err.message, 'products/buy');
      });
    }).catch(err => {
      messageHandler(res, err.message, 'products/buy');
    });
  },
  editGet: (req, res) => {
    let productId = req.params.id;
    Product.findById(productId).then(product => {
      Category.find().then(categories => {
        res.render('products/edit', { product, categories });
      }).catch(err => {
        messageHandler(res, err.message, 'products/edit');
      });
    }).catch(err => {
      messageHandler(res, err.message, 'products/edit');
    });
  },
  editPost: (req, res) => {
    let productId = req.params.id;
    Product.findById(productId).then(product => {
      const form = new formidable.IncomingForm();
      let newProduct = {};
      form.on('error', err => {
        messageHandler(res, err.message, 'products/edit');
      }).on('field', (name, value) => {
        newProduct[name] = value;
      }).on('fileBegin', (name, file) => {
        if (file.name === '') {
          return;
        }

        let splittedPath = file.name.split('.');
        let extension = splittedPath[splittedPath.length - 1];
        file.path = `${dbPath}${uidk()}.${extension}`;
        newProduct.image = file.path.replace('/content', '').replace('.', '');
        let oldImage = product.image.substring(1);
        oldImage = `./content/${oldImage}`;
        fs.unlinkSync(oldImage, err => {
          if (err) {
            messageHandler(res, err.message, 'products/edit');
          }
        });
      }).on('end', () => {
        product.name = newProduct.name;
        product.description = newProduct.description;
        product.price = newProduct.price;
        if (newProduct.hasOwnProperty('image')) {
          product.image = newProduct.image;
        }

        let oldCategory = product.category.toHexString();
        if (newProduct.category !== oldCategory) {
          Category.findById(oldCategory).then(category => {
            category.products.pull(product._id);
            category.save().then(() => {
              Category.findById(newProduct.category).then(nCategory => {
                nCategory.products.push(product._id);
                nCategory.save().then(() => {
                  product.category = newProduct.category; 
                  product.save().then(() => {
                    res.redirect(`/?success=${encodeURIComponent('Product was edited successfully!')}`);
                  }).catch(err => {
                    messageHandler(res, err.message, 'products/edit');
                  });
                }).catch(err => {
                  messageHandler(res, err.message, 'products/edit');
                });
              }).catch(err => {
                messageHandler(res, err.message, 'products/edit');
              });
            }).catch(err => {
              messageHandler(res, err.message, 'products/edit');
            });
          }).catch(err => {
            messageHandler(res, err.message, 'products/edit');
          })
        } else {
          product.save().then(() => {
            res.redirect(`/?success=${encodeURIComponent('Product was edited successfully!')}`);
          }).catch(err => {
            messageHandler(res, err.message, 'products/edit');
          });
        }
      }).parse(req);
    }).catch(err => {
      messageHandler(res, err.message, 'products/edit');
    });
  },
  deleteGet: (req, res) => {
    let productId = req.params.id;
    Product.findById(productId).then(product => {
      res.render('products/delete', { product });
    }).catch(err => {
      messageHandler(res, err.message, 'products/delete')
    });
  },
  deletePost: (req, res) => {
    let productId = req.params.id;
    Product.findById(productId)
      .then(product => {
        Category.findById(product.category.toHexString()).then(category => {
          category.products.pull(product._id);
          category.save().then(() => {
            req.user.createdProducts.pull(product._id);
            req.user.save().then(() => {
              Product.findByIdAndDelete(productId).then((product) => {
                let serverPath = product.image.substring(1);
                serverPath = `./content/${serverPath}`;
                fs.unlink(serverPath, err => {
                  if (err) {
                    messageHandler(res, err.message, 'products/delete');
                    return;
                  }

                  res.redirect(`/?success=${encodeURIComponent('Product was deleted successfully!')}`);
                });
              })
              .catch(err => {
                messageHandler(res, err.message, 'products/delete');
              });
            }).catch(err => {
              messageHandler(res, err.message, 'products/delete');
            });
          }).catch(err => {
            messageHandler(res, err.message, 'products/delete');
          });
        })
        .catch(err => {
          messageHandler(res, err.message, 'products/delete');
        });
      })
      .catch(err => {
        messageHandler(res, err.message, 'products/delete');
      });
  }
}