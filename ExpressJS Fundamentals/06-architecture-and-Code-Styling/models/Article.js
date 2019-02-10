const mongoose = require('mongoose');
const cutContentLength = 50;

const ArticleSchema = new mongoose.Schema({
  title: { type: String, required: true },
  content: { type: String, required: true },
  author: { type: mongoose.Schema.Types.ObjectId, ref: 'User' },
  date: { type: Date, default: Date.now }
}).method({
  cutContent: function() {
    return this.content.split('').splice(0, cutContentLength).join('');
  }
});

const Article = mongoose.model('Article', ArticleSchema);

module.exports = Article;