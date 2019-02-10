const fs = require('fs');
const storageSync = require('./storage');
const filePath = './storage.json';

function save() {
  return storageSync.save(function(storage) {
    return new Promise((resolve, reject) => {
      fs.writeFile(filePath, JSON.stringify(storage), {
        encoding: 'utf8',
        flag: 'w',
        mode: fs.constants.S_IWUSR
      }, err => {
        if (err) {
          reject(err);
          return;
        }
      });
      resolve();
    });
  });
}

function load() {
  return storageSync.load(function(rtnFunc) {
    // fs.exists is deprecated!
    let isFileExists = fs.existsSync(filePath);
    return new Promise((resolve, reject) => {
      if (isFileExists) {
        fs.readFile(filePath, {
          encoding: 'utf8',
          flag: 'r'
        }, (err, data) => {
          if (err) {
            reject(err);
            return;
          }
          
          rtnFunc(data);
          resolve();
        });
      } else {
        resolve();
      }
    });
  });
}

module.exports = {
  put: storageSync.put,
  get: storageSync.get,
  getAll: storageSync.getAll,
  update: storageSync.update,
  delete: storageSync.delete,
  clear: storageSync.clear,
  save: save,
  load: load
}