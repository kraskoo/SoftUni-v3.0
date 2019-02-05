module.exports = (res, e, page, viewObject = undefined, type = 'error') => {
  console.log(e);
  if (type === 'error') {
    res.locals.error = e;
  } else if (type === 'success') {
    res.locals.success = e;
  }
  
  if (viewObject) {
    res.render(page, viewObject);
  } else {
    res.render(page);
  }
};