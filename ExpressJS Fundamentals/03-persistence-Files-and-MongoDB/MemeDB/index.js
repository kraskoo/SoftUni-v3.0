const http = require('http');
const url = require('url');
const handlers = require('./handlers/handlerBlender');
const db = require('./config/dataBase');
const port = 2323;

db.load().then(() => {
  console.log('testing');
  http.createServer((req, res) => {
    res.redirect = function(location) {
      res.writeHead(302, { 'Location': location });
      res.end();
    };
    req.path = url.parse(req.url);
    req.pathname = req.path.pathname;
    req.db = db;
    for (let handler of handlers) {
      let task = handler(req, res);
      if (task !== true) {
        break;
      }
    }
  }).listen(port);
  console.log('Im listening on ' + port);
}).catch(() => {
  console.log('Failed to load DB');
});