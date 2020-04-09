let path = require("path");

module.exports = function(pathFromStatic) {
  return path.posix.join("./static", pathFromStatic || "");
}