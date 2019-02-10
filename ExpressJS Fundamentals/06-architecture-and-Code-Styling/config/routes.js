const homeController = require('../controllers/home');
const userController = require('../controllers/user');
const articleController = require('../controllers/article');
const restrictedPages = require('./auth');

module.exports = (app) => {
  app.get('/', homeController.index);
  app.get('/user/register', userController.registerGet);
  app.post('/user/register', userController.registerPost);
  app.get('/user/login', userController.loginGet);
  app.post('/user/login', userController.loginPost);
  app.get('/user/logout', restrictedPages.isAuthed, userController.logout);
  app.get('/user/details', restrictedPages.isAuthed, userController.detailsGet);

  app.get('/article/create', restrictedPages.isAuthed, articleController.createGet);
  app.post('/article/create', restrictedPages.isAuthed, articleController.createPost);
  app.get('/article/details/:id', articleController.detailGet);
  app.get('/article/edit/:id', restrictedPages.isAuthed, articleController.editGet);
  app.post('/article/edit/:id', restrictedPages.isAuthed, articleController.editPost);
  app.get('/article/delete/:id', restrictedPages.isAuthed, articleController.deleteGet);
  app.post('/article/delete/:id', restrictedPages.isAuthed, articleController.deletePost);
  //TODO Add other app routes and restrict certain pages using auth.js
};