const controllers = require('../controllers');
const restrictedPages = require('./auth');

module.exports = app => {
  app.get('/', controllers.home.index);
  app.post('/search', controllers.home.search);
  app.get('/product/add', restrictedPages.isAuthed, controllers.product.addGet);
  app.post('/product/add', restrictedPages.isAuthed, controllers.product.addPost);
  app.get('/product/buy/:id', restrictedPages.isAuthed, controllers.product.buyGet);
  app.post('/product/buy/:id', restrictedPages.isAuthed, controllers.product.buyPost);
  app.get('/product/edit/:id', restrictedPages.isAuthed, controllers.product.editGet);
  app.post('/product/edit/:id', restrictedPages.isAuthed, controllers.product.editPost);
  app.get('/product/delete/:id', restrictedPages.isAuthed, controllers.product.deleteGet);
  app.post('/product/delete/:id', restrictedPages.isAuthed, controllers.product.deletePost);
  app.get('/category/add', restrictedPages.isAuthed, controllers.category.addGet);
  app.post('/category/add', restrictedPages.isAuthed, controllers.category.addPost);
  app.get('/category/:name/products', restrictedPages.isAuthed, controllers.category.productsGet);
  app.get('/user/register', restrictedPages.isAnonymous, controllers.user.registerGet);
  app.post('/user/register', restrictedPages.isAnonymous, controllers.user.registerPost);
  app.post('/user/logout', controllers.user.logout);
  app.get('/user/login', restrictedPages.isAnonymous, controllers.user.loginGet);
  app.post('/user/login', restrictedPages.isAnonymous, controllers.user.loginPost);
  app.all('*', (req, res) => {
    res.status(404);
    res.send('404 Not Found');
    res.end();
  });
};