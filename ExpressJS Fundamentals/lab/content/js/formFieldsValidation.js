document.addEventListener('DOMContentLoaded', function () {
  var formFields = Array.from(document.getElementsByTagName('input')).concat(Array.from(document.getElementsByTagName('textarea')));
  for (var field of formFields) {
    field.addEventListener('invalid', function (evnt) {
      var formField = evnt.currentTarget;
      formField.setCustomValidity('');
      if (!formField.validity.valid) {
        formField.setCustomValidity(formField.getAttribute('message'));
      }
    });
    field.addEventListener('input', function (evnt) {
      evnt.currentTarget.setCustomValidity('');
    });
  }
});