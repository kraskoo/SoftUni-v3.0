const Article = require('../models/Article');

module.exports = {
  index: (req, res) => {
    Article.find()
      .populate('author')
      .then(articles => {
        articles.forEach(x => {
          x.shortContent = `${x.cutContent()}...`;
        });
        res.render('home/index', { articles });
      })
      .catch(err => {
        console.error(err);
      });
  }
}