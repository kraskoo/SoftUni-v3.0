const fs = require('fs');
const Image = require('../models/ImageSchema');
const Tag = require('../models/TagSchema');

module.exports = (req, res) => {
  if (req.pathname === '/search') {
    let display = '';
    fs.readFile('./views/results.html', {
      encoding: 'utf8',
      flag: 'r'
    }, (err, data) => {
      if (err) {
        console.error(err);
        return;
      }

      let query = req.pathquery;
      let bd = query.beforeDate.trim();
      let ad = query.afterDate.trim();
      let limit = query.Limit.trim();
      if (limit !== '') {
        limit = parseInt(limit);
      }

      let beforeDate = null;
      if (bd !== '') {
        bd = bd.split('-');
        beforeDate = new Date(parseInt(bd[0]), parseInt(bd[1]) - 1, parseInt(bd[2]));
      }

      let afterDate = null;
      if (ad !== '') {
        ad = ad.split('-');
        afterDate = new Date(parseInt(ad[0]), parseInt(ad[1]) - 1, parseInt(ad[2]));
      }

      let tags = query.tagName.trim().split(',').filter(x => x !== '').map(x => x = x.trim());
      let imageCounter = 0;
      let hasLimit = typeof(limit) === 'number';
      Tag.find({ name: { $in: tags } }).then(result => {
        if (result.length === 0) {
          res.writeHead(200, { 'Content-Type': 'text/html' });
          data = data.replace(`<div class='replaceMe'></div>`, '');
          res.end(data);
          return;
        }

        let allImageIds = [];
        result.forEach(x => {
          for (let image of x.images) {
            let id = image._id.toHexString();
            if (!allImageIds.includes(id)) {
              allImageIds.push(id);
            }
          }
        });
        let imageCount = allImageIds.length;
        if (hasLimit) {
          imageCount = limit;
        }

        let imageQuery = Image.find({ _id: { $in: allImageIds } });
        if (beforeDate !== null) {
          imageQuery = imageQuery.where('creationDate').lt(beforeDate);
        }

        if (afterDate !== null) {
          imageQuery = beforeDate !== null ?
            imageQuery.gt(afterDate) :
            imageQuery.where('creationDate').gt(afterDate);
        }

        if (hasLimit) {
          imageQuery = imageQuery.limit(limit);
        }

        imageQuery.then(images => {
          if (images.length === 0) {
            res.writeHead(200, { 'Content-Type': 'text/html' });
            data = data.replace(`<div class='replaceMe'></div>`, '');
            res.end(data);
            return;
          }

          for (let image of images) {
            display += `<fieldset id=${image._id}>
              <legend>${image.title}:</legend>
              <img src="${image.url}"></img>
              <p>${image.description}</p>
              <button onclick='location.href="/delete?id=${image._id}"' class='deleteBtn'>Delete</button>
            </fieldset>`;
            imageCounter++;
            if (imageCount === imageCounter) {
              data = data.replace(`<div class='replaceMe'></div>`, display);
              res.writeHead(200, { 'Content-Type': 'text/html' });
              res.end(data);
            }
          }
        }).catch(err => {
          if (err) {
            console.error(err);
          }
        });
      }).catch(err => {
        if (err) {
          console.error(err);
        }
      });
    });
  } else {
    return true;
  }
};