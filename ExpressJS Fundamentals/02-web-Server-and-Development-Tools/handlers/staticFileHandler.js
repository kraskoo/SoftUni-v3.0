const fs = require('fs');

// Allowed extensions: html, css, js, png and jpg, ico
const allowedMimes = {
  html: 'text/html',
  css: 'text/css',
  js: 'application/javascript',
  png: 'image/png',
  jpg: 'image/jpeg',
  ico: 'image/x-icon'
};

function isTextFile(ext) {
  return ext === 'html' || ext === 'css' || ext === 'js';
}

function isAllowedPath(path) {
  return (path.startsWith('/public') || path === '/favicon.ico') && path.split('.').length > 1;
}

function isValidExtension(ext) {
  return allowedMimes.hasOwnProperty(ext);
}

function isFileExists(path) {
  return fs.existsSync(path);
}

function handle(path, res) {
  let fileAndExtension = path.split('.');
  let extension = fileAndExtension[fileAndExtension.length - 1].toLowerCase();
  if (isValidExtension(extension)) {
    if (path === '/favicon.ico') {
      path = '/public/images/favicon.ico';
    }

    path = `.${path}`;
    if (isFileExists(path)) {
      if (isTextFile(extension)) {
        fs.readFile(path, (err, data) => {
          if (err) {
            console.error(err);
          }
  
          res.writeHead(200, {
            'Content-Type': allowedMimes[extension]
          });
          res.write(data);
          res.end();
        });
      } else {
        fs.readFile(path, (err, data) => {
          if (err) {
            console.error(err);
          }
  
          res.writeHead(200, {
            'Content-Type': allowedMimes[extension]
          });
          res.end(data);
        });
      }
    } else {
      res.writeHead(404, {
        'Content-Type': 'text/plain'
      });
      res.write('File not found!');
      res.end();
    }
  } else {
    res.writeHead(401, {
      'Content-Type': 'text/plain'
    });
    res.write('Unauthorized file access!');
    res.end();
  }
}

module.exports = {
  handle,
  isAllowedPath,
  isFileExists
};