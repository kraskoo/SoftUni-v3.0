const home = require('../controllers/homeController');
const cube = require('../controllers/cubeController');

module.exports = app => {
  app.get('/', home.homeGet);
  app.get('/about', home.aboutGet);
  app.post('/search', home.searchPost);
  app.get('/create', cube.cubeCreateGet);
  app.post('/create', cube.cubeCreatePost);
  app.get('/details/:id', cube.cubeDetail);
};