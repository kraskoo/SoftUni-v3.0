const router = require('express').Router();
const feedController = require('../controllers/feed');

router.post('/street/create', feedController.createStreet);
router.post('/house/create', feedController.createHouse);
router.get('/street/all', feedController.getStreets);

module.exports = router;