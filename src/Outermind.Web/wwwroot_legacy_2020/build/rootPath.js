let path = require("path");

module.exports = function(pathFromRoot) {
  return path.join(__dirname, "..", pathFromRoot);
};