const fs = require('fs');
const db = require('../config/dataBase');
const views = {
  '/': './views/home.html',
  '/addMovie': './views/addMovie.html',
  '/viewAllMovies': './views/viewAll.html',
  '/details': './views/details.html',
  'movieContainer': `<div class="movie movie-details">
    <img class="moviePoster" src="{{movie-url}}" />
    <br />
    <a href="/movies/details/{{id}}">Details</a>
  </div>`,
  'movieDetails': `<div class="content movie-details">
    <img src="{{poster}}" alt="" />
    <h3>Title: {{title}}</h3>
    <h3>Year: {{year}}</h3>
    <p> {{description}}</p>
  </div>`
};

function insertNewMovie(path, req, res) {
  if (path === '/addMovie') {
    let body = '';
    let bodyObject = {};
    req.on('data', chunk => {
      body += chunk.toString();
    });
    req.on('end', () => {
      body.split('&').forEach(val => {
        let kvp = val.split('=');
        if (!bodyObject.hasOwnProperty(kvp[0])) {
          bodyObject[kvp[0]] = unescape(kvp[1]).replace(/\+/g, ' ');
        }
      });
      bodyObject['movieYear'] = parseInt(bodyObject['movieYear']);
      bodyObject['id'] = db.length + 1;
      if (isValidMovieObject(bodyObject)) {
        db.push(bodyObject);
        // Redirect
        res.writeHead(302, {
          'Location': '/viewAllMovies'
        });
        res.end();
      } else {

      }
    });
  } else {
    res.writeHead(401, {
      'Content-Type': 'text-plain'
    });
    res.end('Unknown url path');
  }
}

function isValidMovieObject(obj) {
  let basicValidations = obj.movieTitle !== '' && obj.moviePoster !== '';
  let isPosterValid = /(https?:\/\/.*\.(?:png|jpg))/.test(obj.moviePoster);
  let isYearValied = /\d{4}/.test(obj.movieYear);
  let additionalValidation = isPosterValid && isYearValied;
  return basicValidations && additionalValidation;
}

function testPathForMovieDetails(path) {
  return /\/movies\/details\/\d+/.test(path);
}

function canHandle(path) {
  return views.hasOwnProperty(path) || testPathForMovieDetails(path);
}

function handle(path, res) {
  let isMovieDetails = testPathForMovieDetails(path);
  if (isMovieDetails) {
    let urlParts = path.split('/');
    let id = parseInt(urlParts[urlParts.length - 1]);
    let current = db.filter(x => x.id === id)[0];
    fs.readFile(views['/details'], {
      encoding: 'utf8',
      flag: 'r'
    }, (err, data) => {
      if (err) {
        console.error(err);
      }

      let divContainer = views['movieDetails']
        .replace('{{poster}}', current.moviePoster)
        .replace('{{title}}', current.movieTitle)
        .replace('{{year}}', current.movieYear)
        .replace('{{description}}', current.movieDescription);
      data = data.replace('{{replaceMe}}', divContainer);
      res.writeHead(200, {
        'Content-Type': 'text/html'
      });
      res.write(data);
      res.end();
    });
  } else {
    fs.readFile(views[path], {
      encoding: 'utf8',
      flag: 'r'
    }, (err, data) => {
      if (err) {
        console.error(err);
      }

      if (path === '/viewAllMovies') {
        let sortedDb = db.sort((a, b) => b.movieYear - a.movieYear);
        let allMovies = '';
        for (let movie of sortedDb) {
          allMovies += views['movieContainer'].replace('{{id}}', movie.id).replace('{{movie-url}}', movie.moviePoster);
        }

        data = data.replace('{{replaceMe}}', allMovies);
      }

      res.writeHead(200, {
        'Content-Type': 'text/html'
      });
      res.write(data);
      res.end();
    });
  }
}

module.exports = {
  canHandle,
  handle,
  insertNewMovie
};