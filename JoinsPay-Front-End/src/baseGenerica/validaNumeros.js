module.exports = function (event) {
  let tecla = event.keyCode;

  if ((tecla >= 48 && tecla <= 57)) {
    return true;
  } if ((tecla == 46)){
    return true;
  }else {
    event.preventDefault();
    return false;
  }
}
