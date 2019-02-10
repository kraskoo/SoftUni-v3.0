const fs = require('fs');
const shortid = require('shortid');
const formidable = require('formidable');
const basePath = './public/memeStorage/';
const paths = {
  viewAll: './views/viewAll.html',
  viewAddMeme: './views/addMeme.html',
  getDetails: './views/details.html'
};

function viewAll(req, res) {
  fs.readFile(paths.viewAll, {
    encoding: 'utf8',
    flag: 'r'
  }, (err, data) => {
    if (err) {
      console.error(err);
      return;
    }

    res.writeHead(200, { 'Content-Type':'text/html' });
    let responseDb = '';
    let sortedDb = req.db.getDb().filter(x => x.privacy && x.privacy === 'on').sort((a, b) => b.dateStamp - a.dateStamp);
    for (let meme of sortedDb) {
      responseDb += `<div class="meme">
        <a href="/getDetails?id=${meme.id}">
        <img class="memePoster" src="${meme.memeSrc}"/>
      </div>`;
    }

    let replacedData = data.replace('{{replaceMe}}', responseDb);
    res.writeHead(200, { 'Content-Type':'text/html' });
    res.write(replacedData);
    res.end();
  });
}

function viewAddMeme(req, res) {
  fs.readFile(paths.viewAddMeme, {
    encoding: 'utf8',
    flag: 'r'
  }, (err, data) => {
    if (err) {
      console.error(err);
      return;
    }

    res.writeHead(200, { 'Content-Type':'text/html' });
    res.write(data);
    res.end();
  });
}

function addMeme(req, res) {
  let form = new formidable.IncomingForm();
  let newMeme = {};
  newMeme.id = shortid.generate();
  form.on('field', (field, value) => {
    if (field === 'memeTitle') {
      field = 'title'
    } else if (field === 'status') {
      field = 'privacy';
    } else if (field === 'memeDescription') {
      field = 'description';
    }

    newMeme[field] = value;
  }).on('fileBegin', (name, file) => {
    file.path = newMeme.memeSrc = basePath + newMeme.id + '.jpg';
  }).on('end', () => {
    newMeme.dateStamp = parseInt(Date.now());
    req.db.add(newMeme);
    req.db.save().then(() => { res.redirect('/viewAllMemes'); }).catch(err => {
      if (err) {
        console.error(err);
      }
    });
  }).parse(req);
}

function getDetails(req, res) {
  fs.readFile(paths.getDetails, {
    encoding: 'utf8',
    flag: 'r'
  }, (err, data) => {
    if (err) {
      console.error(err);
      return;
    }

    let query = req.path.query;
    let id = query.split('=')[1];
    let targetedMeme = req.db.getDb().filter(x => x.id === id)[0];
    let response = `<div class="content">
      <img src="${targetedMeme.memeSrc}" alt=""/>
      <h3>Title ${targetedMeme.title}</h3>
      <p> ${targetedMeme.description}</p>
    </div>`;
    res.writeHead(200, { 'Content-Type':'text/html' });
    res.write(data.replace('{{replaceMe}}', response));
    res.end();
  });
}

module.exports = (req, res) => {
  if (req.pathname === '/viewAllMemes' && req.method === 'GET') {
    viewAll(req, res);
  } else if (req.pathname === '/addMeme' && req.method === 'GET') {
    viewAddMeme(req, res);
  } else if (req.pathname === '/addMeme' && req.method === 'POST') {
    addMeme(req, res);
  } else if (req.pathname.startsWith('/getDetails') && req.method === 'GET') {
    getDetails(req, res);
  } else if (req.pathname.startsWith('public/memeStorage') && req.method === 'GET') {
    console.log('HERE');
  } else {
    return true;
  }
};