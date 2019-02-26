const router = require('express').Router();
const feedController = require('../controllers/feed');
const isAuth = require('../middleware/is-auth');

router.get('/movies', feedController.getMovies);
router.post('/movie/create', feedController.createMovie);

module.exports = router;