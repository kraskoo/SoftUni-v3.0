const http = require('http');
const url = require('url');
const qs = require('querystring');
const port = process.env.PORT || 5000;
const handlers = require('./handlers/handlerBlender');

require('./config/db')();

http.createServer((req, res) => {
  res.redirect = function(location) {
    res.writeHead(302, { 'Location': location });
    res.end();
  };
  req.path = url.parse(req.url);
  req.pathname = req.path.pathname
  req.pathquery = qs.parse(req.path.query)
  for (let handler of handlers) {
    let task = handler(req, res);
    if (task !== true) {
      break;
    }
  }
}).listen(port);