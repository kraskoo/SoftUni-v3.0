const controllers = require('../controllers');
const restrictedPages = require('./auth');

module.exports = app => {
  app.get('/', controllers.home.index);
  app.get('/user/register', restrictedPages.isAnonymous, controllers.user.registerGet);
  app.post('/user/register', restrictedPages.isAnonymous, controllers.user.registerPost);
  app.get('/user/login', restrictedPages.isAnonymous, controllers.user.loginGet);
  app.post('/user/login', restrictedPages.isAnonymous, controllers.user.loginPost);
  app.get('/user/logout', restrictedPages.isAuthed, controllers.user.logout);

  app.post('/threads/find', restrictedPages.isAuthed, controllers.thread.findPost);
  app.get('/thread/:otherUser', restrictedPages.isAuthed, controllers.thread.otherUserGet);
  app.post('/thread/:otherUser', restrictedPages.isAuthed, controllers.thread.otherUserPost);
  app.post('/block/:username', restrictedPages.isAuthed, controllers.thread.blockUserPost);
  app.post('/unblock/:username', restrictedPages.isAuthed, controllers.thread.unblockUserPost);
  app.post('/threads/remove/:threadId', restrictedPages.hasRole('Admin'), controllers.thread.removePost);
  app.all('*', (req, res) => {
    res.status(404);
    res.send('404 Not Found');
    res.end();
  });
};