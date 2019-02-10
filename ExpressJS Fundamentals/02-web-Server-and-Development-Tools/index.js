const http = require('http');
const url = require('url');
const staticFileHandler = require('./handlers/staticFileHandler');
const viewFileHandler = require('./handlers/viewFileHandler');
const port = process.env.PORT || 8080;

http.createServer((req, res) => {
  let method = req.method.toLowerCase();
  let urlParts = url.parse(req.url);
  let path = urlParts.pathname;
  if (method === 'get') {
    if (viewFileHandler.canHandle(path)) {
      viewFileHandler.handle(path, res);
    } else if (staticFileHandler.isAllowedPath(path)) {
      staticFileHandler.handle(path, res);
    } else {
      res.writeHead(400, {
        'Content-Type': 'text/plain'
      });
      res.write('Bad request!');
      res.end();
    }
  } else if (method === 'post') {
    viewFileHandler.insertNewMovie(path, req, res);
  } else {
    res.writeHead(400, {
      'Content-Type': 'text/plain'
    });
    res.write('Unsupported http method!');
    res.end();
  }
}).listen(port);
console.log(`Run your browser in http://localhost:${port}`);