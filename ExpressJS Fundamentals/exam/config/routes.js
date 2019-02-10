const controllers = require('../controllers');
const restrictedPages = require('./auth');

module.exports = app => {
  app.get('/', controllers.home.index);
  app.get('/user/register', restrictedPages.isAnonymous, controllers.user.registerGet);
  app.post('/user/register', restrictedPages.isAnonymous, controllers.user.registerPost);
  app.post('/user/logout', restrictedPages.isAuthed, controllers.user.logout);
  app.get('/user/login', restrictedPages.isAnonymous, controllers.user.loginGet);
  app.post('/user/login', restrictedPages.isAnonymous, controllers.user.loginPost);
  app.get('/user/profile', restrictedPages.isAuthed, controllers.user.profileGet);

  app.get('/project/create', restrictedPages.hasRole('Admin'), controllers.project.createProjectGet);
  app.post('/project/create', restrictedPages.hasRole('Admin'), controllers.project.createProjectPost);
  app.post('/project/distribute', restrictedPages.hasRole('Admin'), controllers.project.distributePost);
  app.post('/project/search', restrictedPages.isAuthed, controllers.project.searchPost);
  app.get('/project/all', restrictedPages.isAuthed, controllers.project.allGet);

  app.get('/team/create', restrictedPages.hasRole('Admin'), controllers.team.createTeamGet);
  app.post('/team/create', restrictedPages.hasRole('Admin'), controllers.team.createTeamPost);
  app.post('/team/distribute', restrictedPages.hasRole('Admin'), controllers.team.distributePost);
  app.get('/team/all', restrictedPages.isAuthed, controllers.team.allGet);
  app.post('/team/leave/:id', restrictedPages.isAuthed, controllers.team.leavePost);
  app.post('/team/search', restrictedPages.isAuthed, controllers.team.searchPost);

  app.all('*', (req, res) => {
    res.status(404);
    res.send('404 Not Found');
    res.end();
  });
};