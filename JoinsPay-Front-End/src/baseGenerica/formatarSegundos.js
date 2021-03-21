module.exports = function (h, min, s) {
  return parseFloat(h * 3600 + min * 60 + s);
}
