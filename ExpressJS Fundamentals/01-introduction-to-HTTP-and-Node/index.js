const storage = require('./storage');
storage.load()
storage.put('first','firstValue')
storage.put('second','secondValue')
storage.put('third','thirdValue')
storage.put('fouth','fourthValue')
console.log(storage.get('first'))
console.log(storage.getAll())
storage.delete('second')
storage.update('first','updatedFirst')
storage.save()
storage.clear()
console.log(storage.getAll())
storage.load()
console.log(storage.getAll())

const storageAsync = require('./storageAsync');
storageAsync.load().then(() => {
  console.log(storageAsync.getAll());
  storageAsync.update('first', '1');
  storageAsync.put('second', '2');
  console.log(storageAsync.getAll());
  storageAsync.save().then(() => {
    storageAsync.update('second', '222');
    storageAsync.update('third', 3);
    console.log(storageAsync.getAll());
    storageAsync.delete('first');
    storageAsync.save().then(() => {
      console.log(storageAsync.getAll());
    }).catch(err => console.error(err));
  }).catch(err => console.error(err));
}).catch(err => console.error(err));