module.exports = function (res, message, view, viewObject = undefined, type = 'error') {
  console.log(message);
  if (type === 'error') {
    res.locals.error = message;
  } else if (type === 'success') {
    res.locals.success = message;
  }
  
  if (viewObject) {
    res.render(view, viewObject);
  } else {
    res.render(view);
  }
}