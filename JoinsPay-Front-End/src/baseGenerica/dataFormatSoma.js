module.exports = function (s) {
  let h = Math.floor(s / 3600);
  let min = Math.floor((s - h * 3600) / 60);
  let se = s - Math.floor(s / 60) * 60;


  if (parseInt(h) < 10) {
    h = "0" + h.toString();
  }

  if (parseInt(min) < 10) {
    min = "0" + min.toString();
  }

  if (parseInt(se) < 10) {
    se = "0" + se.toString();
  }

 

  return h + ":" + min + ":" + se;
}
