const fs = require('fs');
const filePath = './storage.json';
let storage = {};

function isKeyIsStringType(key) {
  return typeof(key) === 'string';
}

function hasStorageKey(key) {
  return storage.hasOwnProperty(key);
}

function isStorageEmpty() {
  return Object.keys(storage).length === 0;
}

function put(key, value) {
  if (!isKeyIsStringType(key)) {
    throw new Error('Key should be a string type!');
  }

  if (hasStorageKey(key)) {
    throw new Error('Key already exists!');
  }

  storage[key] = value;
}

function get(key) {
  if (!isKeyIsStringType(key)) {
    throw new Error('Key should be a string type!');
  }

  if (!hasStorageKey(key)) {
    throw new Error('Key doesn\'t exist!');
  }

  return storage[key];
}

function getAll() {
  if (isStorageEmpty()) {
    return 'Storage is empty!';
  }

  return storage;
}

function update(key, newValue) {
  if (!isKeyIsStringType(key)) {
    throw new Error('Key should be a string type!');
  }

  if (!hasStorageKey(key)) {
    throw new Error('Key doesn\'t exist!');
  }

  storage[key] = newValue;
}

function del(key) {
  if (!isKeyIsStringType(key)) {
    throw new Error('Key should be a string type!');
  }

  if (!hasStorageKey(key)) {
    throw new Error('Key doesn\'t exist!');
  }
  
  delete storage[key];
}

function clear() {
  storage = {};
}

function save(pms = undefined) {
  if (pms) {
    return pms(storage);
  }

  fs.writeFileSync(filePath, JSON.stringify(storage), {
    encoding: 'utf8',
    flag: 'w',
    mode: fs.constants.S_IWUSR
  });
}

function load(pms = undefined) {
  if (pms) {
    return pms(function(data) {
      storage = JSON.parse(data);
    });
  }

  if (fs.existsSync(filePath)) {
    storage = JSON.parse(
      fs.readFileSync(filePath, {
        encoding: 'utf8',
        flag: 'r'
      }));
  }
}

module.exports = {
  put,
  get,
  getAll,
  update,
  delete: del,
  clear,
  save,
  load
};