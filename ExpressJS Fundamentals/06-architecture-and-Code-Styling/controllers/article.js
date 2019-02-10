const Article = require('../models/Article');

module.exports = {
  createGet: (req, res) => {
    res.render('article/create');
  },
  createPost: (req, res) => {
    let { title, content } = req.body;
    let newArticle = new Article({ title, content, author: req.user._id });
    newArticle.save().then(() => {
      res.redirect('/');
    }).catch(err => {
      console.error(err);
    });
  },
  detailGet: (req, res) => {
    let id = req.params.id;
    let isRequestHasUser = req.hasOwnProperty('user');
    Article.findById(id)
      .populate('author')
      .then(article => {
        article.fullName = article.author.fullName;
        article.splittedContent = article.content.split(/\r?\n/g);
        let isAdmin = isRequestHasUser && req.user.roles.indexOf('Admin') !== -1;
        let isAuthor = isRequestHasUser && req.user._id.toHexString() === article.author._id.toHexString();
        article.isAuthorOrAdmin = isAdmin || isAuthor;
        res.render('article/details', article);
      }).catch(err => {
        console.error(err);
      });
  },
  editGet: (req, res) => {
    let id = req.params.id;
    let isRequestHasUser = req.hasOwnProperty('user');
    Article.findById(id)
      .populate('author')
      .then(article => {
        let isAdmin = isRequestHasUser && req.user.roles.indexOf('Admin') !== -1;
        let isAuthor = isRequestHasUser && req.user._id.toHexString() === article.author._id.toHexString();
        if (!(isAdmin || isAuthor)) {
          res.redirect('/');
          return;
        }

        res.render('article/edit', article);
      })
      .catch(err => {
        console.error(err);
      });
  },
  editPost: (req, res) => {
    let id = req.params.id;
    let isRequestHasUser = req.hasOwnProperty('user');
    let { title, content } = req.body;
    Article.findById(id)
      .populate('author')
      .then(article => {
        let isAdmin = isRequestHasUser && req.user.roles.indexOf('Admin') !== -1;
        let isAuthor = isRequestHasUser && req.user._id.toHexString() === article.author._id.toHexString();
        if (!(isAdmin || isAuthor)) {
          res.redirect('/');
          return;
        }

        article.title = title;
        article.content = content;
        article.save(() => {
          res.redirect('/');
        }).catch(err => {
          console.error(err);
        });
      })
      .catch(err => {
        console.error(err);
      });
  },
  deleteGet: (req, res) => {
    let id = req.params.id;
    let isRequestHasUser = req.hasOwnProperty('user');
    Article.findById(id)
    .populate('author')
    .then(article => {
      let isAdmin = isRequestHasUser && req.user.roles.indexOf('Admin') !== -1;
      let isAuthor = isRequestHasUser && req.user._id.toHexString() === article.author._id.toHexString();
      if (!(isAdmin || isAuthor)) {
        res.redirect('/');
        return;
      }
      
      res.render('article/delete', article);
    })
    .catch(err => {
      console.error(err);
    });
  },
  deletePost: (req, res) => {
    let id = req.params.id;
    let isRequestHasUser = req.hasOwnProperty('user');
    Article.findById(id)
    .populate('author')
    .then(article => {
      let isAdmin = isRequestHasUser && req.user.roles.indexOf('Admin') !== -1;
      let isAuthor = isRequestHasUser && req.user._id.toHexString() === article.author._id.toHexString();
      if (!(isAdmin || isAuthor)) {
        res.redirect('/');
        return;
      }
      
      Article.findByIdAndRemove(id).then(() => {
        res.redirect('/');
      }).catch(err => {
        console.error(err);
      });
    })
    .catch(err => {
      console.error(err);
    });
  }
};