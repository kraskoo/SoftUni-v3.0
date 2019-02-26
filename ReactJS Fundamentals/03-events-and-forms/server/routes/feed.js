const router = require('express').Router();
const feedController = require('../controllers/feed');
const isAuth = require('../middleware/is-auth');

router.get('/games', feedController.getGames);
router.post('/game/create', feedController.createGame);

module.exports = router;